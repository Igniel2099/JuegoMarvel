using CommunityToolkit.Maui.Views;
using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.View;
using MensajesServidor;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace JuegoMarvel.ModuloLogin.ViewModel.Comandos;

/// <summary>
/// Comando para confirmar el código de recuperación de cuenta introducido por el usuario.
/// Valida el formato del código, consulta al servidor y gestiona la navegación y los mensajes de error o éxito.
/// </summary>
/// <remarks>
/// Utiliza <see cref="OlvidarInformacionViewModel"/> para obtener los datos de la vista y <see cref="AppSettings"/>
/// para la configuración de la conexión al servidor.
/// </remarks>
public class ComandoConfirmarOlvidarInformacion(
    OlvidarInformacionViewModel vm,
    AppSettings settings) : BaseCommand
{
    /// <summary>
    /// Propiedad privada del View Model de Olvidar Informacion
    /// </summary>
    private readonly OlvidarInformacionViewModel _vm = vm;
    private readonly AppSettings _settings = settings;

    /// <summary>
    /// Ejecuta la lógica de validación y comprobación del código de confirmación.
    /// Si el código es válido y existe en el servidor, navega a la pantalla de cambio de contraseña.
    /// Si no, muestra un popup de error.
    /// </summary>
    /// <param name="parameter">Debe ser una instancia de <see cref="OlvidarInformacion"/> (la vista actual).</param>
    public override async void Execute(object? parameter)
    {
        if (parameter is OlvidarInformacion olvidarInformacion)
        {
            if (FormatoCodigoConfirmacion(_vm.CodigoConfirmacion))
            {
                var popup = new PopupErrores(new PopupErroresViewModel(
                    "Error al Recuperar Cuenta",
                    "Error el codigo de confirmación esta vacio"
                    )
                );
                await olvidarInformacion.ShowPopupAsync(popup);
            }
            else
            {
                using var cliente = new TcpClient();
                await cliente.ConnectAsync(_settings.IpServidor, _settings.PuertoServidor);
                using var stream = cliente.GetStream();

                var mensaje = new MensajesModuloLogin(
                    EnumOrigen.OlvidarInformacion,
                    EnumTipoRespuesta.Comprobar,
                    [new Propiedad(EnumTipoValor.CodigoConfirmacion, _vm.CodigoConfirmacion!),
                    new Propiedad(EnumTipoValor.CorreoElectronico, _vm.CorreoElectronico!)],
                    null);

                string contenido = JsonConvert.SerializeObject(mensaje);
                byte[] datosEnvio = Encoding.UTF8.GetBytes(contenido);
                await stream.WriteAsync(datosEnvio, 0, datosEnvio.Length);

                byte[] buffer = new byte[1024];
                int leidos = await stream.ReadAsync(buffer);
                string respuestaJson = Encoding.UTF8.GetString(buffer, 0, leidos);

                var respuestaServidor = JsonConvert.DeserializeObject<MensajesModuloLogin?>(respuestaJson);
                if (respuestaServidor?.Respuesta is not null)
                {
                    if (respuestaServidor.Respuesta == EnumRespuesta.Existente)
                    {
                        var popup = new PopupErrores(new PopupErroresViewModel("Codigo de Confirmacion Correcto"));
                        await olvidarInformacion.ShowPopupAsync(popup);
                        try
                        {
                            // Push sin animación nativa
                            var window = Application.Current.Windows[0];
                            var nav = window.Page.Navigation;

                            // Para hacer el PushModal sin animación nativa:
                            await nav.PushModalAsync(new CambiarContrasena(new CambiarContrasenaViewModel(_settings, respuestaServidor.Propiedades[0].Valor)), false);
                        }
                        catch (Exception ex)
                        {
                            Debug.WriteLine($"===============Error en ComandoNavegarOlvidarInformacion: {ex.Message}");
                            throw;
                        }

                    }
                    else
                    {
                        string mensajeError = respuestaServidor.Respuesta == EnumRespuesta.NoExistente
                            ? "Ese codigo de confirmación no es corrector."
                            : "Error al buscar usuario.";
                        var popup = new PopupErrores(new PopupErroresViewModel(
                                "Error al Recuperar Cuenta",
                                mensajeError
                            )
                        );
                        await olvidarInformacion.ShowPopupAsync(popup);
                    }
                }
            }
        }
    }

    /// <summary>
    /// Valida el formato del código de confirmación.
    /// </summary>
    /// <param name="CodigoConfirmacion">Código de confirmación a validar.</param>
    /// <returns>True si el código es nulo o vacío o no tiene longitud 6; false si tiene longitud 6 y no es nulo o vacío.</returns>
    public bool FormatoCodigoConfirmacion(string? CodigoConfirmacion)
    {
        return string.IsNullOrEmpty(CodigoConfirmacion) && CodigoConfirmacion!.Length == 6;
    }
}
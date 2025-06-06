using CommunityToolkit.Maui.Views;
using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.View;
using MensajesServidor;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;

namespace JuegoMarvel.ModuloLogin.ViewModel.Comandos;

/// <summary>
/// Comando para confirmar el cambio de contraseña de un usuario.
/// Valida la nueva contraseña y, si es válida, la envía al servidor para su actualización.
/// Muestra mensajes de éxito o error mediante popups y gestiona la navegación tras el proceso.
/// </summary>
/// <remarks>
/// Este comando utiliza <see cref="CambiarContrasenaViewModel"/> para obtener los datos de la vista y <see cref="AppSettings"/>
/// para la configuración de la conexión al servidor. Utiliza <see cref="ValidadorContrasena"/> para validar la política de contraseñas.
/// </remarks>
public class ComandoConfirmarCambiarContrasena(CambiarContrasenaViewModel vm, AppSettings settings) : BaseCommand
{
    /// <summary>
    /// Propiedad privada del View Model
    /// </summary>
    private readonly CambiarContrasenaViewModel _vm = vm;

    /// <summary>
    /// Propiedad Privada de la configuración de la app
    /// </summary>
    private readonly AppSettings _settings = settings;

    /// <summary>
    /// Propiedad privada del Validador de constraseñas
    /// </summary>
    private readonly ValidadorContrasena _validadorContrasena = new();

    /// <summary>
    /// Ejecuta la lógica de validación y actualización de la contraseña.
    /// Si la contraseña es válida, la envía al servidor y muestra un popup de éxito; si no, muestra un popup de error.
    /// </summary>
    /// <param name="parameter">Debe ser una instancia de <see cref="CambiarContrasena"/> (la vista actual).</param>
    public override async void Execute(object? parameter)
    {
        if (parameter is CambiarContrasena cambiarContrasena)
        {
            var (contrasenOk, mensajeContrasena) = _validadorContrasena.Validar(_vm.Contrasena, _vm.ConfirmarContrasena);

            _vm.EstadoImgContrasena = contrasenOk;
            _vm.EstadoImgConfirmarContrasena = contrasenOk;
            // Si es verdadero mandarle al servidor la contraseña para guardarlo.
            if (contrasenOk)
            {
                try
                {
                    using var cliente = new TcpClient();
                    await cliente.ConnectAsync(_settings.IpServidor, _settings.PuertoServidor);
                    using var stream = cliente.GetStream();

                    var mensaje = new MensajesModuloLogin(
                        EnumOrigen.OlvidarInformacion,
                        EnumTipoRespuesta.Guardar,
                        [new Propiedad(EnumTipoValor.Contraseña, _vm.Contrasena!),
                        new Propiedad(EnumTipoValor.NombreUsuario, _vm.NombreUsuario)],
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
                        bool enviado = respuestaServidor.Respuesta == EnumRespuesta.Guardado;

                        // Abro popup con todo a null, lo que quiere decir que me salta el
                        // mensaje en el medio.

                        var popup = new PopupErrores(new PopupErroresViewModel("La contraseña ha sido cambiada con exito."));

                        await cambiarContrasena.ShowPopupAsync(popup);

                        await Application.Current.MainPage
                            .Navigation
                            .PopModalAsync(false); // Vuelvo a la pantalla Recuperar Cuenta

                        await Application.Current.MainPage
                            .Navigation
                            .PopModalAsync(false); // Vuelvo a la pantalla Login
                        // Mostrar popup de contrasena guardada y volver a login.
                    }
                    else
                    {
                        var popup = new PopupErrores(new PopupErroresViewModel("Error al Cambiar Contraseña", "Error al guardar la contraseña en el Servidor"));

                        await cambiarContrasena.ShowPopupAsync(popup);// Mostrar popup de error al guardar la contrasena. que es error del servidor
                    }
                }
                catch (SocketException)
                {
                    var popup = new PopupErrores(new PopupErroresViewModel("Error al Cambiar Contraseña", "Error al conectar con el Servidor"));

                    await cambiarContrasena.ShowPopupAsync(popup);
                }
            }
            else
            {
                var popup = new PopupErrores(new PopupErroresViewModel("Error al Cambiar Contraseña", mensajeContrasena));

                await cambiarContrasena.ShowPopupAsync(popup);
                // Mostrar popup de error de validacion de contrasena
            }
        }
    }
}
using CommunityToolkit.Maui.Views;
using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.View;
using MensajesServidor;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace JuegoMarvel.ModuloLogin.ViewModel.Comandos;

public class ComandoConfirmarOlvidarInformacion(
    OlvidarInformacionViewModel vm,
    AppSettings settings) : BaseCommand
{
    private readonly OlvidarInformacionViewModel _vm = vm;
    private readonly AppSettings _settings = settings;

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
                    if(respuestaServidor.Respuesta == EnumRespuesta.Existente)
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

    public bool FormatoCodigoConfirmacion(string? CodigoConfirmacion)
    {
        return string.IsNullOrEmpty(CodigoConfirmacion) && CodigoConfirmacion!.Length == 6;
    }

}
  
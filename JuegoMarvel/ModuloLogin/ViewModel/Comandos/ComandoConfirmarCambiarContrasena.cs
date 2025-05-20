using CommunityToolkit.Maui.Views;
using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.View;
using MensajesServidor;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;

namespace JuegoMarvel.ModuloLogin.ViewModel.Comandos;

public class ComandoConfirmarCambiarContrasena(CambiarContrasenaViewModel vm, AppSettings settings) : BaseCommand
{
    private readonly CambiarContrasenaViewModel _vm = vm;
    private readonly AppSettings _settings = settings;
    private readonly ValidadorContrasena _validadorContrasena = new();

    public override async void Execute(object? parameter)
    {
        if ( parameter is CambiarContrasena cambiarContrasena)
        {
            var (contrasenOk, mensajeContrasena) =  _validadorContrasena.Validar(_vm.Contrasena, _vm.ConfirmarContrasena);
            
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
                    var popup = new PopupErrores(new PopupErroresViewModel("Error al Cambiar Contraseña","Error al conectar con el Servidor"));

                    await cambiarContrasena.ShowPopupAsync(popup);
                }
            }
            else
            {
                var popup = new PopupErrores(new PopupErroresViewModel("Error al Cambiar Contraseña",mensajeContrasena));

                await cambiarContrasena.ShowPopupAsync(popup);
                // Mostrar popup de error de validacion de contrasena
            }
        }
    }

}
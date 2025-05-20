using CommunityToolkit.Maui.Views;
using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.View;
using MensajesServidor;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;

namespace JuegoMarvel.ModuloLogin.ViewModel.Comandos;

public class ComandoEnviar : BaseCommand
{
    private readonly OlvidarInformacionViewModel _vm;
    
    private readonly AppSettings _settings;
    private readonly ComprobadorDominio _comprobador;

    private readonly ValidadorCorreoElectronico _validadorCorreo;

    public ComandoEnviar(OlvidarInformacionViewModel vm, AppSettings settings, ComprobadorDominio comprobador)
    {
        _vm = vm;
        _settings = settings;
        _comprobador = comprobador;
        _validadorCorreo = new(EnumOrigen.OlvidarInformacion, _settings, _comprobador);
    } 
    public override async void Execute(object? parameter)
    {
        var (correoOk, mensajeCorr) = await _validadorCorreo.ValidarAsync(_vm.CorreoElectronico!);
        if (parameter is OlvidarInformacion olvidarInformacion)
        {
            if (correoOk)
            {
                try
                {
                    using var cliente = new TcpClient();
                    await cliente.ConnectAsync(_settings.IpServidor, _settings.PuertoServidor);
                    using var stream = cliente.GetStream();

                    var mensaje = new MensajesModuloLogin(
                        EnumOrigen.OlvidarInformacion,
                        EnumTipoRespuesta.Enviar,
                        [new Propiedad(EnumTipoValor.CorreoElectronico, _vm.CorreoElectronico!)],
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
                        bool enviado = respuestaServidor.Respuesta == EnumRespuesta.Enviado;

                        if (enviado)
                        {
                            _vm.Editable = true; // Asi no puede editar el Entry

                            var popup = new PopupErrores(new PopupErroresViewModel(
                                    "El Codigo de Confirmacion Ha sido enviado ha tu correo electronico."
                                )
                            );

                            await olvidarInformacion.ShowPopupAsync(popup);
                        }
                        else
                        { 
                            var popup = new PopupErrores(new PopupErroresViewModel(
                                    "Error al Recuperar Cuenta", 
                                    "Ha ocurrido un error al enviar el Correo."
                                )
                            );

                            await olvidarInformacion.ShowPopupAsync(popup);
                        }
                    }
                }
                catch (SocketException)
                {
                    var popup = new PopupErrores(new PopupErroresViewModel(
                            "Error al Recuperar Cuenta", 
                            "Ha ocurrido un error al intentar conectarse con el Servidor."
                        )
                    );

                    await olvidarInformacion.ShowPopupAsync(popup);
                }
            }
            else
            {
                var popup = new PopupErrores(new PopupErroresViewModel("Error al Recuperar Cuenta", mensajeCorr));

                await olvidarInformacion.ShowPopupAsync(popup);
            }
            
        }
    }
}
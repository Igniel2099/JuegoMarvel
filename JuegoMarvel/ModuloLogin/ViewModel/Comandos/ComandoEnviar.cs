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
/// Comando para enviar el código de recuperación de cuenta al correo electrónico del usuario.
/// Valida el correo electrónico, realiza la petición al servidor y muestra mensajes de éxito o error mediante popups.
/// </summary>
/// <remarks>
/// Utiliza <see cref="OlvidarInformacionViewModel"/> para obtener los datos de la vista y <see cref="AppSettings"/>
/// para la configuración de la conexión al servidor. Utiliza <see cref="ValidadorCorreoElectronico"/> para validar el correo.
/// </remarks>
public class ComandoEnviar : BaseCommand
{
    /// <summary>
    /// Propiedad privada del View Model de Olvidar Información
    /// </summary>
    private readonly OlvidarInformacionViewModel _vm;

    /// <summary>
    /// Propiedad privada de la configuración de la aplicación
    /// </summary>
    private readonly AppSettings _settings;

    /// <summary>
    /// Propiedad privada del Comprobador del Dominio
    /// </summary>
    private readonly ComprobadorDominio _comprobador;

    /// <summary>
    /// Propiedad privada del Validador del Correo Electronico.
    /// </summary>
    private readonly ValidadorCorreoElectronico _validadorCorreo;

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="ComandoEnviar"/> con las dependencias necesarias.
    /// </summary>
    /// <param name="vm">ViewModel de la pantalla de recuperación de información.</param>
    /// <param name="settings">Configuración de la aplicación.</param>
    /// <param name="comprobador">Comprobador de dominio para validaciones de correo electrónico.</param>
    public ComandoEnviar(OlvidarInformacionViewModel vm, AppSettings settings, ComprobadorDominio comprobador)
    {
        _vm = vm;
        _settings = settings;
        _comprobador = comprobador;
        _validadorCorreo = new(EnumOrigen.OlvidarInformacion, _settings, _comprobador);
    }

    /// <summary>
    /// Ejecuta la validación del correo electrónico y envía la petición al servidor.
    /// Muestra un popup con el resultado del proceso.
    /// </summary>
    /// <param name="parameter">Debe ser una instancia de <see cref="OlvidarInformacion"/> (la vista actual).</param>
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
                            _vm.Editable = true; // Así no puede editar el Entry

                            var popup = new PopupErrores(new PopupErroresViewModel(
                                    "El Código de Confirmación ha sido enviado a tu correo electrónico."
                                )
                            );

                            await olvidarInformacion.ShowPopupAsync(popup);
                        }
                        else
                        {
                            var popup = new PopupErrores(new PopupErroresViewModel(
                                    "Error al Recuperar Cuenta",
                                    "Ha ocurrido un error al enviar el correo."
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
                            "Ha ocurrido un error al intentar conectarse con el servidor."
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
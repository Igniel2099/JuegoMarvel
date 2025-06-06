using MensajesServidor;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace JuegoMarvel.ModuloLogin.Model;

/// <summary>
/// Valida formato, existencia y dominio de direcciones de correo electrónico.
/// </summary>
public class ValidadorCorreoElectronico(EnumOrigen origen, AppSettings configuracion, ComprobadorDominio validadorDominio)
{
    /// <summary>
    /// Propiedad privada que sirve para saber el Origen del mensaje.
    /// </summary>
    private readonly EnumOrigen _origen = origen;

    /// <summary>
    /// propiedad privada de las configuraciones de la aplicacion
    /// </summary>
    private readonly AppSettings _configuracion = configuracion;

    /// <summary>
    /// Propiedad privada de Comprobar de dominios
    /// </summary>
    private readonly ComprobadorDominio _validadorDominio = validadorDominio;

    /// <summary>
    /// Patron que debe seguir un Mail para que pueda validarlo.
    /// </summary>
    private const string PatronEmail = "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$";

    /// <summary>
    /// Ejecuta todas las comprobaciones para validar un email.
    /// </summary>
    public async Task<(bool EsValido, string? Mensaje)> ValidarAsync(string correo)
    {
        if (string.IsNullOrWhiteSpace(correo))
            return (false, "El campo Correo Electrónico no puede estar vacío.");

        if (!Regex.IsMatch(correo.Trim(), PatronEmail, RegexOptions.IgnoreCase))
            return (false, "Formato de correo inválido.");

        bool dominioOk = await _validadorDominio.ComprobarDominioCorreoElectronicoAsync(correo);
        if (!dominioOk)
            return (false, "El dominio del correo electrónico no es válido.");

        var (disponible, msgExistencia) = await ComprobarExistenciaAsync(correo);
        if (!disponible)
            return (false, msgExistencia);

        return (true, null);
    }

    /// <summary>
    /// Comprueba si el correo existe en la base de datos remota, lo envia al servidor, recibe la respueta y la crea si esta no existe.
    /// </summary>
    /// <param name="correo">El correo electronico a comprobar</param>
    /// <returns>Me devuelve un booleano de la comprobacion y un mensaje</returns>
    private async Task<(bool, string?)> ComprobarExistenciaAsync(string correo)
    {
        try
        {
            using var cliente = new TcpClient();
            await cliente.ConnectAsync(_configuracion.IpServidor, _configuracion.PuertoServidor);
            using var stream = cliente.GetStream();

            var mensaje = new MensajesModuloLogin(
                _origen,
                EnumTipoRespuesta.Comprobar,
                [ new Propiedad(EnumTipoValor.CorreoElectronico, correo) ],
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
                bool existe = respuestaServidor.Respuesta == EnumRespuesta.Existente;
                return
                    _origen == EnumOrigen.CrearCuenta
                        ? (!existe, existe ? "El correo electrónico ya existe en la Base de datos." : null)
                        : (existe, existe ? null : "El correo electrónico no existe en la Base de datos.")

                    ;
            }
        }
        catch (SocketException)
        {
            // Podrías loguear aquí si lo deseas
        }
        return (false, "Error comprobando el correo electrónico.");
    }
}

using MensajesServidor;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace JuegoMarvel.ModuloLogin.Model;

/// <summary>
/// Valida formato, existencia y dominio de direcciones de correo electrónico.
/// </summary>
public class ValidadorCorreoElectronico
{
    private readonly AppSettings _configuracion;
    private readonly ComprobadorDominio _validadorDominio;
    private const string PatronEmail = "^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$";

    public ValidadorCorreoElectronico(AppSettings configuracion, ComprobadorDominio validadorDominio)
    {
        _configuracion = configuracion;
        _validadorDominio = validadorDominio;
    }

    /// <summary>
    /// Ejecuta todas las comprobaciones para validar un email.
    /// </summary>
    public async Task<(bool EsValido, string? Mensaje)> ValidarAsync(string correo)
    {
        if (string.IsNullOrWhiteSpace(correo))
            return (false, "El campo Correo Electrónico no puede estar vacío.");

        if (!Regex.IsMatch(correo.Trim(), PatronEmail, RegexOptions.IgnoreCase))
            return (false, "Formato de correo inválido.");

        var (disponible, msgExistencia) = await ComprobarExistenciaAsync(correo);
        if (!disponible)
            return (false, msgExistencia);

        bool dominioOk = await _validadorDominio.ComprobarDominioCorreoElectronicoAsync(correo);
        if (!dominioOk)
            return (false, "El dominio del correo electrónico no es válido.");

        return (true, null);
    }

    private async Task<(bool, string?)> ComprobarExistenciaAsync(string correo)
    {
        try
        {
            using var cliente = new TcpClient();
            await cliente.ConnectAsync(_configuracion.IpServidor, _configuracion.PuertoServidor);
            using var stream = cliente.GetStream();

            var mensaje = new MensajesModuloLogin(
                EnumOrigen.CrearCuenta,
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
                return (!existe, existe ? "El correo electrónico ya existe." : null);
            }
        }
        catch (SocketException)
        {
            // Podrías loguear aquí si lo deseas
        }
        return (false, "Error comprobando el correo electrónico.");
    }
}

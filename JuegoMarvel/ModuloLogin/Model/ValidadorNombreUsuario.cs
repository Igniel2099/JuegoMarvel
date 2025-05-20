using MensajesServidor;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json;
using System.Net.Sockets;
using System.Text;

namespace JuegoMarvel.ModuloLogin.Model;

/// <summary>
/// Valida la disponibilidad y formato del nombre de usuario en el servidor.
/// </summary>
public class ValidadorNombreUsuario(AppSettings configuracion)
{
    private readonly AppSettings _configuracion = configuracion;

    /// <summary>
    /// Verifica que el nombre de usuario no esté vacío y consulte al servidor.
    /// </summary>
    public async Task<(bool EsValido, string? Mensaje)> ValidarAsync(string nombreUsuario)
    {
        if (string.IsNullOrWhiteSpace(nombreUsuario))
            return (false, "El campo Nombre de Usuario no puede estar vacío.");

        return await ComprobarExistenciaAsync(nombreUsuario);
    }

    private async Task<(bool, string?)> ComprobarExistenciaAsync(string nombreUsuario)
    {
        try
        {
            using var cliente = new TcpClient();
            await cliente.ConnectAsync(_configuracion.IpServidor, _configuracion.PuertoServidor);
            using var stream = cliente.GetStream();

            var mensaje = new MensajesModuloLogin(
                EnumOrigen.CrearCuenta,
                EnumTipoRespuesta.Comprobar,
                [new Propiedad(EnumTipoValor.NombreUsuario, nombreUsuario)],
                null);

            var ajustesJson = new JsonSerializerSettings();
            ajustesJson.Converters.Add(new StringEnumConverter());

            string contenido = JsonConvert.SerializeObject(mensaje, Formatting.Indented, ajustesJson);
            byte[] datosEnvio = Encoding.UTF8.GetBytes(contenido);
            await stream.WriteAsync(datosEnvio, 0, datosEnvio.Length);

            byte[] buffer = new byte[1024];
            int leidos = await stream.ReadAsync(buffer);
            string respuestaJson = Encoding.UTF8.GetString(buffer, 0, leidos);

            var respuestaServidor = JsonConvert.DeserializeObject<MensajesModuloLogin?>(respuestaJson, ajustesJson);
            if (respuestaServidor?.Respuesta is not null)
            {
                bool existe = respuestaServidor.Respuesta == EnumRespuesta.Existente;
                return (!existe, existe ? "El nombre de usuario ya existe." : null);
            }
        }
        catch (SocketException)
        {
            // Podrías loguear aquí si lo deseas
        }
        return (false, "Error al comprobar el nombre de usuario.");
    }
}
using JuegoMarvel.ModuloJuego.ViewModel;
using MensajesServidor;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Text.Json;

namespace JuegoMarvel.ModuloJuego.Model;

public class ClienteJuego
{
    private readonly string host;
    private readonly int puerto;
    private TcpClient client;
    private NetworkStream stream;

    public ClienteJuego(string host, int puerto)
    {
        this.host = host;
        this.puerto = puerto;
    }

    public void Conectar()
    {
        client = new TcpClient(host, puerto);
        stream = client.GetStream();
        Debug.WriteLine($"Conectado a {host}:{puerto}");
    }

    public async Task Enviar(MensajesModuloJuego msg)
    {
        var json = JsonSerializer.Serialize(msg);
        var data = Encoding.UTF8.GetBytes(json);
        var prefix = BitConverter.GetBytes(data.Length);
        await stream.WriteAsync(prefix, 0, prefix.Length);
        await stream.WriteAsync(data, 0, data.Length);
    }

    public async Task<MensajesModuloJuego> Recibir()
    {
        var lenBuf = new byte[4];
        await stream.ReadAsync(lenBuf, 0, 4);
        int len = BitConverter.ToInt32(lenBuf, 0);
        var buf = new byte[len];
        int m = 0;
        while (m < len) m += await stream.ReadAsync(buf, m, len - m);
        return JsonSerializer.Deserialize<MensajesModuloJuego>(Encoding.UTF8.GetString(buf));
    }

    public void Cerrar()
    {
        stream?.Close();
        client?.Close();
    }

   
}

using JuegoMarvel.ModuloLogin.Model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace JuegoMarvel.ModuloLogin.ViewModel.Comandos;

public class ComandoLogearse : BaseCommand
{
    public string _nombre = string.Empty;
    public string _contrasena = string.Empty;

    public override async void Execute(object? parameter)
    {

        Debug.WriteLine("==================================" + _nombre + ", " + _contrasena + "==================================");


        try
        {
            using TcpClient cliente = new();
            await cliente.ConnectAsync("192.168.1.13", 5000); // IP y puerto del servidor

            using NetworkStream stream = cliente.GetStream();

            string mensajeJson = JsonConvert.SerializeObject(new Usuario { NombreUsuario = _nombre, Contrasena = _contrasena });
            
            byte[] datos = Encoding.UTF8.GetBytes(mensajeJson);
            await stream.WriteAsync(datos, 0, datos.Length);

            // Leer respuesta
            byte[] buffer = new byte[1024];
            int bytesLeidos = await stream.ReadAsync(buffer, 0, buffer.Length);
            string respuesta = Encoding.UTF8.GetString(buffer, 0, bytesLeidos);

            string mensajeServidor = $"Servidor dijo: {respuesta}";
            Debug.WriteLine("==================================" + mensajeServidor + "==================================");
        }
        catch (Exception ex)
        {
            Debug.WriteLine("==================================" + $"Error: {ex.Message}" + "==================================");
        }
    }
  

}
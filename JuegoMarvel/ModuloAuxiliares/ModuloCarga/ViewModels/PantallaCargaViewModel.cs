using CommunityToolkit.Maui.Views;
using JuegoMarvel.ModuloInicio.ViewModel;
using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.View;
using JuegoMarvel.ModuloLogin.ViewModel;
using JuegoMarvel.Services;
using JuegoMarvel.Views;
using JuegoMarvelData.Data;
using JuegoMarvelData.Models;
using MensajesServidor;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace JuegoMarvel.ModuloAuxiliares.ModuloCarga.ViewModels;

public class PantallaCargaViewModel : BaseViewModel
{
    private readonly AppSettings _configuracion;
    private readonly BbddjuegoMarvelContext _context;
    private readonly string _nombreUsuario;

    private double _progreso;

    public double Progreso
    {
        get => _progreso;
        set
        {
            if (_progreso == value) return;
            _progreso = value;
            OnPropertyChanged();
        }
    }

    public PantallaCargaViewModel(AppSettings configuracion, BbddjuegoMarvelContext context, string nombreUsuario)
    {
        _progreso = 0.0;
        _nombreUsuario = nombreUsuario;
        _context = context;
        _configuracion = configuracion;
    }

    public async Task CargarDatosAsync()
    {
        try
        {
            Thread.Sleep(3000);
            using var cliente = new TcpClient();
            await cliente.ConnectAsync(_configuracion.IpServidor, _configuracion.PuertoServidor);

            using var stream = cliente.GetStream();

            var mensaje = new MensajesModuloLogin(
                EnumOrigen.RecuperarDatos,
                EnumTipoRespuesta.Recuperar,
                [new Propiedad(EnumTipoValor.NombreUsuario, _nombreUsuario.Trim())],
                null
            );

            var ajustesJson = new JsonSerializerSettings();
            ajustesJson.Converters.Add(new StringEnumConverter());

            string contenido = JsonConvert.SerializeObject(mensaje, Formatting.Indented, ajustesJson);
            byte[] datosEnvio = Encoding.UTF8.GetBytes(contenido);
            await stream.WriteAsync(datosEnvio);

            string respuestaJson = await RecibirRespuestaServidor(stream);

            var respuestaServidor = JsonConvert.DeserializeObject<MensajesModuloLogin?>(
                respuestaJson,
                ajustesJson
            ) ?? throw new Exception("No se pudo deserializar la respuesta del servidor.");

            if (respuestaServidor.Respuesta == EnumRespuesta.Error)
            {
                var mainPage = Application.Current!.Windows[0].Page;

                var popup = new PopupErrores(
                   new PopupErroresViewModel(
                       "ERROR AL CARGAR CONTENIDO",
                       "Ha ocurrido un error con el usuario."
                   )
               );
                await mainPage!.ShowPopupAsync(popup);

                // Volver a la pantalla de login.
                await mainPage!
                   .Navigation
                   .PopModalAsync(false);
            }

            if (respuestaServidor.Respuesta != EnumRespuesta.Recuperando)
                throw new Exception("La respuesta del servidor no es la esperada.");

            
            Progreso = 0.5; // Simula un progreso del 50% al recibir la respuesta inicial.

            string respuestaJsonDatosUSuario = await RecibirRespuestaServidor(stream);
            Debug.WriteLine($"Respuesta del servidor: {respuestaJsonDatosUSuario}");
            MandarDatosUsuario? mandarDatosUsuario = 
                    JsonConvert.DeserializeObject<MandarDatosUsuario?>(respuestaJsonDatosUSuario);
            // Procesar los datos del usuario.
            Usuario usuario = ConversorDatosUsuario
                .MapUsuario(mandarDatosUsuario);
            _context.Add(usuario); 

            if (mandarDatosUsuario.Personajes.Count > 0)
            {
                foreach (var personajeDto in mandarDatosUsuario.Personajes)
                {
                    Personaje personaje = ConversorDatosUsuario.MapPersonaje(personajeDto);
                    _context.Add(personaje);
                }
            }

            if (mandarDatosUsuario.Habilidades.Count > 0)
            {
                foreach (var habilidadeDto in mandarDatosUsuario.Habilidades)
                 {
                    Habilidade habilidade = ConversorDatosUsuario.MapHabilidade(habilidadeDto);
                    _context.Add(habilidade);
                }
            }

            if (mandarDatosUsuario.PersonajeUsuarios.Count > 0)
            {
                foreach (var personajeUsuarioDto in mandarDatosUsuario.PersonajeUsuarios)
                {
                    PersonajeUsuario personajeUsuario = ConversorDatosUsuario.MapPersonajeUsuario(personajeUsuarioDto);
                    _context.Add(personajeUsuario);
                }
            }

            if (mandarDatosUsuario.Equipo != null)
            {
                Equipo equipo = ConversorDatosUsuario.MapEquipo(mandarDatosUsuario.Equipo);
                _context.Add(equipo);
            }

            foreach (var peleaDTO in mandarDatosUsuario.Peleas)
            {
                Pelea pelea = ConversorDatosUsuario.MapPelea(peleaDTO);
                _context.Add(pelea);
            }

            _context.SaveChanges(); // Opcional de momento

            Progreso = 0.6;
            Progreso = 0.7;
            Progreso = 0.8;
            Thread.Sleep(1000);
            Progreso = 0.9;
            Progreso = 1;

            // Cambiar de pantalla a la pantalla de inicio.
            var window = Application.Current.Windows[0]; // para apps de una sola ventana
            var nav = window.Page.Navigation;

            // Para hacer el PushModal sin animación nativa:
            await nav.PushModalAsync(new Inicio(new InicioViewModel(_configuracion, _context)), false);
        }
        catch (SocketException ex)
        {
            Debug.WriteLine($"Error de conexión: {ex.Message}");
        } 
    }

    private async static Task<string> RecibirRespuestaServidor(Stream stream)
    {
        using MemoryStream lector = new();
        byte[] buffer = new byte[1024];

        int bytesRead = 0;
        string? contenidoLeido = null;
        while (true)
        {
            int leidos = await stream.ReadAsync(buffer);

            lector.Write(buffer, 0, leidos);
            bytesRead += leidos;

            // Convertimos el contenido leído hasta ahora a string
            contenidoLeido = Encoding.UTF8.GetString(lector.ToArray());

            if (contenidoLeido.Contains("\r\n")) break;
        }

        if (contenidoLeido == null) 
            throw new Exception("No he liedo nada del mensaje del servidor");

        return contenidoLeido;
    }
}

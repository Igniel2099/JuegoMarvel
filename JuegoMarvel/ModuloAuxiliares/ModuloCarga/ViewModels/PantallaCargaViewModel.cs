using CommunityToolkit.Maui.Views;
using JuegoMarvel.ClasesBase;
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

/// <summary>
/// ViewModel para la pantalla de carga de datos del usuario.
/// Gestiona el proceso de recuperación de datos desde el servidor y el progreso de la carga.
/// </summary>
public class PantallaCargaViewModel : BaseViewModel
{
    /// <summary>
    /// Propiedad privada que obtiene las configuraciones de la aplicación
    /// </summary>
    private readonly AppSettings _configuracion;

    /// <summary>
    /// Propiedad privada DbContext para poder contectarse a la base de datos local
    /// </summary>
    private readonly BbddjuegoMarvelContext _context;

    /// <summary>
    /// Propiedad privada Nombre del Usuario del que quiero cargar datos.
    /// </summary>
    private readonly string _nombreUsuario;

    /// <summary>
    /// Propiedad privada que indica el progreso de la barra
    /// </summary>
    private double _progreso;

    /// <summary>
    /// Progreso de la carga de datos, valor entre 0 y 1.
    /// </summary>
    public double Progreso
    {
        get => _progreso;
        set
        {
            if (_progreso.Equals(value)) return;
            _progreso = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="PantallaCargaViewModel"/>.
    /// Inicializa el progreso en 0 y las demas propiedades.
    /// </summary>
    /// <param name="configuracion">Configuración de la aplicación.</param>
    /// <param name="context">Contexto de la base de datos.</param>
    /// <param name="nombreUsuario">Nombre de usuario para recuperar los datos.</param>
    public PantallaCargaViewModel(AppSettings configuracion, BbddjuegoMarvelContext context, string nombreUsuario)
    {
        _progreso = 0.0;
        _nombreUsuario = nombreUsuario;
        _context = context;
        _configuracion = configuracion;
    }

    private MensajesModuloLogin PrepararMensaje() => new(
        EnumOrigen.RecuperarDatos,
        EnumTipoRespuesta.Recuperar,
        [new Propiedad(EnumTipoValor.NombreUsuario, _nombreUsuario.Trim())],
        null
    );

    /// <summary>
    /// Escibre el mensaje a mandar en formato json y lo envia a traves del stream.
    /// </summary>
    /// <param name="stream">Stream con el que puedo enviar mensajes al servidor</param>
    /// <param name="mensaje">Mensaje que le quiero enviar al Servidor</param>
    /// <param name="ajusteJson">Ajustes del Json para Serializar correctamente</param>
    /// <returns>Me avisa de que el método ha terminado su proceso.</returns>
    private static async Task EscribirMensaje(Stream stream, MensajesModuloLogin mensaje, JsonSerializerSettings ajusteJson)
    {
        string contenido = JsonConvert.SerializeObject(mensaje, Formatting.None, ajusteJson);
        byte[] datosEnvio = Encoding.UTF8.GetBytes(contenido);
        await stream.WriteAsync(datosEnvio);
    }

    /// <summary>
    /// Procesa los datos del usuario mandados desde el servidor y los carga en la base de datos local
    /// </summary>
    /// <param name="mandarDatosUsuario">Objeto que contiene toda la información del usurio</param>
    /// <returns>Me avisa de que el método ha terminado su proceso.</returns>
    private async Task ProcesarDatosUsuario(MandarDatosUsuario mandarDatosUsuario)
    {
        Usuario usuario = ConversorDatosUsuario
                .MapUsuario(mandarDatosUsuario);
        await _context.AddAsync(usuario);

        if (mandarDatosUsuario.Personajes.Count > 0)
        {
            foreach (var personajeDto in mandarDatosUsuario.Personajes)
            {
                Personaje personaje = ConversorDatosUsuario.MapPersonaje(personajeDto);
                await _context.AddAsync(personaje);
            }
        }

        if (mandarDatosUsuario.Habilidades.Count > 0)
        {
            foreach (var habilidadeDto in mandarDatosUsuario.Habilidades)
            {
                Habilidade habilidade = ConversorDatosUsuario.MapHabilidade(habilidadeDto);
                await _context.AddAsync(habilidade);
            }
        }

        if (mandarDatosUsuario.PersonajeUsuarios.Count > 0)
        {
            foreach (var personajeUsuarioDto in mandarDatosUsuario.PersonajeUsuarios)
            {
                PersonajeUsuario personajeUsuario = ConversorDatosUsuario.MapPersonajeUsuario(personajeUsuarioDto);
                await _context.AddAsync(personajeUsuario);
            }
        }

        if (mandarDatosUsuario.Equipo != null)
        {
            Equipo equipo = ConversorDatosUsuario.MapEquipo(mandarDatosUsuario.Equipo);
            await _context.AddAsync(equipo);
        }

        foreach (var peleaDTO in mandarDatosUsuario.Peleas)
        {
            Pelea pelea = ConversorDatosUsuario.MapPelea(peleaDTO);
            await _context.AddAsync(pelea);
        }
    }

    /// <summary>
    /// Realiza la carga de datos del usuario desde el servidor y los guarda en la base de datos local.
    /// Actualiza el progreso durante el proceso.
    /// </summary>
    public async Task CargarDatosAsync()
    {
        try
        {
            Thread.Sleep(3000);
            using var cliente = new TcpClient();
            await cliente.ConnectAsync(_configuracion.IpServidor, _configuracion.PuertoServidor);

            using var stream = cliente.GetStream();

            JsonSerializerSettings ajustesJson = new ();
            ajustesJson.Converters.Add(new StringEnumConverter());

            await EscribirMensaje(stream, PrepararMensaje(), ajustesJson);

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
            MandarDatosUsuario mandarDatosUsuario =
                    JsonConvert.DeserializeObject<MandarDatosUsuario?>(respuestaJsonDatosUSuario) 
                    ?? throw new Exception("Los datos del usuario mandados desde el servidor son nulos o se ha vuelto null al deserializar.");

            await ProcesarDatosUsuario(mandarDatosUsuario);

            await _context.SaveChangesAsync();

            Progreso = 0.6;
            Thread.Sleep(10);
            Progreso = 0.7;
            Thread.Sleep(10);
            Progreso = 0.8;
            Thread.Sleep(1000);
            Progreso = 0.9;
            Thread.Sleep(10);
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
        catch (JsonException)
        {
            await Application.Current.MainPage
                .Navigation
                .PopModalAsync(false);
        }
    }

    /// <summary>
    /// Recibe la respuesta del servidor a través del stream proporcionado.
    /// </summary>
    /// <param name="stream">Stream de red para leer la respuesta.</param>
    /// <returns>Cadena JSON recibida del servidor.</returns>
    /// <exception cref="Exception">Se lanza si no se recibe ningún mensaje del servidor.</exception>
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
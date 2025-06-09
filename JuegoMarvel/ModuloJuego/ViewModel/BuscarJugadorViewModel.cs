using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloTienda.Model;
using JuegoMarvel.Services;
using JuegoMarvelData.Data;
using System.Windows.Input;

namespace JuegoMarvel.ModuloJuego.ViewModel;

/// <summary>
/// ViewModel para la pantalla de búsqueda de jugador.
/// Gestiona los datos visuales y la carga de información de los personajes del equipo.
/// </summary>
public class BuscarJugadorViewModel : BaseViewModel
{
    #region CamposViewModel

    /// <summary>
    /// Propiedad privada del Nombre
    /// </summary>
    private string _nombre;

    /// <summary>
    /// Nombre del usuario actual.
    /// </summary>
    public string Nombre
    {
        get => _nombre;
        set
        {
            if (_nombre != value)
            {
                _nombre = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Propiedad privada de la imagen del primer Personaje
    /// </summary>
    private string _imgCuerpoJugadorUno;

    /// <summary>
    /// Imagen del cuerpo del primer personaje del equipo.
    /// </summary>
    public string ImgCuerpoJugadorUno
    {
        get => _imgCuerpoJugadorUno;
        set
        {
            if (_imgCuerpoJugadorUno == value) return;
            _imgCuerpoJugadorUno = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad privada de la imagen del segundo Personaje
    /// </summary>
    private string _imgCuerpoJugadorDos;

    /// <summary>
    /// Imagen del cuerpo del segundo personaje del equipo.
    /// </summary>
    public string ImgCuerpoJugadorDos
    {
        get => _imgCuerpoJugadorDos;
        set
        {
            if (_imgCuerpoJugadorDos == value) return;
            _imgCuerpoJugadorDos = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad privada de la imagen del tercer Personaje.
    /// </summary>
    private string _imgCuerpoJugadorTres;

    /// <summary>
    /// Imagen del cuerpo del tercer personaje del equipo.
    /// </summary>
    public string ImgCuerpoJugadorTres
    {
        get => _imgCuerpoJugadorTres;
        set
        {
            if (_imgCuerpoJugadorTres == value) return;
            _imgCuerpoJugadorTres = value;
            OnPropertyChanged();
        }
    }
    #endregion

    public string NombrePersonajeUno { get; set; }
    public string NombrePersonajeDos { get; set; }
    public string NombrePersonajeTres { get; set; }

    public PersonajesImagenes PersonajesImgs { get; set; }

    /// <summary>
    /// Comando para navegar hacia atrás en la navegación.
    /// </summary>
    public ComandoNavegarVolverAtras NavAtras { get; set; }
    public ICommand ComandoInicializarConexion { get; set; }

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="BuscarJugadorViewModel"/> con valores por defecto.
    /// </summary>
    public BuscarJugadorViewModel()
    {
        _nombre = string.Empty;
        _imgCuerpoJugadorUno = string.Empty;
        _imgCuerpoJugadorDos = string.Empty;
        _imgCuerpoJugadorTres = string.Empty;
        NavAtras = new();
    }

    /// <summary>
    /// Carga los datos del usuario y las imágenes de los personajes del equipo desde la base de datos.
    /// </summary>
    /// <param name="context">Contexto de la base de datos.</param>
    public async Task CargarDatos(BbddjuegoMarvelContext context)
    {
        Nombre = context.Usuarios.FirstOrDefault()!.NombreUsuario;

        GestionPersonajes gestionPersonajes = new(context);

        PersonajesImgs = await GestionPersonajes.CargarPersonajesJsonAsync();

        var listaNombresPersonajesUsuarioEquipo = await gestionPersonajes.CargarNombresEquipoPersonajesUsuario();
        if (listaNombresPersonajesUsuarioEquipo != null)
        {
            for (int i = 0; i < listaNombresPersonajesUsuarioEquipo.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        NombrePersonajeUno = listaNombresPersonajesUsuarioEquipo[i];
                        ImgCuerpoJugadorUno = PersonajesImgs[NombrePersonajeUno].ImgCuerpo;
                        continue;
                    case 1:
                        NombrePersonajeDos = listaNombresPersonajesUsuarioEquipo[i];
                        ImgCuerpoJugadorDos = PersonajesImgs[NombrePersonajeDos].ImgCuerpo;
                        continue;
                    case 2:
                        NombrePersonajeTres = listaNombresPersonajesUsuarioEquipo[i];
                        ImgCuerpoJugadorTres = PersonajesImgs[NombrePersonajeTres].ImgCuerpo;
                        break;
                }
            }
        }
    }
}
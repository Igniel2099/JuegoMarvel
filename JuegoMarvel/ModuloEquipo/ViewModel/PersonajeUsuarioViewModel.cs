using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloEquipo.ViewModel.Comandos;

namespace JuegoMarvel.ModuloEquipo.ViewModel;

/// <summary>
/// ViewModel que representa un personaje de usuario en el equipo.
/// Gestiona las propiedades visuales y de datos del personaje, así como el comando de selección.
/// </summary>
public class PersonajeUsuarioViewModel : BaseViewModel
{
    #region CamposViewModel
    /// <summary>
    /// Propiedad privada de la Imagen del Contenedor
    /// </summary>
    private string _imgContenedor;
    /// <summary>
    /// Imagen de fondo del contenedor del personaje.
    /// </summary>
    public string ImagenContenedor
    {
        get => _imgContenedor;
        set
        {
            if (value == _imgContenedor) return;
            _imgContenedor = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad Privada del nombre
    /// </summary>
    private string _nombre;
    /// <summary>
    /// Nombre del personaje.
    /// </summary>
    public string Nombre
    {
        get => _nombre;
        set
        {
            if (value == _nombre) return;
            _nombre = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad privada del tipo de la habilidad
    /// </summary>
    private string _tipoUno;
    /// <summary>
    /// Primer tipo de la habilidad del personaje.
    /// </summary>
    public string TipoUno
    {
        get => _tipoUno;
        set
        {
            if (value == _tipoUno) return;
            _tipoUno = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad privada del tipo de la habilidad
    /// </summary>
    private string _tipoDos;
    /// <summary>
    /// Segundo tipo o habilidad del personaje.
    /// </summary>
    public string TipoDos
    {
        get => _tipoDos;
        set
        {
            if (value == _tipoDos) return;
            _tipoDos = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad privada del tipo de la habilidad
    /// </summary>
    private string _tipoTres;
    /// <summary>
    /// Tercer tipo o habilidad del personaje.
    /// </summary>
    public string TipoTres
    {
        get => _tipoTres;
        set
        {
            if (value == _tipoTres) return;
            _tipoTres = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad privada de la imagen del cuerpo del personaje
    /// </summary>
    private string _imgCuerpo;
    /// <summary>
    /// Imagen principal del cuerpo del personaje.
    /// </summary>
    public string ImgCuerpo
    {
        get => _imgCuerpo;
        set
        {
            if (value == _imgCuerpo) return;
            _imgCuerpo = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad privada de la imagen de la primera habilidad
    /// </summary>
    private string _imgHabilidadUno;
    /// <summary>
    /// Imagen de la primera habilidad del personaje.
    /// </summary>
    public string ImgHabilidadUno
    {
        get => _imgHabilidadUno;
        set
        {
            if (value == _imgHabilidadUno) return;
            _imgHabilidadUno = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad privada de la imagen de la Segunda habilidad
    /// </summary>
    private string _imgHabilidadDos;
    /// <summary>
    /// Imagen de la segunda habilidad del personaje.
    /// </summary>
    public string ImgHabilidadDos
    {
        get => _imgHabilidadDos;
        set
        {
            if (value == _imgHabilidadDos) return;
            _imgHabilidadDos = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad privada de la imagen de la tercera habilidad
    /// </summary>
    private string _imgHabilidadTres;
    /// <summary>
    /// Imagen de la tercera habilidad del personaje.
    /// </summary>
    public string ImgHabilidadTres
    {
        get => _imgHabilidadTres;
        set
        {
            if (value == _imgHabilidadTres) return;
            _imgHabilidadTres = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region Auxiliares

    /// <summary>
    /// Identificador único del personaje de usuario.
    /// </summary>
    public readonly int IdPersonajeUsuario;

    /// <summary>
    /// Comando para seleccionar este personaje en la vista de equipo.
    /// </summary>
    public ComandoSeleccionarPersonaje ComandoSeleccionarPersonaje { get; set; }
    #endregion

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="PersonajeUsuarioViewModel"/> con los datos y comandos necesarios.
    /// inicializa la imagen del contenedor con un valor por defecto, crea el comando e inicializa los demas campos.
    /// </summary>
    /// <param name="idPersonajeUsuario">Identificador del personaje de usuario.</param>
    /// <param name="vm">ViewModel del equipo asociado.</param>
    /// <param name="nombre">Nombre del personaje.</param>
    /// <param name="tipoUno">Primer tipo o habilidad.</param>
    /// <param name="tipoDos">Segundo tipo o habilidad.</param>
    /// <param name="tipoTres">Tercer tipo o habilidad.</param>
    /// <param name="imgCuerpo">Imagen principal del cuerpo.</param>
    /// <param name="imgHabilidadUno">Imagen de la primera habilidad.</param>
    /// <param name="imgHabilidadDos">Imagen de la segunda habilidad.</param>
    /// <param name="imgHabilidadTres">Imagen de la tercera habilidad.</param>
    public PersonajeUsuarioViewModel(
        int idPersonajeUsuario,
        EquipoViewModel vm,
        string nombre,
        string tipoUno,
        string tipoDos,
        string tipoTres,
        string imgCuerpo,
        string imgHabilidadUno,
        string imgHabilidadDos,
        string imgHabilidadTres)
    {
        // info
        IdPersonajeUsuario = idPersonajeUsuario;
        // Comandos
        ComandoSeleccionarPersonaje = new(vm, this);

        // Visual
        _imgContenedor = "fondo_contenedor_equipo.png";
        _nombre = nombre;
        _tipoUno = tipoUno;
        _tipoDos = tipoDos;
        _tipoTres = tipoTres;
        _imgCuerpo = imgCuerpo;
        _imgHabilidadUno = imgHabilidadUno;
        _imgHabilidadDos = imgHabilidadDos;
        _imgHabilidadTres = imgHabilidadTres;
    }
}
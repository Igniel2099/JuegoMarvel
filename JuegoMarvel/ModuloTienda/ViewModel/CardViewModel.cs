using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloTienda.Model;
using JuegoMarvel.ModuloTienda.ViewModel.Comandos;

namespace JuegoMarvel.ModuloTienda.ViewModel;

/// <summary>
/// ViewModel que representa la tarjeta visual de un personaje en la tienda.
/// Expone las propiedades visuales y de negocio necesarias para mostrar la información del personaje,
/// así como los comandos para mostrar información detallada y gestionar la compra.
/// </summary>
public class CardViewModel : BaseViewModel
{
    #region CamposViewModel

    /// <summary>
    /// Propiedad privada del nombre
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
    /// Propiedad privada del Tipo del Personaje
    /// </summary>
    private string _tipo;

    /// <summary>
    /// Tipo o clase del personaje.
    /// </summary>
    public string Tipo
    {
        get => _tipo;
        set
        {
            if (value == _tipo) return;
            _tipo = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad privada del Grupo del Personaje
    /// </summary>
    private string _grupo;

    /// <summary>
    /// Grupo o afiliación del personaje.
    /// </summary>
    public string Grupo
    {
        get => _grupo;
        set
        {
            if (value == _grupo) return;
            _grupo = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad privada del costo del PErsoanje
    /// </summary>
    private int _costo;

    /// <summary>
    /// Costo en monedas para comprar el personaje.
    /// </summary>
    public int Costo
    {
        get => _costo;
        set
        {
            if (value == _costo) return;
            _costo = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad privada de la Imagen Principal
    /// </summary>
    private string _imgPrincipal;

    /// <summary>
    /// Ruta de la imagen principal del personaje.
    /// </summary>
    public string ImgPrincipal
    {
        get => _imgPrincipal;
        set
        {
            if (value == _imgPrincipal) return;
            _imgPrincipal = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad privada de la imagen de la primera habilidad del personaje
    /// </summary>
    private string _imgHabilidadUno;
    /// <summary>
    /// Ruta de la imagen de la primera habilidad del personaje.
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
    /// Propiedad privada de la imagen de la segunda habilidad del personaje
    /// </summary>
    private string _imgHabilidadDos;
    /// <summary>
    /// Ruta de la imagen de la segunda habilidad del personaje.
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
    /// Propiedad privada de la imagen de la tercera habilidad del personaje
    /// </summary>
    private string _imgHabilidadTres;
    /// <summary>
    /// Ruta de la imagen de la tercera habilidad del personaje.
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

    #region Comandos

    /// <summary>
    /// Comando para mostrar la información detallada del personaje en un popup.
    /// </summary>
    public MostrarInformacionCommand MostrarInformacionCommand { get; set; }

    /// <summary>
    /// Comando para mostrar el popup de compra del personaje.
    /// </summary>
    public ComandoNavCompraPopup NavCompraPopup { get; set; }
    #endregion

    #region Auxiliares

    /// <summary>
    /// Identificador único del personaje.
    /// </summary>
    public int IdPersonaje { get; set; }

    /// <summary>
    /// Evento que se dispara cuando se realiza la compra del personaje.
    /// </summary>
    public event EventHandler<int> AvisarIdCompradol;
    #endregion

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="CardViewModel"/> con los datos del personaje y sus imágenes.
    /// </summary>
    /// <param name="id">Identificador del personaje.</param>
    /// <param name="nombre">Nombre del personaje.</param>
    /// <param name="tipo">Tipo o clase del personaje.</param>
    /// <param name="grupo">Grupo o afiliación del personaje.</param>
    /// <param name="costo">Costo en monedas para comprar el personaje.</param>
    /// <param name="personajeImg">Modelo gráfico con las imágenes del personaje y sus habilidades.</param>
    public CardViewModel(
        int id,
        string nombre,
        string tipo,
        string grupo,
        int costo,
        PersonajeImg personajeImg)
    {
        IdPersonaje = id;
        _nombre = nombre;
        _tipo = tipo;
        _grupo = grupo;
        _costo = costo;

        _imgPrincipal = personajeImg.ImgPrincipal;
        _imgHabilidadUno = personajeImg.Habilidades[0].Casillas.Original;
        _imgHabilidadDos = personajeImg.Habilidades[1].Casillas.Original;
        _imgHabilidadTres = personajeImg.Habilidades[2].Casillas.Original;
    }

    /// <summary>
    /// Lanza el evento <see cref="AvisarIdCompradol"/> para notificar que el personaje ha sido comprado.
    /// </summary>
    /// <param name="sender">Origen del evento.</param>
    /// <param name="e">Identificador del personaje comprado.</param>
    public void OnAvisarIdComprado(object? sender, int e)
    {
        AvisarIdCompradol.Invoke(this, e);
    }
}
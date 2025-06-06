using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloTienda.ViewModel.Comandos;
using JuegoMarvelData.Data;

namespace JuegoMarvel.ModuloTienda.ViewModel;

/// <summary>
/// ViewModel para gestionar la lógica y el estado de la ventana de compra de un personaje en la tienda.
/// Expone las propiedades necesarias para mostrar la información del personaje, el comando de compra y un evento para notificar la compra.
/// </summary>
public class CompraViewModel : BaseViewModel
{
    #region CamposViewModel

    /// <summary>
    /// Propiedad privada del Id del personaje
    /// </summary>
    private int _idPersonaje;
    /// <summary>
    /// Identificador único del personaje a comprar.
    /// </summary>
    public int IdPersonaje
    {
        get => _idPersonaje;
        set
        {
            if (_idPersonaje == value) return;
            _idPersonaje = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad privada del nombre
    /// </summary>
    private string _nombre;
    /// <summary>
    /// Nombre del personaje a comprar.
    /// </summary>
    public string Nombre
    {
        get => _nombre;
        set
        {
            if (_nombre == value) return;
            _nombre = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad privada de la Imagen del Cuerpo
    /// </summary>
    private string _imgCuerpo;

    /// <summary>
    /// Ruta de la imagen del cuerpo del personaje.
    /// </summary>
    public string ImgCuerpo
    {
        get => _imgCuerpo;
        set
        {
            if (_imgCuerpo == value) return;
            _imgCuerpo = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad privada del costo
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
            if (_costo == value) return;
            _costo = value;
            OnPropertyChanged();
        }
    }
    #endregion

    /// <summary>
    /// Comando para ejecutar la compra del personaje.
    /// </summary>
    public ComandoComprar ComandoComprar { get; set; }

    /// <summary>
    /// Evento que se dispara cuando se realiza la compra del personaje.
    /// </summary>
    public event EventHandler<int> AvisarIdCompradol;

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="CompraViewModel"/> con los datos del personaje y el contexto de base de datos.
    /// </summary>
    /// <param name="context">Contexto de la base de datos.</param>
    /// <param name="idPersonaje">Identificador del personaje.</param>
    /// <param name="nombre">Nombre del personaje.</param>
    /// <param name="imgCuerpo">Ruta de la imagen del cuerpo del personaje.</param>
    /// <param name="costo">Costo en monedas para comprar el personaje.</param>
    public CompraViewModel(BbddjuegoMarvelContext context, int idPersonaje, string nombre, string imgCuerpo, int costo)
    {
        _idPersonaje = idPersonaje;
        _nombre = nombre;
        _imgCuerpo = imgCuerpo;
        _costo = costo;
        ComandoComprar = new(this, context);
    }

    /// <summary>
    /// Lanza el evento <see cref="AvisarIdCompradol"/> para notificar que el personaje ha sido comprado.
    /// </summary>
    public void OnAvisarIdComprado()
    {
        AvisarIdCompradol.Invoke(null, IdPersonaje);
    }
}
using JuegoMarvel.ModuloTienda.ViewModel.Comandos;
using JuegoMarvelData.Data;

namespace JuegoMarvel.ModuloTienda.ViewModel;

public class CompraViewModel : BaseViewModel
{
    private int _idPersonaje;
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

    private string _nombre;
    public string Nombre
    {
        get => _nombre;
        set
        {
            if (_nombre== value) return;
            _nombre= value;
            OnPropertyChanged();
        }
    }

    private string _imgCuerpo;
    public string ImgCuerpo
    {
        get => _imgCuerpo;
        set
        {
            if (_imgCuerpo== value) return;
            _imgCuerpo= value;
            OnPropertyChanged();
        }
    }

    private int _costo;
    public int Costo
    {
        get => _costo;
        set
        {
            if (_costo== value) return;
            _costo= value;
            OnPropertyChanged();
        }
    }

    public ComandoComprar ComandoComprar {  get; set; }

    public event EventHandler<int> AvisarIdCompradol;

    public CompraViewModel(BbddjuegoMarvelContext context, int idPersonaje, string nombre, string imgCuerpo, int costo)
    {
        _idPersonaje = idPersonaje;
        _nombre = nombre;
        _imgCuerpo = imgCuerpo;
        _costo = costo;
        ComandoComprar = new(this, context);
    }

    public void OnAvisarIdComprado()
    {
        AvisarIdCompradol.Invoke(null,IdPersonaje);
    }
}

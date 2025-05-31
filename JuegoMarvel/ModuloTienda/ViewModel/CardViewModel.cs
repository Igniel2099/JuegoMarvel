using JuegoMarvel.ModuloTienda.Model;
using JuegoMarvel.ModuloTienda.ViewModel.Comandos;
using JuegoMarvelData.Models;

namespace JuegoMarvel.ModuloTienda.ViewModel;

public class CardViewModel : BaseViewModel
{
    public int IdPersonaje { get; set; }

    private string _nombre;
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
    private string _tipo;
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
    private string _grupo;
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
    private string _costo;
    public string Costo
    {
        get => _costo;
        set
        {
            if (value == _costo) return;
            _costo = value;
            OnPropertyChanged();
        }
    }
    private string _imgPrincipal;
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
    private string _imgHabilidadUno;
    public string ImgHabilidadUno
    {
        get => _imgHabilidadUno;
        set
        {
            if (value == _imgHabilidadUno ) return;
        _imgHabilidadUno = value;
        OnPropertyChanged();
        }
    }
    private string _imgHabilidadDos;
    public string ImgHabilidadDos
    {
        get => _imgHabilidadDos;
        set
        {
            if (value == _imgHabilidadDos) return;
            _imgHabilidadDos= value;
            OnPropertyChanged();
        }
    }

    private string _imgHabilidadTres;
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

    public MostrarInformacionCommand MostrarInformacionCommand { get; set; }

    public CardViewModel(
        int id,
        string nombre,
        string tipo,
        string grupo,
        string costo,
        PersonajeImg personajeImg)
    {
        IdPersonaje = id;
        _nombre = nombre;
        _tipo = tipo;
        _grupo = grupo;
        _costo = costo;

        _imgPrincipal = personajeImg.ImgPrincipal;
        _imgHabilidadUno = personajeImg.ImgHabilidades["HabilidadUno"];
        _imgHabilidadDos = personajeImg.ImgHabilidades["HabilidadDos"];
        _imgHabilidadTres = personajeImg.ImgHabilidades["HabilidadTres"];
    }
}

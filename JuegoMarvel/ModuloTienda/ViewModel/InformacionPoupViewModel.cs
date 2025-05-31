
using JuegoMarvel.ModuloTienda.Model;
using JuegoMarvelData.Models;

namespace JuegoMarvel.ModuloTienda.ViewModel;

public class InformacionPoupViewModel(
    string nombre,
    List<Habilidade> habilidades,
    PersonajeImg personajeImg) : BaseViewModel
{
    private string _nombre = nombre;
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


    private string _nombreHabilidadUno = habilidades[0].Nombre;
    public string NombreHabilidadUno
    {
        get => _nombreHabilidadUno;
        set
        {
            if (value == _nombreHabilidadUno) return;
            _nombreHabilidadUno = value;
            OnPropertyChanged();
        }
    }

    private string _nombreHabilidadDos = habilidades[1].Nombre;
    public string NombreHabilidadDos
    {
        get => _nombreHabilidadDos;
        set
        {
            if (value == _nombreHabilidadDos) return;
            _nombreHabilidadDos = value;
            OnPropertyChanged();
        }
    }

    private string _nombreHabilidadTres = habilidades[2].Nombre;
    public string NombreHabilidadTres
    {
        get => _nombreHabilidadTres;
        set
        {
            if (value == _nombreHabilidadTres) return;
            _nombreHabilidadTres = value;
            OnPropertyChanged();
        }
    }


    private string _imgCuerpo = personajeImg.ImgCuerpo;
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



    private string _imgHabilidadUno = personajeImg.ImgHabilidades["HabilidadUno"];
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
    private string _imgHabilidadDos = personajeImg.ImgHabilidades["HabilidadDos"];
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

    private string _imgHabilidadTres = personajeImg.ImgHabilidades["HabilidadTres"];
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
    
    private string _imgTipoUno = habilidades[0].Tipo;
    public string ImgTipoUno
    {
        get => _imgTipoUno;
        set
        {
            if (value == _imgTipoUno) return;
            _imgTipoUno = value;
            OnPropertyChanged();
        }
    }
    private string _imgTipoDos = habilidades[1].Tipo;
    public string ImgTipoDos
    {
        get => _imgTipoDos;
        set
        {
            if (value == _imgTipoDos) return;
            _imgTipoDos = value;
            OnPropertyChanged();
        }
    }

    private string _imgTipoTres = habilidades[2].Tipo;
    public string ImgTipoTres
    {
        get => _imgTipoTres;
        set
        {
            if (value == _imgTipoTres) return;
            _imgTipoTres = value;
            OnPropertyChanged();
        }
    }


}

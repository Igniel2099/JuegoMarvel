
using JuegoMarvel.ModuloTienda.Model;
using JuegoMarvel.ModuloTienda.ViewModel.Comandos;
using JuegoMarvelData.Models;

namespace JuegoMarvel.ModuloTienda.ViewModel;

public class InformacionPoupViewModel : BaseViewModel
{
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


    private string _nombreHabilidadUno;
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

    private string _nombreHabilidadDos;
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

    private string _nombreHabilidadTres;
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


    private string _imgCuerpo;
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



    private string _imgHabilidadUno;
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
    private string _imgHabilidadDos;
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

    private string _imgTipoUno;
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
    private string _imgTipoDos;
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

    private string _imgTipoTres;
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



    private string _nombreHabilidadSeleccionada;
    public string NombreHabilidadSeleccionada
    {
        get => _nombreHabilidadSeleccionada;
        set
        {
            if (value == _nombreHabilidadSeleccionada) return;
            _nombreHabilidadSeleccionada = value;
            EmpezarAnimacion.OnSeleccionadaCambio(value);
        }
    }

    private string _imgPlay;
    public string ImgPlay
    {
        get => _imgPlay;
        set
        {
            if (value == _imgPlay) return;
            _imgPlay = value;
            OnPropertyChanged();
        }
    }

    public ComandoTerminarAnimacion TerminarAnimacion { get; set; }
    public ComandoEmpezarAnimación EmpezarAnimacion { get; set; }

    public InformacionPoupViewModel(
        string nombre,
        List<Habilidade> habilidades,
        PersonajeImg personajeImg)
    {
        _nombreHabilidadSeleccionada = habilidades[0].Nombre;
        _imgTipoTres = habilidades[2].Tipo;
        _imgTipoDos = habilidades[1].Tipo;
        _imgTipoUno = habilidades[0].Tipo;
        _imgHabilidadTres = personajeImg.Habilidades[2].Casillas.Original;
        _imgHabilidadDos = personajeImg.Habilidades[1].Casillas.Original;
        _imgHabilidadUno = personajeImg.Habilidades[0].Casillas.Original;
        _imgCuerpo = personajeImg.ImgCuerpo;
        _imgPlay = personajeImg.ImgCuerpo;
        _nombreHabilidadTres = habilidades[2].Nombre;
        _nombreHabilidadDos = habilidades[1].Nombre;
        _nombreHabilidadUno = habilidades[0].Nombre;
        _nombre = nombre;


        var dispatcher = Application.Current?.Dispatcher
                         ?? throw new InvalidOperationException("Dispatcher no disponible");

        IDispatcherTimer timer = dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromMilliseconds(100);
        timer.IsRepeating = true;

        EmpezarAnimacion = new ComandoEmpezarAnimación(personajeImg, habilidades[0].Nombre, this, timer);
        TerminarAnimacion = new(this, timer);
    }


}

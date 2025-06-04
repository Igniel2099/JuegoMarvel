using JuegoMarvel.ModuloEquipo.ViewModel.Comandos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoMarvel.ModuloEquipo.ViewModel;

public class PersonajeUsuarioViewModel : BaseViewModel
{
    public readonly int IdPersonajeUsuario;

    private string _imgContenedor;
    public string ImagenContenedor
    {
        get => _imgContenedor;
        set
        {
            if (value == _imgContenedor) return;
            _imgContenedor= value;
            OnPropertyChanged();
        }
    }

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

    private string _tipoUno;
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


    private string _tipoDos;
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


    private string _tipoTres;
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

    public ComandoSeleccionarPersonaje ComandoSeleccionarPersonaje { get; set; }
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
        ComandoSeleccionarPersonaje = new(vm,this);

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

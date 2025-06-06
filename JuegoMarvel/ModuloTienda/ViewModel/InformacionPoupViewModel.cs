using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloTienda.Model;
using JuegoMarvel.ModuloTienda.ViewModel.Comandos;
using JuegoMarvelData.Models;

namespace JuegoMarvel.ModuloTienda.ViewModel;

/// <summary>
/// ViewModel para la ventana de información detallada de un personaje en la tienda.
/// Expone las propiedades visuales y de animación necesarias para mostrar el nombre, habilidades, imágenes y animaciones del personaje.
/// Gestiona la selección de habilidades y los comandos para iniciar y detener la animación de la habilidad seleccionada.
/// </summary>
public class InformacionPoupViewModel : BaseViewModel
{
    #region camposViewModel

    /// <summary>
    /// Propiedad privada del nombre del personaje
    /// </summary>
    private string _nombre;

    /// <summary>
    /// Nombre completo del personaje.
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
    /// Propiedad privada del nombre de la primera habilidad
    /// </summary>
    private string _nombreHabilidadUno;

    /// <summary>
    /// Nombre de la primera habilidad del personaje.
    /// </summary>
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

    /// <summary>
    /// Propiedad privada del nombre de la segunda habilidad
    /// </summary>
    private string _nombreHabilidadDos;

    /// <summary>
    /// Nombre de la segunda habilidad del personaje.
    /// </summary>
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

    /// <summary>
    /// Propiedad privada del nombre de la tercera habilidad
    /// </summary>
    private string _nombreHabilidadTres;

    /// <summary>
    /// Nombre de la tercera habilidad del personaje.
    /// </summary>
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

    /// <summary>
    /// Propieda privada de la Imagen del cuerpo
    /// </summary>
    private string _imgCuerpo;

    /// <summary>
    /// Imagen del cuerpo del personaje.
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
    /// Propiedad privada de la primera Habilidad
    /// </summary>
    private string _imgHabilidadUno;

    /// <summary>
    /// Imagen de la primera habilidad.
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
    /// Propiedad privada de la segunda Habilidad
    /// </summary>
    private string _imgHabilidadDos;

    /// <summary>
    /// Imagen de la segunda habilidad.
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
    /// Imagen de la tercera habilidad.
    /// </summary>
    private string _imgHabilidadTres;

    /// <summary>
    /// Imagen de la tercera habilidad.
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

    /// <summary>
    /// Propiedad privada de la imagen del tipo de la primera habilidad
    /// </summary>
    private string _imgTipoUno;

    /// <summary>
    /// Imagen del tipo de la primera habilidad.
    /// </summary>
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

    /// <summary>
    /// Propiedad privada de la imagen del tipo de la segunda habilidad
    /// </summary>
    private string _imgTipoDos;

    /// <summary>
    /// Imagen del tipo de la segunda habilidad.
    /// </summary>
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

    /// <summary>
    /// Propiedad privada de la imagen del tipo de la tercera habilidad
    /// </summary>
    private string _imgTipoTres;

    /// <summary>
    /// Imagen del tipo de la tercera habilidad.
    /// </summary>
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

    /// <summary>
    /// Propiedad privada de la habilidad seleccionada
    /// </summary>
    private string _nombreHabilidadSeleccionada;

    /// <summary>
    /// Nombre de la habilidad actualmente seleccionada para animar.
    /// Al cambiar, notifica al comando de animación para actualizar la animación.
    /// </summary>
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

    /// <summary>
    /// Propieda privada de la imagen del play
    /// </summary>
    private string _imgPlay;

    /// <summary>
    /// Imagen actual mostrada en la animación (frame actual o imagen estática).
    /// </summary>
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

    /// <summary>
    /// propiedad privada del desplazamiento del Eje X
    /// </summary>
    private double _ejeX;

    /// <summary>
    /// Desplazamiento en el eje X de la animación.
    /// </summary>
    public double EjeX
    {
        get => _ejeX;
        set
        {
            if (_ejeX == value) return;
            _ejeX = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region Comando

    /// <summary>
    /// Comando para detener la animación de la habilidad seleccionada.
    /// </summary>
    public ComandoTerminarAnimacion TerminarAnimacion { get; set; }

    /// <summary>
    /// Comando para iniciar la animación de la habilidad seleccionada.
    /// </summary>
    public ComandoEmpezarAnimación EmpezarAnimacion { get; set; }
    #endregion

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="InformacionPoupViewModel"/> con los datos del personaje y sus habilidades.
    /// Configura los comandos de animación y establece los valores iniciales de las propiedades.
    /// </summary>
    /// <param name="nombre">Nombre completo del personaje.</param>
    /// <param name="habilidades">Lista de habilidades del personaje.</param>
    /// <param name="personajeImg">Modelo gráfico con las imágenes del personaje y sus habilidades.</param>
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
        _ejeX = 0;
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
using JuegoMarvel.ModuloLogin.Model;
using System.Collections.ObjectModel;

namespace JuegoMarvel.ModuloLogin.ViewModel;

/// <summary>
/// ViewModel para mostrar un popup de errores en la interfaz de usuario.
/// Permite configurar el título, la imagen y los mensajes de error a mostrar (Maximo 3 mensajes de error).
/// </summary>
public class PopupErroresViewModel : BaseViewModel
{
    /// <summary>
    /// Propieda privada del titulo de la pantalla.
    /// </summary>
    private string? _tituloPantalla;

    /// <summary>
    /// Título que se muestra en la parte superior del popup.
    /// </summary>
    public string? TituloPantalla
    {
        get => _tituloPantalla;
        set
        {
            if (_tituloPantalla != value)
            {
                _tituloPantalla = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Propiedad privada de la imagen del titulo
    /// </summary>
    private string? _imgTitulo;

    /// <summary>
    /// Ruta o nombre de la imagen que se muestra junto al título.
    /// </summary>
    public string? ImgTitulo
    {
        get => _imgTitulo;
        set
        {
            if (_imgTitulo != value)
            {
                _imgTitulo = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Propiedad privada del bool contenedor de textos visibles
    /// </summary>
    private bool _contenedorTextosVisible;

    /// <summary>
    /// Indica si el contenedor de mensajes de error debe ser visible.
    /// </summary>
    public bool ContenedorTextosVisible
    {
        get => _contenedorTextosVisible;
        set
        {
            if (_contenedorTextosVisible != value)
            {
                _contenedorTextosVisible = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Colección de mensajes de error a mostrar en el popup.
    /// </summary>
    public ObservableCollection<ErrorItem> MensajesError { get; } = [];

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="PopupErroresViewModel"/> con un título y mensajes de error.
    /// </summary>
    /// <param name="tituloPantalla">Título del popup.</param>
    /// <param name="mensajes">Mensajes de error a mostrar (máximo 3).</param>
    /// <exception cref="ArgumentException">Se lanza si se proporcionan más de 3 mensajes.</exception>
    public PopupErroresViewModel(string tituloPantalla, params string?[] mensajes)
    {
        if (mensajes.Length > 3)
            throw new ArgumentException("Se requieren como máximo 3 mensajes de error.", nameof(mensajes));

        TituloPantalla = tituloPantalla;

        SetErrores(mensajes);

        ConfigurarVista();
    }

    /// <summary>
    /// Establece los mensajes de error a mostrar en el popup.
    /// </summary>
    /// <param name="mensajes">Mensajes de error a mostrar (máximo 3).</param>
    public void SetErrores(params string?[] mensajes)
    {
        MensajesError.Clear();
        foreach (var msg in mensajes)
        {
            if (!string.IsNullOrWhiteSpace(msg))
                MensajesError.Add(new ErrorItem { Texto = msg });
        }
    }

    /// <summary>
    /// Configura la visibilidad y la imagen del popup según si hay errores o no.
    /// </summary>
    private void ConfigurarVista()
    {
        if (MensajesError.Any())
        {
            ContenedorTextosVisible = true;
            ImgTitulo = "icono_error.svg";
        }
        else
        {
            ContenedorTextosVisible = false;
            ImgTitulo = "icono_correcto.svg";
        }
    }
}
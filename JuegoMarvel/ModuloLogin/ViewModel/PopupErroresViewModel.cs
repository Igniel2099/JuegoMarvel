using JuegoMarvel.ModuloLogin.Model;
using System.Collections.ObjectModel;

namespace JuegoMarvel.ModuloLogin.ViewModel;

public class PopupErroresViewModel : BaseViewModel
{
    private string? _tituloPantalla;
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

    private string? _imgTitulo;
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

    private bool _contenedorTextosVisible;
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

    public ObservableCollection<ErrorItem> MensajesError { get; } = [];

    public void SetErrores(params string?[] mensajes)
    {
        MensajesError.Clear();
        foreach (var msg in mensajes)
            if (!string.IsNullOrWhiteSpace(msg))
                MensajesError.Add(new ErrorItem { Texto = msg });
    }

    public PopupErroresViewModel(string tituloPantalla, params string?[] mensajes)
    {
        if (mensajes.Length > 3)
            throw new ArgumentException("Se requieren como máximo 3 mensajes de error.", nameof(mensajes));

        TituloPantalla = tituloPantalla; //  "ERROR AL CREAR CUENTA";
                                         //  "FELICIDADES!\\nSE CREO LA CUENTA DE FORMA EXITOSA";

        SetErrores(mensajes);

        ConfigurarVista();
    }

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

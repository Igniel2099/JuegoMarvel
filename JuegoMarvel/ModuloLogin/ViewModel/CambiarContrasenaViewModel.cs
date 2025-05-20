using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.ViewModel.Comandos;
using System.Windows.Input;

namespace JuegoMarvel.ModuloLogin.ViewModel;

public class CambiarContrasenaViewModel : BaseViewModel
{
    private string _nombreUsuario;
    public string NombreUsuario
    {
        get => _nombreUsuario;
        set
        {
            if ( _nombreUsuario != value)
            {
                _nombreUsuario = value;
                OnPropertyChanged();
            }
        }
    }

    private string? _contrasena;
    public string? Contrasena
    {
        get => _contrasena;
        set
        {
            if (_contrasena != value)
            {
                _contrasena = value?.Trim();
                OnPropertyChanged();
            }
        }
    }

    private string? _confirmarContrasena;

    public string? ConfirmarContrasena
    {
        get => _confirmarContrasena;
        set
        {
            if (_confirmarContrasena != value)
            {
                _confirmarContrasena = value?.Trim();
                OnPropertyChanged();
            }
        }
    }


    public string ImgEstadoContrasena =>
    EstadoImgContrasena == null
        ? "icono_estado.png"
        : EstadoImgContrasena == true
            ? "icono_correcto_blanco.svg"
            : "icono_error_blanco.svg";

    private bool? _estadoImgContrasena;
    public bool? EstadoImgContrasena
    {
        get => _estadoImgContrasena;
        set
        {
            if (_estadoImgContrasena == value) return;
            _estadoImgContrasena = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ImgEstadoContrasena));
        }
    }

    public string ImgEstadoConfirmarContrasena =>
    EstadoImgConfirmarContrasena == null
        ? "icono_estado.png"
        : EstadoImgConfirmarContrasena == true
            ? "icono_correcto_blanco.svg"
            : "icono_error_blanco.svg";

    private bool? _estadoImgConfirmarContrasena;
    public bool? EstadoImgConfirmarContrasena
    {
        get => _estadoImgConfirmarContrasena;
        set
        {
            if (_estadoImgConfirmarContrasena == value) return;
            _estadoImgConfirmarContrasena = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ImgEstadoConfirmarContrasena));
        }
    }

    public ComandoConfirmarCambiarContrasena ComandoConfirmar { get; set; }

    public ComandoNavegarVolverAtras ComandoNavVolverAtras;

    public CambiarContrasenaViewModel(AppSettings settings, string nombreUsuario)
    {
        _nombreUsuario = nombreUsuario;
        ComandoConfirmar = new(this, settings);
        ComandoNavVolverAtras = new();
    }

}

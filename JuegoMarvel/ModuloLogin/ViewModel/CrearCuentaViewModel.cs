using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.ViewModel.Comandos;

namespace JuegoMarvel.ModuloLogin.ViewModel;

public class CrearCuentaViewModel : BaseViewModel
{
    private string? _nombreUsuario;
    public string? NombreUsuario
    {
        get => _nombreUsuario;
        set
        {
            if(_nombreUsuario != value)
            {
                _nombreUsuario = value;
                OnPropertyChanged();

            }
        }
    }

    private string? _correoElectronico;
    public string? CorreoElectronico
    {
        get => _correoElectronico;
        set
        {
            if  (_correoElectronico != value)
            {
                _correoElectronico = value;
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
            if (_contrasena != value )
            {
                _contrasena = value;
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
                _confirmarContrasena = value;
                OnPropertyChanged();
            }
        }
    }

    public string ImgEstadoNombreUsuario =>
    EstadoImgNombreUsuario == null
        ? "icono_estado.png"
        : EstadoImgNombreUsuario == true
            ? "icono_correcto.svg"
            : "icono_error.svg";

    private bool? _estadoImgNombreUsuario;
    public bool? EstadoImgNombreUsuario
    {
        get => _estadoImgNombreUsuario;
        set
        {
            if (_estadoImgNombreUsuario == value) return;
            _estadoImgNombreUsuario = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ImgEstadoNombreUsuario));
        }
    }
    public string ImgEstadoCorreoElectronico =>
    EstadoImgCorreoElectronico == null
        ? "icono_estado.png"
        : EstadoImgCorreoElectronico == true
            ? "icono_correcto.svg"
            : "icono_error.svg";

    private bool? _estadoImgCorreoElectronico;
    public bool? EstadoImgCorreoElectronico
    {
        get => _estadoImgCorreoElectronico;
        set
        {
            if (_estadoImgCorreoElectronico == value) return;
            _estadoImgCorreoElectronico = value;
            OnPropertyChanged();
            OnPropertyChanged(nameof(ImgEstadoCorreoElectronico));
        }
    }

    public string ImgEstadoContrasena =>
    EstadoImgContrasena == null
        ? "icono_estado.png"
        : EstadoImgContrasena == true
            ? "icono_correcto.svg"
            : "icono_error.svg";

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
            ? "icono_correcto.svg"
            : "icono_error.svg";

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

    public ComandoConfirmarCrearCuenta ComandoConfirmar { get; set; }

    public ComandoNavegarVolverAtras ComandoNavVolverAtras { get; set; }

    public CrearCuentaViewModel(AppSettings settings)
    {
        ComandoConfirmar = new(settings, this);
        ComandoNavVolverAtras = new();
    }

}

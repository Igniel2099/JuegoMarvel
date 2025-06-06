using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.ViewModel.Comandos;
using JuegoMarvel.Services;

namespace JuegoMarvel.ModuloLogin.ViewModel;

/// <summary>
/// ViewModel para la pantalla de creación de cuenta.
/// Gestiona los datos de entrada, validaciones y comandos relacionados con el registro de un nuevo usuario.
/// </summary>
public class CrearCuentaViewModel : BaseViewModel
{
    #region CamposViewModel

    /// <summary>
    /// Propiedad privada del nombre de usuario.
    /// </summary>
    private string? _nombreUsuario;

    /// <summary>
    /// Nombre de usuario introducido por el usuario.
    /// </summary>
    public string? NombreUsuario
    {
        get => _nombreUsuario;
        set
        {
            if (_nombreUsuario != value)
            {
                _nombreUsuario = value?.Trim();
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Propiedad privada del Correo Electronico
    /// </summary>
    private string? _correoElectronico;

    /// <summary>
    /// Correo electrónico introducido por el usuario.
    /// </summary>
    public string? CorreoElectronico
    {
        get => _correoElectronico;
        set
        {
            if (_correoElectronico != value)
            {
                _correoElectronico = value?.Trim();
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Propiedad privada de la contraseña
    /// </summary>
    private string? _contrasena;

    /// <summary>
    /// Contraseña introducida por el usuario.
    /// </summary>
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

    /// <summary>
    /// Propiedad privada de confirmar Contraseña
    /// </summary>
    private string? _confirmarContrasena;

    /// <summary>
    /// Confirmación de la contraseña introducida por el usuario.
    /// </summary>
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

    /// <summary>
    /// Imagen que representa el estado de validación del nombre de usuario.
    /// </summary>
    public string ImgEstadoNombreUsuario =>
        EstadoImgNombreUsuario == null
            ? "icono_estado.png"
            : EstadoImgNombreUsuario == true
                ? "icono_correcto_blanco.svg"
                : "icono_error_blanco.svg";

    /// <summary>
    /// Propiedad privada del estado de la imagen del nombre usuario.
    /// </summary>
    private bool? _estadoImgNombreUsuario;

    /// <summary>
    /// Estado de validación del nombre de usuario (null: sin validar, true: válido, false: inválido).
    /// </summary>
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

    /// <summary>
    /// Imagen que representa el estado de validación del correo electrónico.
    /// </summary>
    public string ImgEstadoCorreoElectronico =>
        EstadoImgCorreoElectronico == null
            ? "icono_estado.png"
            : EstadoImgCorreoElectronico == true
                ? "icono_correcto_blanco.svg"
                : "icono_error_blanco.svg";

    /// <summary>
    /// Propiedad privada del estado de la imagen del Correo Electronico.
    /// </summary>
    private bool? _estadoImgCorreoElectronico;

    /// <summary>
    /// Estado de validación del correo electrónico (null: sin validar, true: válido, false: inválido).
    /// </summary>
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

    /// <summary>
    /// Imagen que representa el estado de validación de la contraseña.
    /// </summary>
    public string ImgEstadoContrasena =>
        EstadoImgContrasena == null
            ? "icono_estado.png"
            : EstadoImgContrasena == true
                ? "icono_correcto_blanco.svg"
                : "icono_error_blanco.svg";

    /// <summary>
    /// Propiedad privada del estado de la imagen de la contraseña
    /// </summary>
    private bool? _estadoImgContrasena;

    /// <summary>
    /// Estado de validación de la contraseña (null: sin validar, true: válida, false: inválida).
    /// </summary>
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

    /// <summary>
    /// Imagen que representa el estado de validación de la confirmación de contraseña.
    /// </summary>
    public string ImgEstadoConfirmarContrasena =>
        EstadoImgConfirmarContrasena == null
            ? "icono_estado.png"
            : EstadoImgConfirmarContrasena == true
                ? "icono_correcto_blanco.svg"
                : "icono_error_blanco.svg";

    /// <summary>
    /// Propiedad privada del estado de la imagen de confirmar contraseña.
    /// </summary>
    private bool? _estadoImgConfirmarContrasena;

    /// <summary>
    /// Estado de validación de la confirmación de contraseña (null: sin validar, true: válida, false: inválida).
    /// </summary>
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
    #endregion

    #region Comandos

    /// <summary>
    /// Comando para confirmar la creación de la cuenta.
    /// </summary>
    public ComandoConfirmarCrearCuenta ComandoConfirmar { get; set; }

    /// <summary>
    /// Comando para navegar hacia atrás en la navegación.
    /// </summary>
    public ComandoNavegarVolverAtras ComandoNavVolverAtras { get; set; }
    #endregion

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="CrearCuentaViewModel"/>.
    /// </summary>
    /// <param name="settings">Configuración de la aplicación.</param>
    /// <param name="comprobador">Comprobador de dominio para validaciones.</param>
    public CrearCuentaViewModel(AppSettings settings, ComprobadorDominio comprobador)
    {
        ComandoConfirmar = new(settings, comprobador, this);
        ComandoNavVolverAtras = new();
    }
}
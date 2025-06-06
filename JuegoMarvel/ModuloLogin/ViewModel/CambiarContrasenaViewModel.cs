using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.ViewModel.Comandos;
using JuegoMarvel.Services;

namespace JuegoMarvel.ModuloLogin.ViewModel;

/// <summary>
/// ViewModel para la pantalla de cambio de contraseña.
/// Gestiona que el nombre usuario se inicialize bien y el campo contraseña y confirmar contraseña,
/// las imagenes tambien se actualizan según sean validos los campos o no, que esto lo controlan los,
/// campos booleanos y finalmente gestiona el comando para cambiar la contraseña
/// </summary>
public class CambiarContrasenaViewModel : BaseViewModel
{
    #region CamposViewModel

    /// <summary>
    /// Propiedad privada del nombre del Usuario
    /// </summary>
    private string _nombreUsuario;

    /// <summary>
    /// Nombre de usuario al que se le cambiará la contraseña.
    /// </summary>
    public string NombreUsuario
    {
        get => _nombreUsuario;
        set
        {
            if (_nombreUsuario != value)
            {
                _nombreUsuario = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Propiedad privada de la contraseña
    /// </summary>
    private string? _contrasena;

    /// <summary>
    /// Nueva contraseña introducida por el usuario.
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
    /// Propiedad privada de la confirmacion de la nueva contraseña
    /// </summary>
    private string? _confirmarContrasena;

    /// <summary>
    /// Confirmación de la nueva contraseña introducida por el usuario.
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
    /// Propiedad privada del estado de la imagen de la confirmación de contraseña
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
    /// Comando para confirmar el cambio de contraseña.
    /// </summary>
    public ComandoConfirmarCambiarContrasena ComandoConfirmar { get; set; }

    /// <summary>
    /// Comando para navegar hacia atrás en la navegación.
    /// </summary>
    public ComandoNavegarVolverAtras ComandoNavVolverAtras { get; set; }
    #endregion

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="CambiarContrasenaViewModel"/>.
    /// inicializando el nombre del usuario, el comando confirmar y el comando volver atras
    /// </summary>
    /// <param name="settings">Configuración de la aplicación.</param>
    /// <param name="nombreUsuario">Nombre de usuario al que se le cambiará la contraseña.</param>
    public CambiarContrasenaViewModel(AppSettings settings, string nombreUsuario)
    {
        _nombreUsuario = nombreUsuario;
        ComandoConfirmar = new(this, settings);
        ComandoNavVolverAtras = new();
    }
}
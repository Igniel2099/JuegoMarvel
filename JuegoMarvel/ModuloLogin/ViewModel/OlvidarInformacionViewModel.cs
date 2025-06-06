using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.ViewModel.Comandos;
using JuegoMarvel.Services;

namespace JuegoMarvel.ModuloLogin.ViewModel;

/// <summary>
/// ViewModel para la pantalla de recuperación de información de usuario.
/// Gestiona el correo electrónico, el código de confirmación y los comandos asociados al proceso de recuperación.
/// </summary>
public class OlvidarInformacionViewModel : BaseViewModel
{
    #region camposViewModel

    /// <summary>
    /// Propiedad privada del correo electronico.
    /// </summary>
    private string? _correoElectronico;

    /// <summary>
    /// Correo electrónico introducido por el usuario para recuperar la información.
    /// </summary>
    public string? CorreoElectronico
    {
        get => _correoElectronico;
        set
        {
            if (_correoElectronico == value) return;
            _correoElectronico = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad privada del codigo de confirmación.
    /// </summary>
    private string? _codigoConfirmacion;

    /// <summary>
    /// Código de confirmación enviado al correo electrónico del usuario.
    /// </summary>
    public string? CodigoConfirmacion
    {
        get => _codigoConfirmacion;
        set
        {
            if (_codigoConfirmacion == value) return;
            _codigoConfirmacion = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad privada de si es editable el campo de correo electronico
    /// </summary>
    private bool _editable;

    /// <summary>
    /// Indica si el campo de correo electronico es editable.
    /// </summary>
    public bool Editable
    {
        get => _editable;
        set
        {
            if (_editable == value) return;
            _editable = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region Comandos

    /// <summary>
    /// Comando para enviar el correo de recuperación.
    /// </summary>
    public ComandoEnviar ComandoEnviar { get; set; }

    /// <summary>
    /// Comando para confirmar el código de recuperación.
    /// </summary>
    public ComandoConfirmarOlvidarInformacion ComandoConfirmar { get; set; }

    /// <summary>
    /// Comando para navegar hacia atrás en la navegación.
    /// </summary>
    public ComandoNavegarVolverAtras ComandoNavVolverAtras { get; set; }
    #endregion

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="OlvidarInformacionViewModel"/>.
    /// Inicializando el boleano editable como false, los campos vacio y los comandos.
    /// </summary>
    /// <param name="settings">Configuración de la aplicación.</param>
    /// <param name="comprobador">
    /// Comprobador de dominio para la validación del correo electronico.
    /// </param>
    public OlvidarInformacionViewModel(AppSettings settings, ComprobadorDominio comprobador)
    {
        _editable = false;
        CorreoElectronico = string.Empty;
        CodigoConfirmacion = string.Empty;
        ComandoEnviar = new(this, settings, comprobador);
        ComandoConfirmar = new(this, settings);
        ComandoNavVolverAtras = new();
    }
}
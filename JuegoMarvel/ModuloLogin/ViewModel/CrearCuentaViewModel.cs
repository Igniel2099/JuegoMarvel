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
            if(value != null && _nombreUsuario != value)
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
            if  (value != null && _correoElectronico != value)
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
            if (value != null && _contrasena != value )
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
            if (value != null  && _confirmarContrasena != value)
            {
                _confirmarContrasena = value;
                OnPropertyChanged();
            }
        }
    }

    public ComandoConfirmarCrearCuenta ComandoConfirmar { get; set; }

    public ComandoNavegarVolverAtras ComandoNavVolverAtras { get; set; }

    public CrearCuentaViewModel()
    {
        ComandoConfirmar = new(this);
        ComandoNavVolverAtras = new();
    }

}

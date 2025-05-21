using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.ViewModel.Comandos;
using JuegoMarvel.ModuloLogin.ViewModel.Comandos.Navegar;

namespace JuegoMarvel.ModuloLogin.ViewModel;

public class LoginViewModel : BaseViewModel
{
    private string? _nombreUsuario;
    public string? NombreUsuario
    {
        get => _nombreUsuario;
        set
        {
            if ( _nombreUsuario == value) return;

            _nombreUsuario = value;
            OnPropertyChanged();
            ComandoLogearse.Nombre = value!;
        }
    }

    private string? _contrasena;
    public string? Contrasena
    {
        get => _contrasena;
        set
        {
            if (_contrasena == value) return;

            _contrasena = value;
            OnPropertyChanged();
            ComandoLogearse.Contrasena = value!;
        }
    }

    public ComandoLogearse ComandoLogearse { get; set; }
    public ComandoNavegarCrearCuenta ComandoNavCrearCuenta { get; set; }
    public ComandoNavegarOlvidarInformacion ComandoNavOlvidarInformacion { get; set; }
    
    public LoginViewModel(AppSettings settings, ComprobadorDominio comprobador)
    {
        ComandoLogearse = 
            new ComandoLogearse(settings);
        ComandoNavCrearCuenta = 
            new ComandoNavegarCrearCuenta(settings, comprobador);
        ComandoNavOlvidarInformacion = 
            new ComandoNavegarOlvidarInformacion(settings, comprobador);

    }
}

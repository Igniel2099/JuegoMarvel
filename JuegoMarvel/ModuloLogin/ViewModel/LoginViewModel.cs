using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.ViewModel.Comandos;
using JuegoMarvel.ModuloLogin.ViewModel.Comandos.Navegar;

namespace JuegoMarvel.ModuloLogin.ViewModel;

public class LoginViewModel : BaseViewModel
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
                comandoLogearse._nombre = value;
            }
        }
    }

    private string _contrasena;
    public string Contrasena
    {
        get => _contrasena;
        set
        {
            if (_contrasena != value)
            {
               
                _contrasena = value;
                OnPropertyChanged();
                comandoLogearse._contrasena = value;
            }
        }
    }


    public ComandoLogearse comandoLogearse { get; set; }
    public ComandoNavegarCrearCuenta ComandoNavCrearCuenta { get; set; }
    public ComandoNavegarOlvidarInformacion ComandoNavOlvidarInformacion { get; set; }
    

    public LoginViewModel(AppSettings settings)
    {
        comandoLogearse = new ComandoLogearse();
        ComandoNavCrearCuenta = new ComandoNavegarCrearCuenta(settings);
        ComandoNavOlvidarInformacion = new ComandoNavegarOlvidarInformacion();
    }
}

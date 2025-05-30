using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvelData.Data;
using System.Windows.Input;

namespace JuegoMarvel.ModuloInicio.ViewModel;

public class InicioViewModel : BaseViewModel
{
    private string _nombreUsuario;

    public string NombreUsuario
    {
        get => _nombreUsuario;
        set
        {
            if (_nombreUsuario == value) return;
            _nombreUsuario = value;
            OnPropertyChanged();
        }
    }

    private string _nivel;
    public string Nivel
    {
        get => _nivel;
        set
        {
            if (_nivel == value) return;
            _nivel = value;
            OnPropertyChanged();
        }
    }

    private string _puntos;
    public string Puntos
    {
        get => _puntos;
        set
        {
            if (_puntos == value) return;
            _puntos = value;
            OnPropertyChanged();
        }
    }
    private string _monedas;
    public string Monedas
    {
        get => _monedas;
        set
        {
            if (_monedas == value) return;
            _monedas = value;
            OnPropertyChanged();
        }
    }


    public ICommand ComandoNavAyuda { get; set; }
    public ICommand ComandoNavConfiguracion { get; set; }

    public ICommand ComandoNavEmpezar { get; set; }

    public ComandoNavegar ComandoNavTienda { get; set; }

    public ICommand ComandoNavEquipo { get; set; }

    public InicioViewModel(AppSettings configuracion, BbddjuegoMarvelContext context)
    {
        ComandoNavTienda = new ComandoNavegar(context);
        var primerUsuario = context.Usuarios.FirstOrDefault() ?? 
            throw new Exception("Tenemos un problema, no hay usuarios en la tabla"); // Solo hay uno o deberia

        _nombreUsuario = primerUsuario.NombreUsuario;
        _nivel = "1";
        _puntos = primerUsuario.SuperPuntos.ToString();
        _monedas = primerUsuario.Monedas.ToString();
    }
}


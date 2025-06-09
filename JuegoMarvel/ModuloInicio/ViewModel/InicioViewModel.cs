using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvelData.Data;
using System.Windows.Input;

namespace JuegoMarvel.ModuloInicio.ViewModel;

/// <summary>
/// ViewModel principal para la pantalla de inicio.
/// Gestiona los datos del usuario y los comandos de navegación.
/// </summary>
public class InicioViewModel : BaseViewModel
{
    #region CamposViewModel

    /// <summary>
    /// Propiedad privada del nombre de usuario
    /// </summary>
    private string _nombreUsuario;

    /// <summary>
    /// Nombre del usuario actual.
    /// </summary>
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

    /// <summary>
    /// Propiedad Privada del Nivel.
    /// </summary>
    private string _nivel;
    /// <summary>
    /// Nivel actual del usuario.
    /// </summary>
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

    /// <summary>
    /// Propiedad privada de los puntos
    /// </summary>
    private string _puntos;
    /// <summary>
    /// Puntos actuales del usuario.
    /// </summary>
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

    /// <summary>
    /// Propiedad privada de las monedas.
    /// </summary>
    private string _monedas;
    /// <summary>
    /// Monedas actuales del usuario.
    /// </summary>
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
    #endregion

    /// <summary>
    /// Comando principal de navegación.
    /// </summary>
    public ComandoNavegar ComandoNav { get; set; }

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="InicioViewModel"/> con los datos del usuario y el contexto.
    /// Inicializo el comando navegar, obtendo el usuario de la base de datos, he inicializo los campos.
    /// </summary>
    /// <param name="configuracion">Configuración de la aplicación.</param>
    /// <param name="context">Contexto de la base de datos.</param>
    public InicioViewModel(AppSettings configuracion, BbddjuegoMarvelContext context)
    {
        ComandoNav = new ComandoNavegar(configuracion, context);

        var primerUsuario = context.Usuarios.FirstOrDefault() ??
            throw new Exception("Tenemos un problema, no hay usuarios en la tabla"); // Solo hay uno o deberia

        _nombreUsuario = primerUsuario.NombreUsuario;
        _nivel = "1";
        _puntos = primerUsuario.SuperPuntos.ToString();
        _monedas = primerUsuario.Monedas.ToString();
    }

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="InicioViewModel"/> sin datos.
    /// </summary>
    public InicioViewModel()
    {
    }
}
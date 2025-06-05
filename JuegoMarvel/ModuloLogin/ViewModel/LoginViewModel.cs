using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.ViewModel.Comandos;
using JuegoMarvel.ModuloLogin.ViewModel.Comandos.Navegar;
using JuegoMarvel.Services;
using JuegoMarvelData.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace JuegoMarvel.ModuloLogin.ViewModel;

/// <summary>
/// ViewModel para la pantalla de inicio de sesión.
/// Gestiona los campos de nombre y constraseña del View, para que sigan ciertos criterios,
/// crea comandos para navegar, ya sea para Crear cuenta o Olvidar contraseña y tiene la 
/// logica del boton login.
/// </summary>
public class LoginViewModel : BaseViewModel
{
    /// <summary>
    /// Propiedad privada del Nombre de usuario
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
            if ( _nombreUsuario == value) return;

            _nombreUsuario = value;
            OnPropertyChanged();
            ComandoLogearse.Nombre = value!;
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
            if (_contrasena == value) return;

            _contrasena = value;
            OnPropertyChanged();
            ComandoLogearse.Contrasena = value!;
        }
    }


    /// <summary>
    /// Comando para realizar el login.
    /// </summary>
    public ComandoLogearse ComandoLogearse { get; set; }

    /// <summary>
    /// Comando para navegar a la pantalla de creación de cuenta.
    /// </summary>
    public ComandoNavegarCrearCuenta ComandoNavCrearCuenta { get; set; }

    /// <summary>
    /// Comando para navegar a la pantalla de recuperación de información.
    /// </summary>
    public ComandoNavegarOlvidarInformacion ComandoNavOlvidarInformacion { get; set; }

    public RelayCommand ComandoBorraDatos { get; set; } // Borrar esto solo para debuggear

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="LoginViewModel"/>.
    /// </summary>
    /// <param name="context">DbContexto de la base de datos SQLite interna de la aplicación</param>
    /// <param name="settings">Configuración de la aplicación.</param>
    /// <param name="comprobador">Comprobador de dominio para validaciones.</param>
    public LoginViewModel(BbddjuegoMarvelContext context, AppSettings settings, ComprobadorDominio comprobador)
    {
        ComandoLogearse = 
            new ComandoLogearse(settings, context);
        ComandoNavCrearCuenta = 
            new ComandoNavegarCrearCuenta(settings, comprobador);
        ComandoNavOlvidarInformacion = 
            new ComandoNavegarOlvidarInformacion(settings, comprobador);

        //Momentaneo 
        NombreUsuario = "Sentry";
        Contrasena = "Alexander1234567890_";
        
        
        ComandoLogearse.Nombre = NombreUsuario!;

        ComandoLogearse.Contrasena = Contrasena!;

        ComandoBorraDatos = new RelayCommand(() =>
            {
                context.Database.ExecuteSqlRaw("DELETE FROM Usuario");
                context.Database.ExecuteSqlRaw("DELETE FROM Habilidade");
                context.Database.ExecuteSqlRaw("DELETE FROM Equipo");
                context.Database.ExecuteSqlRaw("DELETE FROM PersonajeUsuario");
                context.Database.ExecuteSqlRaw("DELETE FROM Personaje");
                context.Database.ExecuteSqlRaw("DELETE FROM Pelea");
                context.SaveChanges();
                Debug.WriteLine("\n\n\nHecho Borrado datos de la Base de datos\n\n\n");
            }
        );
    }
}

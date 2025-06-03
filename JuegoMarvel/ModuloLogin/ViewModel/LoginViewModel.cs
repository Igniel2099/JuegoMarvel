using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.ViewModel.Comandos;
using JuegoMarvel.ModuloLogin.ViewModel.Comandos.Navegar;
using JuegoMarvel.Services;
using JuegoMarvelData.Data;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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

    public RelayCommand ComandoBorraDatos { get; set; }
    public LoginViewModel(BbddjuegoMarvelContext context, AppSettings settings, ComprobadorDominio comprobador)
    {
        // Borrar lo de la base de datos.

        ComandoLogearse = 
            new ComandoLogearse(settings, context);
        ComandoNavCrearCuenta = 
            new ComandoNavegarCrearCuenta(settings, comprobador);
        ComandoNavOlvidarInformacion = 
            new ComandoNavegarOlvidarInformacion(settings, comprobador);

        //Momentaneo 
        NombreUsuario = "walther";
        Contrasena = "Carbono143412_";
        
        
        ComandoLogearse.Nombre = NombreUsuario!;

        ComandoLogearse.Contrasena = Contrasena!;

        ComandoBorraDatos = new RelayCommand(() =>
            {
                context.Database.ExecuteSqlRaw("DELETE FROM Usuario");
                context.Database.ExecuteSqlRaw("DELETE FROM Habilidade");
                context.Database.ExecuteSqlRaw("DELETE FROM Pelea");
                context.Database.ExecuteSqlRaw("DELETE FROM PersonajeUsuario");
                context.Database.ExecuteSqlRaw("DELETE FROM Equipo");
                context.Database.ExecuteSqlRaw("DELETE FROM Personaje");
                context.SaveChanges();
                Debug.WriteLine("\n\n\nHecho Borrado datos de la Base de datos\n\n\n");
            }
        );
    }


}

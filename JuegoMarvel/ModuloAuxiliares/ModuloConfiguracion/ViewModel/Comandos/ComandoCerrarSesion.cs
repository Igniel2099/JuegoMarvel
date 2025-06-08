using System.Diagnostics;
using JuegoMarvel.ClasesBase;
using JuegoMarvel.Views;
using JuegoMarvelData.Data;
using Microsoft.EntityFrameworkCore;
using JuegoMarvel.ModuloLogin.ViewModel;

namespace JuegoMarvel.ModuloAuxiliares.ModuloConfiguracion.ViewModel.Comandos;

public class ComandoCerrarSesion(BbddjuegoMarvelContext context) : BaseCommand
{
    private readonly BbddjuegoMarvelContext _context = context;
    public async override void Execute(object? parameter)
    {
        // Tengo que mandar los datos al Servidor Actualizados
        await ActualizarDatosUsuarioServidor();

        await BorrarDatos();

        // la clave "mantener_sesion" guardada en archivos de Android para gestionar el inicio de
        // sesión para settarlos a false.
        Preferences.Set("mantener_sesion", false);

        await CambiarPantalla();
    }

    private async Task BorrarDatos()
    {
        // Tengo que borrar los datos locales
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM Usuario");
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM Habilidade");
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM Equipo");
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM PersonajeUsuario");
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM Personaje");
        await _context.Database.ExecuteSqlRawAsync("DELETE FROM Pelea");

        _context.ChangeTracker.Clear(); // Limpia todo el seguimiento de entidades en memoria

        await _context.SaveChangesAsync();
        Debug.WriteLine("\n\n\nHecho Borrado datos de la Base de datos\n\n\n");
    }

    private async Task CambiarPantalla()
    {
        var window = Application.Current.Windows[0]; // para apps de una sola ventana
        var nav = window.Page.Navigation;

        var loginVm = ((App)Application.Current).Services.GetRequiredService<LoginViewModel>();


        // Para hacer el PushModal sin animación nativa:
        await nav.PushModalAsync(new Login(loginVm), false);
    }

    private async Task ActualizarDatosUsuarioServidor()
    {

    }
}

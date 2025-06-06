using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.Views;
using JuegoMarvelData.Data;
using Microsoft.EntityFrameworkCore;

namespace JuegoMarvel.ModuloAuxiliares.ModuloCargaInicial.ViewModel;

public class CargaInicialViewModel(BbddjuegoMarvelContext context, AppSettings settings, ComprobadorDominio comprobador) : BaseViewModel
{

    private readonly BbddjuegoMarvelContext _context = context;
    private readonly AppSettings _settings = settings;
    private readonly ComprobadorDominio _comprobador = comprobador;

    // Verificar si tengo un parametro que he guardado para poder guardar la sesión
    // Si es falso verifico elimino toda la base de datos local e inicia Login
    // Si es true verifico los datos si existen campos, inicio en Inicio
    // Verificar la integridad de los personajes de la base de datos con el json de las imagenes

    public async Task CargarDatosAsync()
    {
        if (Preferences.Get("mantener_sesion", false)) // el false solo se pone si manetener sesión nunca ha sido iniciado.
        {
            var window = Application.Current.Windows[0];
            var nav = window.Page.Navigation;
            await nav.PushModalAsync(new Inicio(new ModuloInicio.ViewModel.InicioViewModel(_settings, _context)));
        }
        else
        {
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Usuario");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Habilidade");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Equipo");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM PersonajeUsuario");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Personaje");
            await _context.Database.ExecuteSqlRawAsync("DELETE FROM Pelea");
            await _context.SaveChangesAsync();

            var window = Application.Current.Windows[0];
            var nav = window.Page.Navigation;
            await nav.PushModalAsync(new Login(new ModuloLogin.ViewModel.LoginViewModel(_context,_settings, _comprobador)));
        }
    }
}

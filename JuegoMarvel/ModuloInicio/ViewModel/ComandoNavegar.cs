using JuegoMarvel.ModuloAuxiliares.ModuloConfiguracion.View;
using JuegoMarvel.ModuloEquipo.View;
using JuegoMarvel.ModuloEquipo.ViewModel;
using JuegoMarvel.ModuloJuego.View;
using JuegoMarvel.ModuloJuego.ViewModel;
using JuegoMarvel.ModuloTienda.ViewModel;
using JuegoMarvel.ModuloAuxiliares.ModuloConfiguracion.ViewModel;
using JuegoMarvel.Views;
using JuegoMarvelData.Data;
using JuegoMarvel.ClasesBase;

namespace JuegoMarvel.ModuloInicio.ViewModel;

/// <summary>
/// Comando para gestionar la navegación entre las diferentes páginas principales de la aplicación.
/// </summary>
public class ComandoNavegar(BbddjuegoMarvelContext context) : BaseCommand
{
    /// <summary>
    /// Contexto de la base de datos utilizado para inicializar los ViewModels de las páginas.
    /// </summary>
    private readonly BbddjuegoMarvelContext _context = context;

    /// <summary>
    /// Ejecuta la navegación a la página correspondiente según el nombre recibido como parámetro.
    /// </summary>
    /// <param name="parameter">Nombre de la página a la que se desea navegar.</param>
    public async override void Execute(object? parameter)
    {
        if (parameter is string pageName)
        {
            var window = Application.Current.Windows[0];
            var nav = window.Page.Navigation;

            switch (pageName)
            {
                case "Configuración":
                    await nav.PushModalAsync(new ConfiguracionView(new ConfiguracionViewModel(_context)));
                    break;

                case "Ayuda":
                    //await nav.PushAsync(new Ayuda());
                    break;

                case "Tienda":
                    TiendaViewModel tVm = new(_context);
                    await tVm.CargarDatosDelView();
                    await nav.PushModalAsync(new Tienda(tVm));
                    break;

                case "Equipo":
                    // Cargo los datos de manera asincrona antes de iniciar la vista(Buena practica)
                    EquipoViewModel vm = new(_context);
                    await vm.InicializarPersonajesUsuarioViewModelAync(_context);
                    await nav.PushModalAsync(new EquipoView(vm));
                    break;

                case "Empezar":
                    BuscarJugadorViewModel bjVm = new();
                    await bjVm.CargarDatos(_context);
                    await nav.PushModalAsync(new BuscarJugador(bjVm));
                    break;
            }
        }
        else
        {
            Console.WriteLine("Parámetro inválido para la navegación.");
        }
    }
}
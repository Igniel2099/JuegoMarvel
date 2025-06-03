using JuegoMarvel.ModuloEquipo.View;
using JuegoMarvel.ModuloEquipo.ViewModel;
using JuegoMarvel.ModuloTienda.ViewModel;
using JuegoMarvel.Views;
using JuegoMarvelData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoMarvel.ModuloInicio.ViewModel;

public class ComandoNavegar(BbddjuegoMarvelContext context) : BaseCommand
{
    private readonly BbddjuegoMarvelContext _context = context;
    public async override void Execute(object? parameter)
    {
        if (parameter is string pageName)
        {
            var window = Application.Current.Windows[0];
            var nav = window.Page.Navigation;

            switch (pageName)
            {
                case "Configuración":
                    //await nav.PushAsync(new Configuracion());
                    break;

                case "Ayuda":
                    //await nav.PushAsync(new Ayuda());
                    break;

                case "Tienda":
                    await nav.PushModalAsync(new Tienda(new TiendaViewModel(_context)));
                    break;

                case "Equipo":
                    // Cargo los datos de manera asincrona antes de iniciar la vista(Buena practica)
                    EquipoViewModel vm = new(_context);
                    await vm.InicializarPersonajesUsuarioViewModelAync(_context);
                    await nav.PushAsync(new Equipo(vm));
                    break;

                case "Empezar":
                    //await nav.PushAsync(new Juego());
                    break;
            }
        }
        else
        {
            Console.WriteLine("Parámetro inválido para la navegación.");

        }
    }
}

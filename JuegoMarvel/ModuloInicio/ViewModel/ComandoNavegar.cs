using JuegoMarvel.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoMarvel.ModuloInicio.ViewModel;

public class ComandoNavegar : BaseCommand
{
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
                    await nav.PushModalAsync(new Tienda());
                    break;

                case "Equipo":
                    //await nav.PushAsync(new Equipo());
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

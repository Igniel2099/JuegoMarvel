
using JuegoMarvel.ModuloLogin.View;

namespace JuegoMarvel.ModuloLogin.ViewModel.Comandos;

public partial class ComandoNavegarCrearCuenta : BaseCommand
{
    public override async void Execute(object? parameter)
    {
        // Push sin animación nativa
        var window = Application.Current.Windows[0];      // para apps de una sola ventana
        var nav = window.Page.Navigation;

        // Para hacer el PushModal sin animación nativa:
        await nav.PushModalAsync(new CrearCuenta(), false);
    }
}

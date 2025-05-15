
using JuegoMarvel.ModuloLogin.View;
using System.Diagnostics;

namespace JuegoMarvel.ModuloLogin.ViewModel.Comandos.Navegar;

public partial class ComandoNavegarOlvidarInformacion : BaseCommand
{
    public override async void Execute(object? parameter)
    {
        try
        {
            // Push sin animación nativa
            var window = Application.Current.Windows[0];      // para apps de una sola ventana
            var nav = window.Page.Navigation;

            // Para hacer el PushModal sin animación nativa:
            await nav.PushModalAsync(new OlvidarInformacion(), false);

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"===============Error en ComandoNavegarOlvidarInformacion: {ex.Message}");    
            throw;
        }
    }
}

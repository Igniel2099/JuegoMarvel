
namespace JuegoMarvel.ModuloLogin.ViewModel.Comandos;

public class ComandoNavegarVolverAtras : BaseCommand
{
    public override async void Execute(object? parameter)
    {
        await Application.Current.MainPage
        .Navigation
        .PopModalAsync(false);
    }
}

namespace JuegoMarvel.Services;

public class ComandoNavegarVolverAtras : BaseCommand
{
    public override async void Execute(object? parameter)
    {
        await Application.Current.MainPage
        .Navigation
        .PopModalAsync(false);
    }
}

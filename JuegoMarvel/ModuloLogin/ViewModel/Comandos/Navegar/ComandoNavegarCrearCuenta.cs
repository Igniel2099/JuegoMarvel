
using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.View;

namespace JuegoMarvel.ModuloLogin.ViewModel.Comandos;

public partial class ComandoNavegarCrearCuenta : BaseCommand
{
    private readonly AppSettings _settings;
    private readonly ComprobadorDominio _comprobador;

    public ComandoNavegarCrearCuenta(AppSettings settings, ComprobadorDominio comprobador)
    {
        _settings = settings;
        _comprobador = comprobador;
    }
    public override async void Execute(object? parameter)
    {
        // Push sin animación nativa
        var window = Application.Current.Windows[0]; // para apps de una sola ventana
        var nav = window.Page.Navigation;

        // Para hacer el PushModal sin animación nativa:
        await nav.PushModalAsync(new CrearCuenta(new CrearCuentaViewModel(_settings, _comprobador)), false);
    }
}

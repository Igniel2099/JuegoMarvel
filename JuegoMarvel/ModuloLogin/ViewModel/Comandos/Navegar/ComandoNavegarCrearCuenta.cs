using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.View;

namespace JuegoMarvel.ModuloLogin.ViewModel.Comandos.Navegar;

/// <summary>
/// Comando para navegar a la pantalla de creación de cuenta.
/// Realiza la navegación modal hacia la vista <see cref="CrearCuenta"/> utilizando el ViewModel correspondiente.
/// </summary>
public partial class ComandoNavegarCrearCuenta : BaseCommand
{
    private readonly AppSettings _settings;
    private readonly ComprobadorDominio _comprobador;

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="ComandoNavegarCrearCuenta"/>.
    /// </summary>
    /// <param name="settings">Configuración de la aplicación.</param>
    /// <param name="comprobador">Comprobador de dominio para validaciones.</param>
    public ComandoNavegarCrearCuenta(AppSettings settings, ComprobadorDominio comprobador)
    {
        _settings = settings;
        _comprobador = comprobador;
    }

    /// <summary>
    /// Ejecuta la navegación modal hacia la pantalla de creación de cuenta.
    /// </summary>
    /// <param name="parameter">Parámetro opcional (no utilizado).</param>
    public override async void Execute(object? parameter)
    {
        // Push sin animación nativa
        var window = Application.Current.Windows[0]; // para apps de una sola ventana
        var nav = window.Page.Navigation;

        // Para hacer el PushModal sin animación nativa:
        await nav.PushModalAsync(new CrearCuenta(new CrearCuentaViewModel(_settings, _comprobador)), false);
    }
}
using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.View;
using System.Diagnostics;

namespace JuegoMarvel.ModuloLogin.ViewModel.Comandos.Navegar;

/// <summary>
/// Comando para navegar a la pantalla de recuperación de información de usuario (por ejemplo, restablecimiento de contraseña).
/// Realiza la navegación modal hacia la vista <see cref="OlvidarInformacion"/> utilizando el ViewModel correspondiente.
/// </summary>
/// <remarks>
/// Este comando utiliza <see cref="AppSettings"/> para la configuración de la aplicación y <see cref="ComprobadorDominio"/>
/// para validar dominios de correo electrónico durante el proceso de recuperación.
/// </remarks>
public partial class ComandoNavegarOlvidarInformacion(AppSettings settings, ComprobadorDominio comprobador) : BaseCommand
{
    /// <summary>
    /// Propiedad Privada de la configuración de la app
    /// </summary>
    private readonly AppSettings _settings = settings;

    /// <summary>
    /// Propiedad privada del Comprobador de dominios
    /// </summary>
    private readonly ComprobadorDominio _comprobador = comprobador;

    /// <summary>
    /// Ejecuta la navegación modal hacia la pantalla de recuperación de información de usuario.
    /// </summary>
    /// <param name="parameter">Parámetro opcional (no utilizado).</param>
    public override async void Execute(object? parameter)
    {
        try
        {
            // Push sin animación nativa
            var window = Application.Current.Windows[0];      // para apps de una sola ventana
            var nav = window.Page.Navigation;

            // Para hacer el PushModal sin animación nativa:
            await nav.PushModalAsync(new OlvidarInformacion(new OlvidarInformacionViewModel(_settings, _comprobador)), false);
        }
        catch (Exception ex)
        {
            Debug.WriteLine($"===============Error en ComandoNavegarOlvidarInformacion: {ex.Message}");
            throw;
        }
    }
}
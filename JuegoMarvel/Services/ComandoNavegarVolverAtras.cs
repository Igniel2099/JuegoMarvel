using JuegoMarvel.ClasesBase;

namespace JuegoMarvel.Services;

/// <summary>
/// Comando que permite navegar hacia atrás en la pila de navegación modal de la aplicación.
/// Hereda de <c>BaseCommand</c> y se usa típicamente para cerrar pantallas mostradas con <c>PushModalAsync</c>.
/// </summary>
public class ComandoNavegarVolverAtras : BaseCommand
{
    /// <summary>
    /// Ejecuta el comando de navegación hacia atrás cerrando la página modal actual.
    /// </summary>
    /// <param name="parameter">Parámetro opcional no utilizado en este comando.</param>
    /// <remarks>
    /// Usa <c>async void</c> porque este método se usa como comando, por ejemplo en botones.
    /// </remarks>
    public override async void Execute(object? parameter)
    {
        await Application.Current.MainPage
            .Navigation
            .PopModalAsync(false); // Propiedad privada implícita: la animación está deshabilitada (false)
    }
}

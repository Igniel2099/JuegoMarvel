
using System.Windows.Input;

namespace JuegoMarvel.Services;

/// <summary>
/// Implementación básica del patrón <see cref="ICommand"/> para encapsular una acción sin parámetros.
/// Es útil en escenarios MVVM para enlazar comandos a botones o eventos desde la vista.
/// </summary>
public class RelayCommand : ICommand
{
    /// <summary>
    /// Propiedad privada que almacena la acción que se ejecutará cuando se invoque el comando.
    /// </summary>
    private readonly Action _execute;

    /// <summary>
    /// Inicializa una nueva instancia del comando con la acción a ejecutar.
    /// </summary>
    /// <param name="execute">Acción que se ejecutará al invocar el comando.</param>
    public RelayCommand(Action execute)
    {
        _execute = execute;
    }

    /// <summary>
    /// Evento que se lanza cuando cambia la capacidad del comando para ejecutarse.
    /// No se usa en esta implementación, pero se puede invocar manualmente si se necesita.
    /// </summary>
    public event EventHandler? CanExecuteChanged;

    /// <summary>
    /// Determina si el comando se puede ejecutar.
    /// Esta implementación siempre devuelve <c>true</c>.
    /// </summary>
    /// <param name="parameter">Parámetro opcional (no usado).</param>
    /// <returns><c>true</c>, siempre permite la ejecución.</returns>
    public bool CanExecute(object? parameter) => true;

    /// <summary>
    /// Ejecuta la acción encapsulada.
    /// </summary>
    /// <param name="parameter">Parámetro opcional (no usado).</param>
    public void Execute(object? parameter) => _execute();
}

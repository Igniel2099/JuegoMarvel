using JuegoMarvel.ModuloJuego.ViewModel;

namespace JuegoMarvel.ModuloJuego.View;

/// <summary>
/// Página para buscar un jugador y mostrar animaciones de espera.
/// </summary>
public partial class BuscarJugador : ContentPage
{
    /// <summary>
    /// Inicializa una nueva instancia de <see cref="BuscarJugador"/> con el ViewModel proporcionado.
    /// </summary>
    /// <param name="vm">Instancia de <see cref="BuscarJugadorViewModel"/> para el binding de la vista.</param>
    public BuscarJugador(BuscarJugadorViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        // Inicia la animación correctamente en el hilo de la UI
        RepetirAnimacionBorder();
    }

    /// <summary>
    /// Inicia una animación infinita de los bordes, repitiendo la secuencia indefinidamente.
    /// </summary>
    private async void RepetirAnimacionBorder()
    {
        while (true)
        {
            await AnimarBordersSecuencialmente();
            await Task.Delay(1000); // Delay entre repeticiones completas
        }
    }

    /// <summary>
    /// Anima los bordes subiéndolos arriba y abajo de manera secuencial, uno por uno.
    /// </summary>
    /// <returns>Una tarea que representa la finalización de la animación secuencial.</returns>
    private async Task AnimarBordersSecuencialmente()
    {
        var borders = new[] { Border1, Border2, Border3 };

        foreach (var border in borders)
        {
            await border.TranslateTo(0, -10, 150, Easing.CubicOut);
            await border.TranslateTo(0, 0, 150, Easing.CubicIn);
            await Task.Delay(200); // Retardo entre cada uno
        }
    }
}
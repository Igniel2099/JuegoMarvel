using JuegoMarvel.ModuloJuego.ViewModel;
using Microsoft.Maui.Controls;

namespace JuegoMarvel.ModuloJuego.View;

public partial class BuscarJugador : ContentPage
{
    public BuscarJugador(BuscarJugadorViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        // Inicia la animación correctamente en el hilo de la UI
        RepetirAnimacionBorder();
    }

    /// <summary>
    /// Animacion Infinita de la animacion de los bordes
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
    /// Anima los bordes subiendolos arriba y abajo de manera secuencial,
    /// uno por uno
    /// </summary>
    /// <returns>Devuelve un aviso de que la animación ha terminado</returns>
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

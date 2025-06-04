
namespace JuegoMarvel.Services;

public static class AnimacionesComunes
{
    public static void CambiarImagenGrid(
        List<Grid> listaGrids, 
        Grid gridBuscado,
        string rutaImagen)
    {
        var imagenInterna = gridBuscado.Children.OfType<Image>().FirstOrDefault();

        if (imagenInterna!.Source == null)
            imagenInterna.Source = rutaImagen;

        listaGrids.Remove(gridBuscado);
    }

    public static void BorrarImagenGrid(List<Grid> listaGrids)
    {
        foreach (var grid in listaGrids)
        {
            var imagenAntigua = grid.Children.OfType<Image>().FirstOrDefault();
            imagenAntigua!.Source = null;
        }
    }

    public static async void AnimacionImagen(Grid gridTocado)
    {
        var imagen = gridTocado.Children.OfType<Image>().FirstOrDefault();

        if (imagen != null)
        {
            await imagen.ScaleTo(0.9, 100, Easing.CubicIn); // Escala al 80% en 100ms
            await imagen.ScaleTo(1.0, 100, Easing.CubicOut); // Vuelve al 100% en 100ms
        }
    }

    public static async Task AnimacionGrid(Grid gridTocado)
    {
        await gridTocado.ScaleTo(0.9, 100, Easing.CubicIn); // Escala al 80% en 100ms
        await gridTocado.ScaleTo(1.0, 100, Easing.CubicOut); // Vuelve al 100% en 100ms
    }

    /// <summary>
    /// Anima un efecto de vibración sobre el elemento dado.
    /// </summary>
    /// <param name="view">El View (por ejemplo, un Grid) que queremos "agitar".</param>
    /// <param name="duration">Duración total de la vibración, en milisegundos.</param>
    /// <param name="amplitude">Distancia máxima que oscilará (en pixeles) hacia cada lado.</param>
    public static async Task VibrarGrid(View view, uint duration = 200, double amplitude = 5)
    {
        int iterations = 4;

        uint stepDuration = (uint)(duration / (iterations * 2));

        for (int i = 0; i < iterations; i++)
        {
            await view.TranslateTo(amplitude, 0, stepDuration, Easing.Linear);
            await view.TranslateTo(-amplitude, 0, stepDuration, Easing.Linear);
        }

        await view.TranslateTo(0, 0, stepDuration, Easing.Linear);
    }
}

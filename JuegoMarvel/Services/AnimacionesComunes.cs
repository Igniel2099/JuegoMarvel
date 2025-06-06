namespace JuegoMarvel.Services;

/// <summary>
/// Contiene animaciones comunes reutilizables para elementos visuales en la aplicación MAUI.
/// Incluye efectos de escala, vibración y manipulación de imágenes dentro de grids.
/// </summary>
public static class AnimacionesComunes
{
    /// <summary>
    /// Cambia la imagen de un Grid específico dentro de una lista y la elimina de dicha lista.
    /// </summary>
    /// <param name="listaGrids">Lista de grids que contienen imágenes.</param>
    /// <param name="gridBuscado">Grid al que se le quiere cambiar la imagen.</param>
    /// <param name="rutaImagen">Ruta de la nueva imagen que se va a establecer.</param>
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

    /// <summary>
    /// Borra la imagen (establece `Source = null`) de todos los grids dentro de la lista dada.
    /// </summary>
    /// <param name="listaGrids">Lista de grids cuyas imágenes deben borrarse.</param>
    public static void BorrarImagenGrid(List<Grid> listaGrids)
    {
        foreach (var grid in listaGrids)
        {
            var imagenAntigua = grid.Children.OfType<Image>().FirstOrDefault();
            imagenAntigua!.Source = null;
        }
    }

    /// <summary>
    /// Realiza una animación de escala (zoom-in y luego zoom-out) sobre la imagen contenida dentro de un Grid.
    /// </summary>
    /// <param name="gridTocado">Grid que contiene la imagen a animar.</param>
    /// <remarks>
    /// Este método es de tipo <c>async void</c> porque puede ser usado como manejador de eventos.
    /// </remarks>
    public static async void AnimacionImagen(Grid gridTocado)
    {
        var imagen = gridTocado.Children.OfType<Image>().FirstOrDefault();

        if (imagen != null)
        {
            await imagen.ScaleTo(0.9, 100, Easing.CubicIn); // Escala al 90% en 100ms
            await imagen.ScaleTo(1.0, 100, Easing.CubicOut); // Vuelve al 100% en 100ms
        }
    }

    /// <summary>
    /// Realiza una animación de escala sobre todo el Grid (zoom-in y zoom-out).
    /// </summary>
    /// <param name="gridTocado">Grid al que se le aplicará la animación de escala.</param>
    public static async Task AnimacionGrid(Grid gridTocado)
    {
        await gridTocado.ScaleTo(0.9, 100, Easing.CubicIn); // Escala al 90% en 100ms
        await gridTocado.ScaleTo(1.0, 100, Easing.CubicOut); // Vuelve al 100% en 100ms
    }

    /// <summary>
    /// Aplica una animación de vibración lateral al elemento visual dado, simulando un efecto de "temblor".
    /// </summary>
    /// <param name="view">El elemento visual (por ejemplo, Grid o Frame) a animar.</param>
    /// <param name="duration">Duración total de la vibración, en milisegundos.</param>
    /// <param name="amplitude">Distancia máxima de desplazamiento lateral en píxeles.</param>
    public static async Task VibrarGrid(View view, uint duration = 200, double amplitude = 5)
    {
        int iterations = 4; // Propiedad privada implícita: número de ciclos de oscilación

        uint stepDuration = (uint)(duration / (iterations * 2)); // Propiedad privada: duración de cada paso

        for (int i = 0; i < iterations; i++)
        {
            await view.TranslateTo(amplitude, 0, stepDuration, Easing.Linear);
            await view.TranslateTo(-amplitude, 0, stepDuration, Easing.Linear);
        }

        await view.TranslateTo(0, 0, stepDuration, Easing.Linear);
    }
}

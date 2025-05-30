
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
}

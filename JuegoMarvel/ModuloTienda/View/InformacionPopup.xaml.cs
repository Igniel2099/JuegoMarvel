using CommunityToolkit.Maui.Views;
using JuegoMarvel.ModuloTienda.ViewModel;
using JuegoMarvel.Services;

namespace JuegoMarvel.Views;

public partial class InformacionPopup : Popup
{
    public InformacionPopup(InformacionPoupViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    private async void ClickedBotonCerrar(object sender, EventArgs e)
    {
        ImageButton imgButton = (ImageButton)sender;
        await imgButton.ScaleTo(0.9, 100, Easing.CubicIn); // Escala al 80% en 100ms
        await imgButton.ScaleTo(1, 100, Easing.CubicIn); // Escala al 80% en 100ms

        Close();
    }

    private async void TappedPlayOrPause(object sender, TappedEventArgs e)
    {
        Image img = (Image)sender;
        await img.ScaleTo(0.9, 100, Easing.CubicIn); // Escala al 80% en 100ms
        await img.ScaleTo(1, 100, Easing.CubicIn); // Escala al 80% en 100ms
        if (img.Source is FileImageSource fileImgSource)
            fileImgSource.File = fileImgSource.File == "icono_play.png" 
                ? "icono_pausa.png" 
                : "icono_play.png";

    }

    private void TappedGridHabilidad(object sender, TappedEventArgs e)
    {
        Grid gridTocado = (Grid)sender;
        List<Grid> GridsBarMenuTop = [GridPrimeraHabilidad, GridSegundaHabilidad, GridTerceraHabilidad];

        AnimacionesComunes.CambiarImagenGrid(
            GridsBarMenuTop,
            gridTocado, 
            "boton_seleccionado_habilidad.png"
        );

        AnimacionesComunes.BorrarImagenGrid(GridsBarMenuTop);

        AnimacionesComunes.AnimacionImagen(gridTocado);
    }
}
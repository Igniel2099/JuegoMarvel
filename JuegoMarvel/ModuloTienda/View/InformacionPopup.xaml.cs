using CommunityToolkit.Maui.Views;
using JuegoMarvel.ModuloTienda.ViewModel;
using JuegoMarvel.Services;

namespace JuegoMarvel.Views;

public partial class InformacionPopup : Popup
{

    private void CalcularTamañoDinamico()
    {
        // Obtenemos la info de la pantalla
        var infoPantalla = DeviceDisplay.MainDisplayInfo;

        // Obtenemos ancho y alto en unidades independientes de dispositivo (dp)
        double anchoPantalla = infoPantalla.Width / infoPantalla.Density;
        double altoPantalla = infoPantalla.Height / infoPantalla.Density;

        // Calculamos el tamaño deseado (por ejemplo, 80% del ancho y 50% del alto)
        double anchoPopup = anchoPantalla * 0.87;
        double altoPopup = altoPantalla * 0.9;

        // Asignamos el tamaño al popup
        this.Size = new Size(anchoPopup, altoPopup);
    }

    private readonly InformacionPoupViewModel _vm;

    public InformacionPopup(InformacionPoupViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
        CalcularTamañoDinamico();
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
        {

            if (fileImgSource.File == "icono_play.png")
            {
                fileImgSource.File = "icono_pausa.png";
                _vm.EmpezarAnimacion.Execute(null);

            }
            else
            {
                fileImgSource.File = "icono_play.png";
                _vm.TerminarAnimacion.Execute(null);
            }




        }
    }

    private void TappedGridHabilidad(object sender, TappedEventArgs e)
    {
        Grid gridTocado = (Grid)sender;
        _vm.NombreHabilidadSeleccionada = gridTocado.Children.OfType<Label>().FirstOrDefault().Text;
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
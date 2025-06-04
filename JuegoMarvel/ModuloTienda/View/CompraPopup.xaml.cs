using CommunityToolkit.Maui.Views;
using JuegoMarvel.ModuloTienda.ViewModel;

namespace JuegoMarvel.ModuloTienda.View;

public partial class CompraPopup : Popup
{
	public CompraPopup(CompraViewModel vm)
	{
		InitializeComponent();
        CalcularTamañoDinamico();
        BindingContext = vm;
    }

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

    public async void ClickedBotonCerrar(object sender, EventArgs e)
    {
        ImageButton imgButton = (ImageButton)sender;
        await imgButton.ScaleTo(0.9, 100, Easing.CubicIn); // Escala al 80% en 100ms
        await imgButton.ScaleTo(1, 100, Easing.CubicIn); // Escala al 80% en 100ms

        Close();
    }
}
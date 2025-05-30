using CommunityToolkit.Maui.Views;
using JuegoMarvel.ModuloLogin.ViewModel;

namespace JuegoMarvel.ModuloLogin.View;

public partial class PopupErrores : Popup
{
	public PopupErrores(PopupErroresViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        CalcularTamañoDinamico();
    }

    private void CalcularTamañoDinamico()
    {
        // Obtenemos la info de la pantalla
        var infoPantalla = DeviceDisplay.MainDisplayInfo;

        // Obtenemos ancho y alto en unidades independientes de dispositivo (dp)
        double anchoPantalla = infoPantalla.Width / infoPantalla.Density;
        double altoPantalla = infoPantalla.Height / infoPantalla.Density;

        // Calculamos el tamaño deseado (por ejemplo, 80% del ancho y 50% del alto)
        double anchoPopup = anchoPantalla * 0.9;
        double altoPopup = altoPantalla * 0.9;

        // Asignamos el tamaño al popup
        this.Size = new Size(anchoPopup, altoPopup);
    }

    private void ClickedBotonCerrar(object sender, EventArgs e)
    {
		Close();
    }
}
using JuegoMarvel.ModuloInicio.ViewModel;
using System.Threading.Tasks;

namespace JuegoMarvel.Views;

public partial class Inicio : ContentPage
{
	public Inicio(InicioViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
	}

    private async void OnHomeTapped(object sender, TappedEventArgs e)
    {
        var border = (Border)sender;
        // Animación: escalar al 80% y volver al 100%
        await border.ScaleTo(0.8, 100, Easing.CubicIn);
        await border.ScaleTo(1.0, 100, Easing.CubicOut);
    }

    private async void OnTiendaTapped(object sender, TappedEventArgs e)
    {
        var border = (Border)sender;
        // Animación: escalar al 80% y volver al 100%
        await border.ScaleTo(0.8, 100, Easing.CubicIn);
        await border.ScaleTo(1.0, 100, Easing.CubicOut);

        if (BindingContext is InicioViewModel vm)
        {
            vm.ComandoNavTienda.Execute("Tienda");
        }
    }
    private async void OnEquipoTapped(object sender, TappedEventArgs e)
    {
        var border = (Border)sender;
        // Animación: escalar al 80% y volver al 100%
        await border.ScaleTo(0.8, 100, Easing.CubicIn);
        await border.ScaleTo(1.0, 100, Easing.CubicOut);
        if (BindingContext is InicioViewModel vm)
        {
            vm.ComandoNavTienda.Execute("Equipo");
        }
    }
    private async void OnEmpezarTapped(object sender, TappedEventArgs e)
    {
        var border = (Border)sender;
        // Animación: escalar al 80% y volver al 100%
        await border.ScaleTo(0.8, 100, Easing.CubicIn);
        await border.ScaleTo(1.0, 100, Easing.CubicOut);
    }

    private async void OnAyudaTapped(object sender, EventArgs e)
    {
        var imageButton = (ImageButton)sender;
        // Animación: escalar al 80% y volver al 100%
        await imageButton.ScaleTo(0.8, 100, Easing.CubicIn);
        await imageButton.ScaleTo(1.0, 100, Easing.CubicOut);
    }

    private async void OnConfigTapped(object sender, EventArgs e)
    {
        var imageButton = (ImageButton)sender;
        // Animación: escalar al 80% y volver al 100%
        await imageButton.ScaleTo(0.8, 100, Easing.CubicIn);
        await imageButton.ScaleTo(1.0, 100, Easing.CubicOut);
    }
}
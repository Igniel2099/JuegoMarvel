using JuegoMarvel.ModuloInicio.ViewModel;
using System.Threading.Tasks;

namespace JuegoMarvel.Views;

public partial class Inicio : ContentPage
{

    public Inicio()
    {
        InitializeComponent();

        if (!DesignMode.IsDesignModeEnabled)
        {
            // Solo asigna el ViewModel en tiempo de ejecución, no en diseño
            BindingContext = new InicioViewModel();
        }
    }

    public Inicio(InicioViewModel vm) : this()
    {
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
            vm.ComandoNav.Execute("Tienda");
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
            vm.ComandoNav.Execute("Equipo");
        }
    }
    private async void OnEmpezarTapped(object sender, EventArgs e)
    {
        var border = (Button)sender;
        // Animación: escalar al 80% y volver al 100%
        await border.ScaleTo(0.8, 100, Easing.CubicIn);
        await border.ScaleTo(1.0, 100, Easing.CubicOut);
        if (BindingContext is InicioViewModel vm)
        {
            vm.ComandoNav.Execute("Empezar");
        }
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
        if (BindingContext is InicioViewModel vm)
        {
            vm.ComandoNav.Execute("Configuración");
        }
    }
}
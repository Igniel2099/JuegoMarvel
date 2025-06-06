using JuegoMarvel.ModuloInicio.ViewModel;

namespace JuegoMarvel.Views;

/// <summary>
/// Página principal de la aplicación que muestra el menú de inicio y gestiona la navegación y animaciones de los controles.
/// </summary>
public partial class Inicio : ContentPage
{
    /// <summary>
    /// Inicializa una nueva instancia de <see cref="Inicio"/> y asigna el ViewModel por defecto si no está en modo diseño.
    /// </summary>
    public Inicio()
    {
        InitializeComponent();

        if (!DesignMode.IsDesignModeEnabled)
        {
            // Solo asigna el ViewModel en tiempo de ejecución, no en diseño
            BindingContext = new InicioViewModel();
        }
    }

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="Inicio"/> con el ViewModel proporcionado.
    /// </summary>
    /// <param name="vm">Instancia de <see cref="InicioViewModel"/> para el binding de la vista.</param>
    public Inicio(InicioViewModel vm) : this()
    {
        BindingContext = vm;
    }

    /// <summary>
    /// Maneja el evento de tap sobre el botón de inicio (home) con animación.
    /// </summary>
    private async void OnHomeTapped(object sender, TappedEventArgs e)
    {
        var border = (Border)sender;
        // Animación: escalar al 80% y volver al 100%
        await border.ScaleTo(0.8, 100, Easing.CubicIn);
        await border.ScaleTo(1.0, 100, Easing.CubicOut);
    }

    /// <summary>
    /// Maneja el evento de tap sobre el botón de tienda, realiza animación y navega a la tienda.
    /// </summary>
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

    /// <summary>
    /// Maneja el evento de tap sobre el botón de equipo, realiza animación y navega a la pantalla de equipo.
    /// </summary>
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

    /// <summary>
    /// Maneja el evento de tap sobre el botón de empezar, realiza animación y navega a la pantalla de juego.
    /// </summary>
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

    /// <summary>
    /// Maneja el evento de tap sobre el botón de ayuda, realiza animación.
    /// </summary>
    private async void OnAyudaTapped(object sender, EventArgs e)
    {
        var imageButton = (ImageButton)sender;
        // Animación: escalar al 80% y volver al 100%
        await imageButton.ScaleTo(0.8, 100, Easing.CubicIn);
        await imageButton.ScaleTo(1.0, 100, Easing.CubicOut);
    }

    /// <summary>
    /// Maneja el evento de tap sobre el botón de configuración, realiza animación y navega a la pantalla de configuración.
    /// </summary>
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
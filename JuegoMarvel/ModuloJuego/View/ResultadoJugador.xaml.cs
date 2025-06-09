using JuegoMarvel.ModuloInicio.ViewModel;
using JuegoMarvel.ModuloJuego.ViewModel;
using JuegoMarvel.Views;

namespace JuegoMarvel.ModuloJuego.View;

public partial class ResultadoJugador : ContentPage
{
	public ResultadoJugador(string estadoJugador)
	{
		InitializeComponent();
		BindingContext = new ResultadoJugadorViewModel(estadoJugador);
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Cancelamos la animación al cambiar de página
        Thread.Sleep(3000); // Esperar 1 segundo antes de cambiar de página
        var inicioVm = ((App)Application.Current).Services.GetRequiredService<InicioViewModel>();
        var inicioPage = new Inicio(inicioVm);
        NavigationPage.SetHasNavigationBar(inicioPage, false);
        Application.Current.MainPage = new NavigationPage(inicioPage);
    }

}
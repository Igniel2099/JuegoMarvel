using JuegoMarvel.ModuloLogin.ViewModel;

namespace JuegoMarvel.ModuloLogin.View;

public partial class OlvidarInformacion : ContentPage
{
	public OlvidarInformacion(OlvidarInformacionViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}
using JuegoMarvel.ModuloLogin.ViewModel;

namespace JuegoMarvel.ModuloLogin.View;

public partial class CambiarContrasena : ContentPage
{
	public CambiarContrasena(CambiarContrasenaViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}
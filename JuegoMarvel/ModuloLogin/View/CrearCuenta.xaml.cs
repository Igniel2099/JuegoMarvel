using JuegoMarvel.ModuloLogin.ViewModel;

namespace JuegoMarvel.ModuloLogin.View;

public partial class CrearCuenta : ContentPage
{
	public CrearCuenta(CrearCuentaViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }
}
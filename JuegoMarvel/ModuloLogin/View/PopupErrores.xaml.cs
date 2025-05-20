using CommunityToolkit.Maui.Views;
using JuegoMarvel.ModuloLogin.ViewModel;

namespace JuegoMarvel.ModuloLogin.View;

public partial class PopupErrores : Popup
{
	public PopupErrores(PopupErroresViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

    private void ClickedBotonCerrar(object sender, EventArgs e)
    {
		Close();
    }
}
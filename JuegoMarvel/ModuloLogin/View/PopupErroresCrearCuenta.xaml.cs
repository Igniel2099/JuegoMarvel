using CommunityToolkit.Maui.Views;

namespace JuegoMarvel.ModuloLogin.View;

public partial class PopupErroresCrearCuenta : Popup
{
	public PopupErroresCrearCuenta()
	{
		InitializeComponent();
	}

    private void ClickedBotonCerrar(object sender, EventArgs e)
    {
		Close();
    }
}
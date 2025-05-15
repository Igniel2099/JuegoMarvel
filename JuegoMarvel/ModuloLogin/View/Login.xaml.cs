using JuegoMarvel.ModuloLogin.ViewModel;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace JuegoMarvel.Views;

public partial class Login : ContentPage
{
	public Login(LoginViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

    private  void OnConectarClicked(object sender, EventArgs e)
    {
    }

}
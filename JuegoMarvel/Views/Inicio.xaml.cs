using System.Threading.Tasks;

namespace JuegoMarvel.Views;

public partial class Inicio : ContentPage
{
	public Inicio()
	{
		InitializeComponent();
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        // Push sin animación nativa
        var window = Application.Current.Windows[0];      // para apps de una sola ventana
        var nav = window.Page.Navigation;

        // Para hacer el PushModal sin animación nativa:
        await nav.PushModalAsync(new Tienda(), false);
    }
}
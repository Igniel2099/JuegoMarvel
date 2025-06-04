using JuegoMarvel.ModuloEquipo.ViewModel;

namespace JuegoMarvel.ModuloEquipo.View;

public partial class EquipoView : ContentPage
{
    public EquipoView(EquipoViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage
            .Navigation
            .PopModalAsync(false);
    }
}
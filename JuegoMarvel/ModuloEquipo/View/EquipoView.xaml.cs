using JuegoMarvel.ModuloEquipo.ViewModel;

namespace JuegoMarvel.ModuloEquipo.View;

public partial class EquipoView : ContentPage
{
    public EquipoView(EquipoViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}
using JuegoMarvel.ModuloEquipo.ViewModel;

namespace JuegoMarvel.ModuloEquipo.View;

public partial class Equipo : ContentPage
{
	public Equipo(EquipoViewModel vm)
	{
		InitializeComponent();

		BindingContext = vm;
	}
}
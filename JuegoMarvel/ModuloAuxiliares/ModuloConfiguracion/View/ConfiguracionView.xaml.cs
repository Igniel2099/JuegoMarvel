using JuegoMarvel.ModuloAuxiliares.ModuloConfiguracion.ViewModel;

namespace JuegoMarvel.ModuloAuxiliares.ModuloConfiguracion.View;

public partial class ConfiguracionView : ContentPage
{
	public ConfiguracionView(ConfiguracionViewModel  vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}
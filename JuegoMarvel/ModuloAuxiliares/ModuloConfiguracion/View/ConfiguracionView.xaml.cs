using JuegoMarvel.ModuloAuxiliares.ModuloConfiguracion.ViewModel;

namespace JuegoMarvel.ModuloAuxiliares.ModuloConfiguracion.View;

/// <summary>
/// P�gina de configuraci�n de la aplicaci�n.
/// </summary>
public partial class ConfiguracionView : ContentPage
{
    /// <summary>
    /// Inicializa una nueva instancia de <see cref="ConfiguracionView"/> con el ViewModel proporcionado.
    /// </summary>
    /// <param name="vm">Instancia de <see cref="ConfiguracionViewModel"/> para el binding de la vista.</param>
    public ConfiguracionView(ConfiguracionViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
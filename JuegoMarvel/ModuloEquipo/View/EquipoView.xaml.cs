using JuegoMarvel.ModuloEquipo.ViewModel;

namespace JuegoMarvel.ModuloEquipo.View;

/// <summary>
/// P�gina de contenido para la gesti�n y visualizaci�n del equipo de personajes.
/// </summary>
public partial class EquipoView : ContentPage
{
    /// <summary>
    /// Inicializa una nueva instancia de <see cref="EquipoView"/> con el ViewModel proporcionado.
    /// </summary>
    /// <param name="vm">Instancia de <see cref="EquipoViewModel"/> para el binding de la vista.</param>
    public EquipoView(EquipoViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
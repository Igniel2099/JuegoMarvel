using JuegoMarvel.ModuloTienda.ViewModel;
using JuegoMarvel.Services;

namespace JuegoMarvel.Views;

/// <summary>
/// Página de la tienda donde se muestran los personajes disponibles para comprar y sus detalles.
/// Permite la interacción con los filtros de la barra superior y la visualización de información adicional de los personajes.
/// </summary>
/// <remarks>
/// El <see cref="Tienda"/> utiliza un <see cref="TiendaViewModel"/> como contexto de datos, que contiene la lógica de negocio,
/// la lista de personajes disponibles, los comandos de navegación y los datos visuales necesarios para la vista.
/// Incluye métodos para gestionar la animación y selección de filtros, así como la animación de los controles de información.
/// </remarks>
public partial class Tienda : ContentPage
{
    private readonly TiendaViewModel _vm;

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="Tienda"/> con el ViewModel proporcionado.
    /// </summary>
    /// <param name="vm">Instancia de <see cref="TiendaViewModel"/> que gestiona la lógica y el estado de la tienda.</param>
    public Tienda(TiendaViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    /// <summary>
    /// Maneja el evento de tap sobre los filtros de la barra superior (por tipo, grupo, etc.).
    /// Aplica la animación visual y actualiza la selección del filtro.
    /// </summary>
    /// <param name="sender">El grid del filtro seleccionado.</param>
    /// <param name="e">Argumentos del evento.</param>
    private void OnGridTapped(object sender, EventArgs e)
    {
        Grid gridTocado = (Grid)sender;
        List<Grid> GridsBarMenuTop = [GridAll, GridTipo, GridGrupo];

        AnimacionesComunes.CambiarImagenGrid(GridsBarMenuTop, gridTocado, "bar_seleccion.png");
        AnimacionesComunes.BorrarImagenGrid(GridsBarMenuTop);
        AnimacionesComunes.AnimacionImagen(gridTocado);
    }

    /// <summary>
    /// Maneja el evento de tap sobre el icono de información de un personaje.
    /// Aplica la animación visual correspondiente.
    /// </summary>
    /// <param name="sender">El grid de información seleccionado.</param>
    /// <param name="e">Argumentos del evento.</param>
    private async void OnInformationTapped(object sender, EventArgs e)
    {
        Grid gridInformación = (Grid)sender;
        AnimacionesComunes.AnimacionImagen(gridInformación);
        // Aquí se podría enlazar el comando para mostrar el popup de información del personaje.
    }
}
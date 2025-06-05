using JuegoMarvel.ModuloTienda.ViewModel;
using JuegoMarvel.Services;

namespace JuegoMarvel.Views;

public partial class Tienda : ContentPage
{
    private readonly TiendaViewModel _vm;
    public Tienda(TiendaViewModel vm)
	{
		InitializeComponent();
        _vm = vm;
        BindingContext = vm;
    }

    private void OnGridTapped(object sender, EventArgs e)
    {
        Grid gridTocado = (Grid)sender;
        List<Grid> GridsBarMenuTop = [GridAll, GridTipo, GridGrupo];

        AnimacionesComunes.CambiarImagenGrid(GridsBarMenuTop, gridTocado,"bar_seleccion.png");

        AnimacionesComunes.BorrarImagenGrid(GridsBarMenuTop);

        AnimacionesComunes.AnimacionImagen(gridTocado);
    }

    private async void OnInformationTapped(object sender, EventArgs e)
    {
        Grid gridInformación = (Grid)sender;

        AnimacionesComunes.AnimacionImagen(gridInformación);
        //  Deneria Enlazarlo para hacer la animación y desde aqui lanzar el comando pero bueno
    }
}
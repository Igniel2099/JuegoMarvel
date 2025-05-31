using JuegoMarvel.ModuloTienda.ViewModel;
using JuegoMarvel.Services;

namespace JuegoMarvel.Views;

public partial class Tienda : ContentPage
{
    public Tienda(TiendaViewModel vm)
	{
		InitializeComponent();

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

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await Application.Current.MainPage
            .Navigation
            .PopModalAsync(false);
    }
}
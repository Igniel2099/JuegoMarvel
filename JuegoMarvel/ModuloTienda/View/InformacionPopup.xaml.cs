using CommunityToolkit.Maui.Views;
using JuegoMarvel.ModuloTienda.ViewModel;
using JuegoMarvel.Services;

namespace JuegoMarvel.Views;

/// <summary>
/// Popup personalizado para mostrar información detallada de un personaje y sus habilidades en la tienda.
/// Permite visualizar imágenes, animaciones y detalles de habilidades, así como interactuar con controles de animación.
/// </summary>
/// <remarks>
/// El <see cref="InformacionPopup"/> utiliza un <see cref="InformacionPoupViewModel"/> como contexto de datos, que contiene
/// la información visual y lógica de las habilidades y animaciones del personaje seleccionado.
/// Incluye métodos para ajustar el tamaño dinámicamente, gestionar la animación de habilidades y responder a la interacción del usuario.
/// </remarks>
public partial class InformacionPopup : Popup
{
    private readonly InformacionPoupViewModel _vm;

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="InformacionPopup"/> con el ViewModel proporcionado.
    /// </summary>
    /// <param name="vm">Instancia de <see cref="InformacionPoupViewModel"/> que gestiona la lógica y el estado del popup.</param>
    public InformacionPopup(InformacionPoupViewModel vm)
    {
        InitializeComponent();
        _vm = vm;
        BindingContext = vm;
        CalcularTamañoDinamico();
    }

    /// <summary>
    /// Calcula y asigna el tamaño del popup en función del tamaño de la pantalla del dispositivo.
    /// </summary>
    private void CalcularTamañoDinamico()
    {
        var infoPantalla = DeviceDisplay.MainDisplayInfo;
        double anchoPantalla = infoPantalla.Width / infoPantalla.Density;
        double altoPantalla = infoPantalla.Height / infoPantalla.Density;
        double anchoPopup = anchoPantalla * 0.9;
        double altoPopup = altoPantalla * 0.9;
        this.Size = new Size(anchoPopup, altoPopup);
    }

    /// <summary>
    /// Evento que se ejecuta al pulsar el botón de cerrar el popup.
    /// Realiza una animación de escala sobre el botón antes de cerrar el popup.
    /// </summary>
    /// <param name="sender">El botón de imagen que dispara el evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    private async void ClickedBotonCerrar(object sender, EventArgs e)
    {
        ImageButton imgButton = (ImageButton)sender;
        await imgButton.ScaleTo(0.9, 100, Easing.CubicIn);
        await imgButton.ScaleTo(1, 100, Easing.CubicIn);
        Close();
    }

    /// <summary>
    /// Evento que se ejecuta al pulsar el botón de play/pausa de la animación.
    /// Cambia el icono y ejecuta el comando correspondiente en el ViewModel.
    /// </summary>
    /// <param name="sender">La imagen que actúa como botón de play/pausa.</param>
    /// <param name="e">Argumentos del evento.</param>
    private async void TappedPlayOrPause(object sender, TappedEventArgs e)
    {
        Image img = (Image)sender;
        await img.ScaleTo(0.9, 100, Easing.CubicIn);
        await img.ScaleTo(1, 100, Easing.CubicIn);
        if (img.Source is FileImageSource fileImgSource)
        {
            if (fileImgSource.File == "icono_play.png")
            {
                fileImgSource.File = "icono_pausa.png";
                _vm.EmpezarAnimacion.Execute(null);
            }
            else
            {
                fileImgSource.File = "icono_play.png";
                _vm.TerminarAnimacion.Execute(null);
            }
        }
    }

    /// <summary>
    /// Evento que se ejecuta al seleccionar una habilidad en la barra superior.
    /// Cambia la habilidad seleccionada, actualiza la imagen y ejecuta animaciones visuales.
    /// </summary>
    /// <param name="sender">El grid de la habilidad seleccionada.</param>
    /// <param name="e">Argumentos del evento.</param>
    private void TappedGridHabilidad(object sender, TappedEventArgs e)
    {
        Grid gridTocado = (Grid)sender;
        _vm.NombreHabilidadSeleccionada = gridTocado.Children.OfType<Label>().FirstOrDefault().Text;
        List<Grid> GridsBarMenuTop = [GridPrimeraHabilidad, GridSegundaHabilidad, GridTerceraHabilidad];

        AnimacionesComunes.CambiarImagenGrid(
            GridsBarMenuTop,
            gridTocado,
            "boton_seleccionado_habilidad.png"
        );

        AnimacionesComunes.BorrarImagenGrid(GridsBarMenuTop);

        AnimacionesComunes.AnimacionImagen(gridTocado);
    }
}
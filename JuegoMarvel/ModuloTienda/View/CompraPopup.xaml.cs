using CommunityToolkit.Maui.Views;
using JuegoMarvel.ModuloTienda.ViewModel;

namespace JuegoMarvel.ModuloTienda.View;

/// <summary>
/// Popup personalizado para mostrar la información y confirmación de compra de un personaje en la tienda.
/// Ajusta su tamaño dinámicamente según la pantalla y permite cerrar el popup mediante un botón con animación.
/// </summary>
/// <remarks>
/// El <see cref="CompraPopup"/> utiliza un <see cref="CompraViewModel"/> como contexto de datos, que contiene la información
/// del personaje a comprar, el comando de compra y los datos visuales necesarios para la vista.
/// </remarks>
public partial class CompraPopup : Popup
{
    /// <summary>
    /// Inicializa una nueva instancia de <see cref="CompraPopup"/> con el ViewModel proporcionado.
    /// </summary>
    /// <param name="vm">Instancia de <see cref="CompraViewModel"/> que gestiona la lógica y el estado del popup de compra.</param>
    public CompraPopup(CompraViewModel vm)
    {
        InitializeComponent();
        CalcularTamañoDinamico();
        BindingContext = vm;
    }

    /// <summary>
    /// Calcula y asigna el tamaño del popup en función del tamaño de la pantalla del dispositivo.
    /// </summary>
    private void CalcularTamañoDinamico()
    {
        // Obtenemos la info de la pantalla
        var infoPantalla = DeviceDisplay.MainDisplayInfo;

        // Obtenemos ancho y alto en unidades independientes de dispositivo (dp)
        double anchoPantalla = infoPantalla.Width / infoPantalla.Density;
        double altoPantalla = infoPantalla.Height / infoPantalla.Density;

        // Calculamos el tamaño deseado (por ejemplo, 90% del ancho y 90% del alto)
        double anchoPopup = anchoPantalla * 0.9;
        double altoPopup = altoPantalla * 0.9;

        // Asignamos el tamaño al popup
        this.Size = new Size(anchoPopup, altoPopup);
    }

    /// <summary>
    /// Evento que se ejecuta al pulsar el botón de cerrar el popup.
    /// Realiza una animación de escala sobre el botón antes de cerrar el popup.
    /// </summary>
    /// <param name="sender">El botón de imagen que dispara el evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    public async void ClickedBotonCerrar(object sender, EventArgs e)
    {
        ImageButton imgButton = (ImageButton)sender;
        await imgButton.ScaleTo(0.9, 100, Easing.CubicIn); // Escala al 90% en 100ms
        await imgButton.ScaleTo(1, 100, Easing.CubicIn);   // Vuelve al 100% en 100ms

        Close();
    }
}
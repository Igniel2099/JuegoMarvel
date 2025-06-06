using CommunityToolkit.Maui.Views;
using JuegoMarvel.ModuloLogin.ViewModel;

namespace JuegoMarvel.ModuloLogin.View;

/// <summary>
/// Popup personalizado para mostrar mensajes de error al usuario.
/// Ajusta su tamaño dinámicamente según la pantalla y permite cerrar el popup mediante un botón.
/// </summary>
public partial class PopupErrores : Popup
{
    /// <summary>
    /// Inicializa una nueva instancia de <see cref="PopupErrores"/> con el ViewModel proporcionado.
    /// </summary>
    /// <param name="vm">Instancia de <see cref="PopupErroresViewModel"/> que gestiona los mensajes y el estado del popup.</param>
    public PopupErrores(PopupErroresViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
        CalcularTamañoDinamico();
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
    /// </summary>
    /// <param name="sender">Objeto que dispara el evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    private void ClickedBotonCerrar(object sender, EventArgs e)
    {
        Close();
    }
}
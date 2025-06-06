using JuegoMarvel.ModuloLogin.ViewModel;

namespace JuegoMarvel.ModuloLogin.View;

/// <summary>
/// P�gina de contenido para la recuperaci�n de informaci�n de usuario (por ejemplo, restablecimiento de contrase�a).
/// Asocia el <see cref="OlvidarInformacionViewModel"/> como contexto de datos para gestionar el flujo de recuperaci�n,
/// incluyendo la introducci�n del correo electr�nico, el env�o del c�digo de confirmaci�n y la validaci�n del mismo.
/// </summary>
public partial class OlvidarInformacion : ContentPage
{
    /// <summary>
    /// Inicializa una nueva instancia de <see cref="OlvidarInformacion"/> con el ViewModel proporcionado.
    /// </summary>
    /// <param name="viewModel">Instancia de <see cref="OlvidarInformacionViewModel"/> que gestiona la l�gica y el estado de la vista.</param>
    public OlvidarInformacion(OlvidarInformacionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
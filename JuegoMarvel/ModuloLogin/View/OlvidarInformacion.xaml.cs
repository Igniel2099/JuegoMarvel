using JuegoMarvel.ModuloLogin.ViewModel;

namespace JuegoMarvel.ModuloLogin.View;

/// <summary>
/// Página de contenido para la recuperación de información de usuario (por ejemplo, restablecimiento de contraseña).
/// Asocia el <see cref="OlvidarInformacionViewModel"/> como contexto de datos para gestionar el flujo de recuperación,
/// incluyendo la introducción del correo electrónico, el envío del código de confirmación y la validación del mismo.
/// </summary>
public partial class OlvidarInformacion : ContentPage
{
    /// <summary>
    /// Inicializa una nueva instancia de <see cref="OlvidarInformacion"/> con el ViewModel proporcionado.
    /// </summary>
    /// <param name="viewModel">Instancia de <see cref="OlvidarInformacionViewModel"/> que gestiona la lógica y el estado de la vista.</param>
    public OlvidarInformacion(OlvidarInformacionViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
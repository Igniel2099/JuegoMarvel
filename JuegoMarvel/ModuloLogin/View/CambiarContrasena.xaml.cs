using JuegoMarvel.ModuloLogin.ViewModel;

namespace JuegoMarvel.ModuloLogin.View;

/// <summary>
/// Página de contenido para el cambio de contraseña de usuario.
/// Asocia el <see cref="CambiarContrasenaViewModel"/> como contexto de datos para gestionar la lógica y el estado de la vista.
/// </summary>
public partial class CambiarContrasena : ContentPage
{
    /// <summary>
    /// Inicializa una nueva instancia de <see cref="CambiarContrasena"/> con el ViewModel proporcionado.
    /// </summary>
    /// <param name="vm">Instancia de <see cref="CambiarContrasenaViewModel"/> que gestiona la lógica de la vista.</param>
    public CambiarContrasena(CambiarContrasenaViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
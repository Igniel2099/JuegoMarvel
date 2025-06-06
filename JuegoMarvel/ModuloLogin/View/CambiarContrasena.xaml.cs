using JuegoMarvel.ModuloLogin.ViewModel;

namespace JuegoMarvel.ModuloLogin.View;

/// <summary>
/// P�gina de contenido para el cambio de contrase�a de usuario.
/// Asocia el <see cref="CambiarContrasenaViewModel"/> como contexto de datos para gestionar la l�gica y el estado de la vista.
/// </summary>
public partial class CambiarContrasena : ContentPage
{
    /// <summary>
    /// Inicializa una nueva instancia de <see cref="CambiarContrasena"/> con el ViewModel proporcionado.
    /// </summary>
    /// <param name="vm">Instancia de <see cref="CambiarContrasenaViewModel"/> que gestiona la l�gica de la vista.</param>
    public CambiarContrasena(CambiarContrasenaViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
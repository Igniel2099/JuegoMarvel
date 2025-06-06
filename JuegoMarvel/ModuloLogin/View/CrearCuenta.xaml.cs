using JuegoMarvel.ModuloLogin.ViewModel;

namespace JuegoMarvel.ModuloLogin.View;

/// <summary>
/// P�gina de contenido para el registro de un nuevo usuario.
/// Asocia el <see cref="CrearCuentaViewModel"/> como contexto de datos para gestionar la l�gica, validaciones y comandos de la vista.
/// </summary>
public partial class CrearCuenta : ContentPage
{
    /// <summary>
    /// Inicializa una nueva instancia de <see cref="CrearCuenta"/> con el ViewModel proporcionado.
    /// </summary>
    /// <param name="vm">Instancia de <see cref="CrearCuentaViewModel"/> que gestiona la l�gica y el estado de la vista.</param>
    public CrearCuenta(CrearCuentaViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }
}
using JuegoMarvel.ClasesBase;

namespace JuegoMarvel.ModuloEquipo.ViewModel.Comandos;

/// <summary>
/// Comando para seleccionar un personaje de usuario en la vista de equipo.
/// Cambia la imagen de fondo del contenedor seleccionado y actualiza la selección en el ViewModel.
/// </summary>
public class ComandoSeleccionarPersonaje(
    EquipoViewModel vm,
    PersonajeUsuarioViewModel perUsViewModel) : BaseCommand
{
    /// <summary>
    /// Propiedad privada que contiene el ViewModel del Equipo
    /// </summary>
    private readonly EquipoViewModel _viewModel = vm;

    /// <summary>
    /// Propiedad Privada que contiene al view model del Personaje Usuario.
    /// </summary>
    private readonly PersonajeUsuarioViewModel _perUsViewModel = perUsViewModel;

    /// <summary>
    /// Ejecuta la lógica de selección de personaje, aplicando la animación visual correspondiente.
    /// </summary>
    /// <param name="parameter">Debe ser un objeto de tipo <see cref="Grid"/> que representa el contenedor visual seleccionado.</param>
    public override void Execute(object? parameter)
    {
        ComprobacionGenerarAnimacion(parameter);
    }

    /// <summary>
    /// Comprueba el parámetro recibido y ejecuta la animación de selección si es válido.
    /// </summary>
    /// <param name="parameter">El parámetro recibido, esperado de tipo <see cref="Grid"/>.</param>
    /// <exception cref="Exception">Se lanza si el parámetro no es un Grid válido.</exception>
    private void ComprobacionGenerarAnimacion(object? parameter)
    {
        if (parameter is Grid gridTocado)
            AnimacionSeleccionGrid(gridTocado);
        else
            throw new Exception("Se esta registrando un Control no valido");
    }

    /// <summary>
    /// Aplica la animación visual de selección al grid y actualiza la selección en el ViewModel.
    /// </summary>
    /// <param name="gridTocado">El grid que ha sido tocado/seleccionado.</param>
    private void AnimacionSeleccionGrid(Grid gridTocado)
    {
        if (_viewModel.PersonajeUsuarioSeleccionado != null)
            _viewModel.PersonajeUsuarioSeleccionado.ImagenContenedor = "fondo_contenedor_equipo.png";

        _perUsViewModel.ImagenContenedor = "fondo_seleccion_equipo.png";

        _viewModel.PersonajeUsuarioSeleccionado = _perUsViewModel;
    }
}
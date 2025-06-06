using JuegoMarvel.ClasesBase;

namespace JuegoMarvel.ModuloTienda.ViewModel.Comandos;

/// <summary>
/// Comando para detener la animación de la habilidad de un personaje en la vista de información.
/// Restaura la imagen principal del personaje y reinicia el desplazamiento visual.
/// Detiene el temporizador de animación si está en ejecución.
/// </summary>
/// <remarks>
/// Utiliza <see cref="InformacionPoupViewModel"/> para actualizar la vista y <see cref="IDispatcherTimer"/> para controlar el tiempo de la animación.
/// Este comando suele usarse junto con <see cref="ComandoEmpezarAnimación"/> para gestionar el ciclo de animación de habilidades.
/// </remarks>
public class ComandoTerminarAnimacion(
    InformacionPoupViewModel viewModel,
    IDispatcherTimer? timer
    ) : BaseCommand
{

    /// <summary>
    /// Propiedad privada de la Informacion ViewModel
    /// </summary>
    private readonly InformacionPoupViewModel _viewModel = viewModel;

    /// <summary>
    /// Propiedad privada del Timer que controla los tiempos.
    /// </summary>
    private readonly IDispatcherTimer? _timer = timer;

    /// <summary>
    /// Ejecuta el comando para detener la animación, restaurando la imagen principal y reiniciando el eje X.
    /// Si el temporizador está en ejecución, lo detiene.
    /// </summary>
    /// <param name="parameter">No se utiliza.</param>
    public override void Execute(object? parameter)
    {
        _viewModel.ImgPlay = _viewModel.ImgCuerpo;
        _viewModel.EjeX = 0;
        if (_timer.IsRunning)
            _timer.Stop();
    }
}
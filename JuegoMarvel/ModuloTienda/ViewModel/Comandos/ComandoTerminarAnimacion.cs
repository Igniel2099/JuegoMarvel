
namespace JuegoMarvel.ModuloTienda.ViewModel.Comandos;

public class ComandoTerminarAnimacion(
    InformacionPoupViewModel viewModel,
    IDispatcherTimer? timer
    ) : BaseCommand
{
    private readonly InformacionPoupViewModel _viewModel = viewModel;
    private IDispatcherTimer? _timer = timer;
    public override void Execute(object? parameter)
    {
        _viewModel.ImgPlay = _viewModel.ImgCuerpo;
        _viewModel.EjeX = 0;
        if (_timer.IsRunning)
            _timer.Stop();

    }
}

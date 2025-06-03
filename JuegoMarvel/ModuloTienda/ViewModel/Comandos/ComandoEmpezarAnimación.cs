using JuegoMarvel.ModuloTienda.Model;

namespace JuegoMarvel.ModuloTienda.ViewModel.Comandos;

public class ComandoEmpezarAnimación : BaseCommand
{
    private readonly PersonajeImg _personajeImg;
    private int _indiceActual;

    public event EventHandler<string> EventoSeleccionadaCambio;

    private string _nombreHabilidadSeleccionada;

    private IDispatcherTimer? _timer;



    private readonly InformacionPoupViewModel _informacionVm;
    public ComandoEmpezarAnimación(
        PersonajeImg personajeImg,
        string nombreHabilidad,
        InformacionPoupViewModel informacionVm,
        IDispatcherTimer? timer)
    {
        _personajeImg = personajeImg;
        _indiceActual = 0;
        _nombreHabilidadSeleccionada = nombreHabilidad;
        EventoSeleccionadaCambio += CambiarPropiedad;
        _informacionVm = informacionVm;
        _timer = timer;
    }

    public override void Execute(object? parameter)
    {
        InicializarAnimarPersonaje();
        if (!_timer.IsRunning)
            _timer.Start();
    }

    private void InicializarAnimarPersonaje()
    {
        _indiceActual = 0;
        _timer.Tick -= OnTimerTick;
        _timer.Tick += OnTimerTick;
    }

    private void OnTimerTick(object sender, EventArgs e)
    {
        // Avanzar al siguiente fotograma (cíclicamente)
        HabilidadImg habilidadImg = _personajeImg.Habilidades.FirstOrDefault(h => h.Nombre == _nombreHabilidadSeleccionada);
        _indiceActual = (_indiceActual + 1) % habilidadImg.Sprites.Count;
        _informacionVm.ImgPlay = habilidadImg.Sprites[_indiceActual].Path;
    }

    public void OnSeleccionadaCambio(string nuevaSeleccion)
    {
        EventoSeleccionadaCambio?.Invoke(this, nuevaSeleccion);
    }

    public void CambiarPropiedad(object? sender, string nuevaSeleccion)
    {
        if (!_timer.IsRunning)
            _nombreHabilidadSeleccionada = nuevaSeleccion;
    }

}

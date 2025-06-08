using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloTienda.Model;

namespace JuegoMarvel.ModuloJuego.ViewModel.Comandos
{
    public class ComandoPegar : BaseCommand
    {
        private const double MAX_EJE_X = 570;

        private readonly PersonajeImg _personajeImg;
        private readonly JuegoViewModel _juegoVm;
        private readonly double _posicionInicial;
        private string? _nombreHabilidadSeleccionada;
        private IDispatcherTimer? _timer;

        private List<CategoriaSprite>? _categoriasAnimables;
        private List<double>? _intervalosPorFrame;
        private List<double> _desplazamientosPorFrame = new();

        private int _categoriaIndice;
        private int _frameIndice;

        /// <summary>
        /// @param posicionInicial: valor de EjeXPersonajePropio al que volver al terminar
        /// </summary>
        public ComandoPegar(PersonajeImg personajeImg, JuegoViewModel juegoVm, double posicionInicial)
        {
            _personajeImg = personajeImg;
            _juegoVm = juegoVm;
            _posicionInicial = posicionInicial;
        }

        public override bool CanExecute(object? parameter) => true;

        public override void Execute(object? parameter)
        {
            if (parameter is not string habilidad)
                return;

            _nombreHabilidadSeleccionada = habilidad;

            // Asegurarnos de partir siempre de la posición inicial
            _juegoVm.EjeXPersonajePropio = _posicionInicial;

            InicializarAnimacion();

            if (_timer is not null && !_timer.IsRunning)
                _timer.Start();
        }

        private void InicializarAnimacion()
        {
            // Crear un timer propio
            _timer = Application
                        .Current!
                        .Dispatcher!
                        .CreateTimer();

            // Buscar la habilidad
            var habilidadImg = _personajeImg
                .Habilidades
                .FirstOrDefault(h => h.Nombre == _nombreHabilidadSeleccionada);

            if (habilidadImg?.Sprites == null)
                return;

            // Construir categorías
            var todas = new List<CategoriaSprite>
            {
                habilidadImg.Sprites.Estatico,
                habilidadImg.Sprites.Adelante,
                habilidadImg.Sprites.Ataque,
                habilidadImg.Sprites.Atras
            };

            _categoriasAnimables = new();
            _intervalosPorFrame = new();
            _desplazamientosPorFrame.Clear();

            foreach (var cat in todas)
            {
                if (cat.Frames?.Count > 0)
                {
                    _categoriasAnimables.Add(cat);

                    if (!double.TryParse(cat.Tiempo, out var tiempoTotal))
                        tiempoTotal = 0.1;

                    int cnt = cat.Frames.Count;
                    double intervalo = tiempoTotal / cnt;
                    _intervalosPorFrame.Add(intervalo);

                    if (cat == habilidadImg.Sprites.Adelante)
                        _desplazamientosPorFrame.Add(MAX_EJE_X / cnt);
                    else if (cat == habilidadImg.Sprites.Atras)
                        _desplazamientosPorFrame.Add(-MAX_EJE_X / cnt);
                    else
                        _desplazamientosPorFrame.Add(0.0);
                }
            }

            _categoriaIndice = 0;
            _frameIndice = 0;

            if (_intervalosPorFrame!.Any())
                _timer.Interval = TimeSpan.FromSeconds(_intervalosPorFrame[0]);

            _timer.Tick -= OnTimerTick;
            _timer.Tick += OnTimerTick;
            _timer.IsRepeating = true;
        }

        private void OnTimerTick(object? sender, EventArgs e)
        {
            if (_categoriasAnimables == null || !_categoriasAnimables.Any())
                return;

            _frameIndice++;
            var cat = _categoriasAnimables[_categoriaIndice];
            var frames = cat.Frames!;

            // Si llegamos al final de esta categoría:
            if (_frameIndice >= frames.Count)
            {
                _categoriaIndice++;

                // Si ya no hay más categorías, finalizamos:
                if (_categoriaIndice >= _categoriasAnimables.Count)
                {
                    // Volver a la posición inicial
                    _juegoVm.EjeXPersonajePropio = _posicionInicial;
                    // Y detener el timer
                    _timer?.Stop();
                    return;
                }

                // Si aún queda categoría siguiente, reseteamos frame y ajustamos intervalo
                _frameIndice = 0;
                double nuevoInt = _intervalosPorFrame![_categoriaIndice];
                _timer!.Interval = TimeSpan.FromSeconds(nuevoInt);

                cat = _categoriasAnimables[_categoriaIndice];
                frames = cat.Frames!;
            }

            // Actualizar imagen de frame y desplazar
            _juegoVm.ImgPersonajePropio = frames[_frameIndice].Path;
            double deltaX = _desplazamientosPorFrame[_categoriaIndice];
            _juegoVm.EjeXPersonajePropio += deltaX;
        }
    }
}

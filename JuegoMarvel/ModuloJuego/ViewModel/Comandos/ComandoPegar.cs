using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloTienda.Model;
using MensajesServidor;
using System.Threading;

namespace JuegoMarvel.ModuloJuego.ViewModel.Comandos
{
    public class ComandoPegar : BaseCommand
    {
        private const double MAX_EJE_X = 460;

        private readonly PersonajeImg _personajeImg;
        private readonly JuegoViewModel _juegoVm;
        private readonly double _posicionInicial;
        private readonly bool _direccionPositiva;

        private string? _nombreHabilidadSeleccionada;
        private IDispatcherTimer? _timer;

        private List<CategoriaSprite>? _categoriasAnimables;
        private List<double>? _intervalosPorFrame;
        private List<double> _desplazamientosPorFrame = [];

        private int _categoriaIndice;
        private int _frameIndice;


        public event EventHandler? AnimacionTerminada;

        /// <summary>
        /// @param posicionInicial: valor de EjeXPersonajePropio al que volver al terminar
        /// @param direccionPositiva: si true, fuerza deltaX >= 0; si false, fuerza deltaX <= 0
        /// </summary>
        public ComandoPegar(
            PersonajeImg personajeImg,
            JuegoViewModel juegoVm,
            double posicionInicial,
            bool direccionPositiva = true   
        )
        {
            _personajeImg = personajeImg;
            _juegoVm = juegoVm;
            _posicionInicial = posicionInicial;
            _direccionPositiva = direccionPositiva;
        }

        public override bool CanExecute(object? parameter) => true;

        public async override void Execute(object? parameter)
        {
            if (parameter is not string habilidad)
                return;

            _nombreHabilidadSeleccionada = habilidad;

            // Partir siempre de la posición inicial
            if (_direccionPositiva)
            {
                _juegoVm.EjeXPersonajePropio = _posicionInicial;
                
            }
            else
            {
                _juegoVm.EjeXPersonajeContrario = _posicionInicial;
            }

            InicializarAnimacion();

            if (_timer is not null && !_timer.IsRunning)
            {
                if (_direccionPositiva)
                {
                    await _juegoVm.Cliente.Enviar(new MensajesModuloJuego
                    {
                        TipoMensaje = EnumMensajeJuego.MandarHabilidad,
                        Valor = _nombreHabilidadSeleccionada
                    });
                }
                _timer.Start();
            }

        }

        private void InicializarAnimacion()
        {
            _timer = Application
                        .Current!
                        .Dispatcher!
                        .CreateTimer();

            var habilidadImg = _personajeImg
                .Habilidades
                .FirstOrDefault(h => h.Nombre == _nombreHabilidadSeleccionada);

            if (habilidadImg?.Sprites == null)
                return;

            var todas = new List<CategoriaSprite>
            {
                habilidadImg.Sprites.Estatico,
                habilidadImg.Sprites.Adelante,
                habilidadImg.Sprites.Ataque,
                habilidadImg.Sprites.Atras
            };

            _categoriasAnimables = [];
            _intervalosPorFrame = [];
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

                    int signoBase = cat == habilidadImg.Sprites.Adelante 
                        ? +1
                        : cat == habilidadImg.Sprites.Atras 
                        ? -1
                        : 0;
                    double desplazPorFrame = (MAX_EJE_X / cnt) * signoBase;
                    _desplazamientosPorFrame.Add(desplazPorFrame);
                }
            }

            _categoriaIndice = 0;
            _frameIndice = 0;

            if (_intervalosPorFrame.Any())
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

            // Si termina esta categoría, pasamos a la siguiente
            if (_frameIndice >= frames.Count)
            {
                _categoriaIndice++;
                if (_categoriaIndice >= _categoriasAnimables.Count)
                {
                    // Fin de animación: reset y stop

                    if(_direccionPositiva)
                        _juegoVm.EjeXPersonajePropio = _posicionInicial;
                    else
                        _juegoVm.EjeXPersonajeContrario = _posicionInicial;

                    _timer?.Stop();
                    if(_direccionPositiva)
                    {
                        if (_juegoVm.EscudoContraria > 0)
                            _juegoVm.EscudoContraria = Math.Max(0, _juegoVm.EscudoContraria - 0.5);
                        else
                            _juegoVm.VidaContraria = Math.Max(0, _juegoVm.VidaContraria - 0.5);
                    }
                    else
                    {
                        if (_juegoVm.EscudoPropio > 0)
                            _juegoVm.EscudoPropio = Math.Max(0, _juegoVm.EscudoPropio - 0.5);
                        else
                            _juegoVm.VidaPropia = Math.Max(0, _juegoVm.VidaPropia - 0.5);
                    }

                    AnimacionTerminada?.Invoke(this, EventArgs.Empty);
                    return;
                }

                _frameIndice = 0;
                _timer!.Interval = TimeSpan.FromSeconds(_intervalosPorFrame![_categoriaIndice]);
                cat = _categoriasAnimables[_categoriaIndice];
                frames = cat.Frames!;
            }

            if(_direccionPositiva)
                _juegoVm.ImgPersonajePropio = frames[_frameIndice].Path;
            else
                _juegoVm.ImagePersonajeContrario = frames[_frameIndice].Path;

            double baseDelta = _desplazamientosPorFrame[_categoriaIndice];
            double deltaX = _direccionPositiva
                ? baseDelta     
                : -baseDelta;   

            if (_direccionPositiva)
                _juegoVm.EjeXPersonajePropio += deltaX;
            else
                _juegoVm.EjeXPersonajeContrario += deltaX;
        }
    }
}

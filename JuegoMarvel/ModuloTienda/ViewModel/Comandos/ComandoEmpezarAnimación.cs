using System;
using System.Collections.Generic;
using System.Linq;
using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloTienda.Model;

namespace JuegoMarvel.ModuloTienda.ViewModel.Comandos;

/// <summary>
/// Comando para iniciar y gestionar la animación de las habilidades de un personaje en la vista de información.
/// Controla el avance de los frames de animación, el desplazamiento visual y la transición entre categorías de animación.
/// </summary>
/// <remarks>
/// Utiliza <see cref="PersonajeImg"/> para obtener los datos gráficos, <see cref="InformacionPoupViewModel"/> para actualizar la vista
/// y un <see cref="IDispatcherTimer"/> para controlar el tiempo de la animación. Permite cambiar la habilidad animada en tiempo real.
/// </remarks>
public class ComandoEmpezarAnimación : BaseCommand
{
    /// <summary>
    /// Propiedad privada constante de la distancia maxima a llegar en el Eje x. 
    /// </summary>
    private const double MAX_EJE_X = 170;

    /// <summary>
    /// Evento que se dispara cuando se selecciona una nueva habilidad para animar.
    /// </summary>
    public event EventHandler<string> EventoSeleccionadaCambio;

    /// <summary>
    /// Propiedad privada de las Imgenes del Persoanje para las animaciones
    /// </summary>
    private readonly PersonajeImg _personajeImg;

    /// <summary>
    /// Propiedad privada del Nombre de la Habilidad que ha sido seleccionada.
    /// </summary>
    private string _nombreHabilidadSeleccionada;

    /// <summary>
    /// Propiedad privada del Timer que controla el tiempo por intervalos.
    /// </summary>
    private IDispatcherTimer? _timer;

    /// <summary>
    /// Propiedad privada del ViewModel de la Información
    /// </summary>
    private readonly InformacionPoupViewModel _informacionVm;

    // Lista de categorías (Estatico, Adelante, Ataque, Atras) que tienen al menos un frame
    private List<CategoriaSprite>? _categoriasAnimables;

    /// <summary>
    /// Propiedad privada de la lista del tiempo de los intervalos por frame
    /// </summary>
    private List<double>? _intervalosPorFrame;

    /// <summary>
    /// Propiedad privada de la lista de la distantida de los desplazamientos por frame.
    /// </summary>
    private List<double> _desplazamientosPorFrame;

    // Índices para llevar la cuenta de en qué categoría y en qué frame estamos
    private int _categoriaIndice;
    private int _frameIndice;

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="ComandoEmpezarAnimación"/>.
    /// </summary>
    /// <param name="personajeImg">Datos gráficos del personaje a animar.</param>
    /// <param name="nombreHabilidad">Nombre de la habilidad seleccionada para animar.</param>
    /// <param name="informacionVm">ViewModel de la vista de información donde se muestra la animación.</param>
    /// <param name="timer">Temporizador para controlar el avance de la animación.</param>
    public ComandoEmpezarAnimación(
        PersonajeImg personajeImg,
        string nombreHabilidad,
        InformacionPoupViewModel informacionVm,
        IDispatcherTimer? timer)
    {
        _personajeImg = personajeImg;
        _nombreHabilidadSeleccionada = nombreHabilidad;
        EventoSeleccionadaCambio += CambiarPropiedad;
        _informacionVm = informacionVm;
        _timer = timer;
    }

    /// <summary>
    /// Ejecuta el comando, inicializando la animación y comenzando el temporizador.
    /// </summary>
    /// <param name="parameter">No se utiliza.</param>
    public override void Execute(object? parameter)
    {
        InicializarAnimarPersonaje();
        if (_timer is not null && !_timer.IsRunning)
            _timer.Start();
    }

    /// <summary>
    /// Inicializa los parámetros y el estado necesarios para comenzar la animación del personaje.
    /// Calcula los intervalos y desplazamientos por frame según la habilidad seleccionada.
    /// </summary>
    private void InicializarAnimarPersonaje()
    {
        // Reset de índices
        _categoriaIndice = 0;
        _frameIndice = -1; // Se incrementará a 0 en el primer tick

        // Desuscribir evento previo y volver a suscribir
        if (_timer is not null)
        {
            _timer.Tick -= OnTimerTick;
            _timer.Tick += OnTimerTick;
        }

        // Obtener la habilidad seleccionada
        var habilidadImg = _personajeImg
            .Habilidades
            .FirstOrDefault(h => h.Nombre == _nombreHabilidadSeleccionada);

        if (habilidadImg?.Sprites == null)
        {
            // No hay sprites; no hacer nada
            _categoriasAnimables = null;
            return;
        }

        // En orden fijo: Estatico, Adelante, Ataque, Atras
        var todasCategorias = new List<CategoriaSprite>()
        {
            habilidadImg.Sprites.Estatico,
            habilidadImg.Sprites.Adelante,
            habilidadImg.Sprites.Ataque,
            habilidadImg.Sprites.Atras
        };

        // Filtrar solo aquellas categorías con al menos un frame
        _categoriasAnimables = [];
        _intervalosPorFrame = [];
        _desplazamientosPorFrame = [];

        foreach (var catSprite in todasCategorias)
        {
            if (catSprite.Frames?.Count > 0)
            {
                _categoriasAnimables.Add(catSprite);

                // Parsear el tiempo total para esa categoría
                if (!double.TryParse(catSprite.Tiempo, out var tiempoTotal))
                    tiempoTotal = 0.1; // valor por defecto si falla el parseo

                // Calcular intervalo por frame = tiempoTotal / número de frames
                var cuentaFrames = catSprite.Frames.Count;
                var intervalo = tiempoTotal / cuentaFrames;
                _intervalosPorFrame.Add(intervalo);
                if (catSprite == habilidadImg.Sprites.Adelante)
                {
                    _desplazamientosPorFrame.Add(MAX_EJE_X / cuentaFrames);
                }
                else if (catSprite == habilidadImg.Sprites.Atras)
                {
                    _desplazamientosPorFrame.Add(-(MAX_EJE_X / cuentaFrames));
                }
                else
                {
                    _desplazamientosPorFrame.Add(0);
                }
            }
        }

        if (_categoriasAnimables.Count > 0 && _timer is not null)
            // Ajustar el intervalo inicial al intervalo por frame de la primera categoría
            _timer.Interval = TimeSpan.FromSeconds(_intervalosPorFrame![0]);
    }

    /// <summary>
    /// Evento que se ejecuta en cada tick del temporizador para avanzar la animación.
    /// Actualiza el frame mostrado y el desplazamiento visual en la vista.
    /// </summary>
    /// <param name="sender">Origen del evento.</param>
    /// <param name="e">Argumentos del evento.</param>
    private void OnTimerTick(object sender, EventArgs e)
    {
        if (_categoriasAnimables == null || _categoriasAnimables.Count == 0)
            return;

        // Avanzar al siguiente frame en la categoría actual
        _frameIndice++;
        var categoriaActual = _categoriasAnimables[_categoriaIndice];
        var frames = categoriaActual.Frames!;

        if (_frameIndice >= frames.Count)
        {
            // Llegamos al final de esta categoría: pasar a la siguiente
            _frameIndice = 0;
            _categoriaIndice = (_categoriaIndice + 1) % _categoriasAnimables.Count;
            categoriaActual = _categoriasAnimables[_categoriaIndice];
            frames = categoriaActual.Frames!;

            // Actualizar intervalo del timer al intervalo por frame de la nueva categoría
            if (_timer is not null)
            {
                var nuevoIntervalo = _intervalosPorFrame![_categoriaIndice];
                _timer.Interval = TimeSpan.FromSeconds(nuevoIntervalo);
            }
        }

        // Mostrar el frame actual en la vista
        _informacionVm.ImgPlay = frames[_frameIndice].Path;
        _informacionVm.EjeX += _desplazamientosPorFrame[_categoriaIndice];
    }

    /// <summary>
    /// Llama al evento para notificar un cambio de habilidad seleccionada.
    /// </summary>
    /// <param name="nuevaSeleccion">Nombre de la nueva habilidad seleccionada.</param>
    public void OnSeleccionadaCambio(string nuevaSeleccion)
    {
        EventoSeleccionadaCambio?.Invoke(this, nuevaSeleccion);
    }

    /// <summary>
    /// Cambia la propiedad de la habilidad seleccionada si la animación no está en curso.
    /// </summary>
    /// <param name="sender">Origen del evento.</param>
    /// <param name="nuevaSeleccion">Nombre de la nueva habilidad seleccionada.</param>
    public void CambiarPropiedad(object? sender, string nuevaSeleccion)
    {
        // Solo cambiar si no está corriendo la animación
        if (_timer is not null && !_timer.IsRunning)
            _nombreHabilidadSeleccionada = nuevaSeleccion;
    }
}
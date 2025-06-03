using System;
using System.Collections.Generic;
using System.Linq;
using JuegoMarvel.ModuloTienda.Model;

namespace JuegoMarvel.ModuloTienda.ViewModel.Comandos;

public class ComandoEmpezarAnimación : BaseCommand
{
    private const double MAX_EJE_X = 170;

    public event EventHandler<string> EventoSeleccionadaCambio;

    private readonly PersonajeImg _personajeImg;
    private string _nombreHabilidadSeleccionada;
    private IDispatcherTimer? _timer;
    private readonly InformacionPoupViewModel _informacionVm;

    // Lista de categorías (Estatico, Adelante, Ataque, Atras) que tienen al menos un frame
    private List<CategoriaSprite>? _categoriasAnimables;
    private List<double>? _intervalosPorFrame;

    private List<double> _desplazamientosPorFrame;


    // Índices para llevar la cuenta de en qué categoría y en qué frame estamos
    private int _categoriaIndice;
    private int _frameIndice;

    public ComandoEmpezarAnimación(~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~~
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

    public override void Execute(object? parameter)
    {
        InicializarAnimarPersonaje();
        if (_timer is not null && !_timer.IsRunning)
            _timer.Start();
    }

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
        _categoriasAnimables = new List<CategoriaSprite>();
        _intervalosPorFrame = new List<double>();
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
                    _desplazamientosPorFrame.Add( - (MAX_EJE_X / cuentaFrames));
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

    public void OnSeleccionadaCambio(string nuevaSeleccion)
    {
        EventoSeleccionadaCambio?.Invoke(this, nuevaSeleccion);
    }

    public void CambiarPropiedad(object? sender, string nuevaSeleccion)
    {
        // Solo cambiar si no está corriendo la animación
        if (_timer is not null && !_timer.IsRunning)
            _nombreHabilidadSeleccionada = nuevaSeleccion;
    }
}

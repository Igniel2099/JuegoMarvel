using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloTienda.Model;
using JuegoMarvel.ModuloTienda.ViewModel.Comandos;
using JuegoMarvel.Services;
using JuegoMarvelData.Data;
using JuegoMarvelData.Models;
using System.Collections.ObjectModel;

namespace JuegoMarvel.ModuloTienda.ViewModel;

/// <summary>
/// ViewModel para la ventana de la Tienda que contiene todos los personajes disponibles para comprar.
/// Expone las propiedades visuales y de animación necesarias para mostrar a los personajes que se pueden comprar y
/// el dinero y puntos que tiene el usuario.
/// </summary>
public class TiendaViewModel(BbddjuegoMarvelContext context) : BaseViewModel
{
    #region CamposViewModel

    /// <summary>
    /// Propiedad privada de los superpuntos del usuario actual como texto.
    /// </summary>
    private string _puntos = context.Usuarios.FirstOrDefault().SuperPuntos.ToString();

    /// <summary>
    /// Puntos del usuario actual en formato string. Notifica a la vista cuando cambia.
    /// </summary>
    public string Puntos
    {
        get => _puntos;
        set
        {
            if (_puntos == value) return;
            _puntos = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad privada de las monedas del usuario actual como texto.
    /// </summary>
    private string _monedas = context.Usuarios.FirstOrDefault().Monedas.ToString();

    /// <summary>
    /// Monedas del usuario actual en formato string. Notifica a la vista cuando cambia.
    /// </summary>
    public string Monedas
    {
        get => _monedas;
        set
        {
            if (_monedas == value) return;
            _monedas = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region Auxiliares

    /// <summary>
    /// Comando que permite volver atrás en la navegación.
    /// </summary>
    public ComandoNavegarVolverAtras NavAtras { get; set; } = new();

    /// <summary>
    /// Colección observable de tarjetas de personajes disponibles para comprar.
    /// </summary>
    public ObservableCollection<CardViewModel> PersonajesCards { get; set; } = [];

    /// <summary>
    /// Contexto de base de datos para acceder a datos de usuario y personajes.
    /// </summary>
    private readonly BbddjuegoMarvelContext _context = context;

    /// <summary>
    /// Servicio de gestión de personajes, encargado de consultar y cargar personajes e imágenes.
    /// </summary>
    public GestionPersonajes GestionPersonajes { get; } = new(context);
    #endregion

    /// <summary>
    /// Carga los datos necesarios para la vista, incluyendo los personajes disponibles y sus imágenes.
    /// </summary>
    public async Task CargarDatosDelView()
    {
        var (personajes, personajeImagenes) = await ObtenerRecursosNecesarios();
        CargarTarjetasPersonajes(personajes, personajeImagenes);
    }

    /// <summary>
    /// Obtiene desde la base de datos y desde el JSON los recursos necesarios para la tienda.
    /// </summary>
    /// <returns>Una tupla con la lista de personajes disponibles y sus imágenes asociadas.</returns>
    private async Task<(List<Personaje>, PersonajesImagenes)> ObtenerRecursosNecesarios()
    {
        List<Personaje> personajes = await GestionPersonajes.ObtenerPersonajesSinPersonajesUsuarioAsync();
        var personajesImgenes = await GestionPersonajes.CargarPersonajesJsonAsync();
        return (personajes, personajesImgenes);
    }

    /// <summary>
    /// Carga las tarjetas visuales para cada personaje disponible en la tienda.
    /// </summary>
    /// <param name="personajes">Lista de personajes a mostrar.</param>
    /// <param name="personajesImagenes">Diccionario de imágenes por nombre de personaje.</param>
    private void CargarTarjetasPersonajes(List<Personaje> personajes, PersonajesImagenes personajesImagenes)
    {
        foreach (var personaje in personajes)
        {
            string nombre = personaje.NombreCompleto;

            if (personajesImagenes.TryGetValue(nombre, out var personajeImg))
            {
                CardViewModel card = new(
                    personaje.IdPersonaje,
                    nombre,
                    personaje.Tipo,
                    personaje.Grupo,
                    personaje.Coste.Value,
                    personajeImg
                );

                // Comando para mostrar información adicional del personaje
                card.MostrarInformacionCommand = new MostrarInformacionCommand(card, _context, personajeImg);

                // Suscripción al evento de compra
                card.AvisarIdCompradol += OnAvisarIdComprado;

                // Comando para mostrar el popup de compra
                card.NavCompraPopup = new ComandoNavCompraPopup(card, _context, personajesImagenes[nombre].ImgCuerpo);

                PersonajesCards.Add(card);
            }
            else
            {
                throw new Exception("Error al buscar al personajeImg dentro de personajeImagenes");
            }
        }
    }

    /// <summary>
    /// Manejador del evento que se dispara cuando se compra un personaje.
    /// Elimina la tarjeta del personaje comprado y actualiza las monedas del usuario.
    /// </summary>
    /// <param name="sender">Tarjeta que envía el evento.</param>
    /// <param name="e">ID del personaje comprado.</param>
    public void OnAvisarIdComprado(object? sender, int e)
    {
        if (sender is CardViewModel card)
            PersonajesCards.Remove(card);

        Monedas = context.Usuarios.FirstOrDefault().Monedas.ToString();
    }
}

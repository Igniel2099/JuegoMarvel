
using JuegoMarvel.ModuloTienda.Model;
using JuegoMarvel.ModuloTienda.ViewModel.Comandos;
using JuegoMarvel.Services;
using JuegoMarvelData.Data;
using JuegoMarvelData.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace JuegoMarvel.ModuloTienda.ViewModel;

public class TiendaViewModel(BbddjuegoMarvelContext context) : BaseViewModel
{
    private string _puntos = context.Usuarios.FirstOrDefault().SuperPuntos.ToString();
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
    private string _monedas = context.Usuarios.FirstOrDefault().Monedas.ToString();
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


    public ObservableCollection<CardViewModel> PersonajesCards { get; set; } = [];

    private readonly BbddjuegoMarvelContext _context = context;
    public GestionPersonajes GestionPersonajes { get; } = new(context);

    public async Task CargarDatosDelView()
    {
        var (personajes, personajeImagenes) = await ObtenerRecursosNecesarios();
        CargarTarjetasPersonajes(personajes, personajeImagenes);
    }

    private async Task<(List<Personaje>, PersonajesImagenes)> ObtenerRecursosNecesarios()
    {
        List<Personaje> personajes = await GestionPersonajes.ObtenerPersonajesSinPersonajesUsuarioAsync();

        var personajesImgenes = await GestionPersonajes.CargarPersonajesJsonAsync();

        return (personajes, personajesImgenes);
    }

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
                card.MostrarInformacionCommand = new MostrarInformacionCommand(
                    card,
                    _context,
                    personajeImg
                );

                card.AvisarIdCompradol += OnAvisarIdComprado;

                card.NavCompraPopup = new ComandoNavCompraPopup(card, _context, personajesImagenes[nombre].ImgCuerpo);

                PersonajesCards.Add(card);
            }
            else
                throw new Exception("Error al buscar al personajeImg dentro de personajeImagenes");

        }
    }

    public void OnAvisarIdComprado(object? sender, int e)
    {
        if (sender is CardViewModel card)
            PersonajesCards.Remove(card);
        Monedas = context.Usuarios.FirstOrDefault().Monedas.ToString();
    }
}
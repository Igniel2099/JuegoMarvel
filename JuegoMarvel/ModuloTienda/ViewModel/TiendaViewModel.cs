using JuegoMarvel.ModuloTienda.Model;
using JuegoMarvel.ModuloTienda.ViewModel.Comandos;
using JuegoMarvelData.Data;
using JuegoMarvelData.Models;
using System.Collections.ObjectModel;

namespace JuegoMarvel.ModuloTienda.ViewModel;

public class TiendaViewModel : BaseViewModel
{
    private readonly BbddjuegoMarvelContext _context;
    public GestionPersonajes GestionPersonajes { get; }

    public ObservableCollection<CardViewModel> PersonajesCards { get; set; } = [];

    public TiendaViewModel(BbddjuegoMarvelContext context)
    {
        _context = context;
        GestionPersonajes = new(context);
        _ = CargarDatos();
    }

    private async Task CargarDatos()
     {
        List<Personaje> personajes = GestionPersonajes.ObtenerPersonajes();

        var personajesImgenes = await GestionPersonajes.CargarPersonajesJsonAsync();

        if (personajesImgenes.Count != personajes.Count)
            throw new Exception("No tienes la misma cantidad de personajes que de personajes con imagenes");

        foreach (var personaje in personajes)
        {
            string nombre = personaje.NombreCompleto;

            if (personajesImgenes.TryGetValue(nombre, out var personajeImg))
            {
                CardViewModel card = new(
                    personaje.IdPersonaje,
                    nombre,
                    personaje.Tipo,
                    personaje.Grupo,
                    personaje.Coste.ToString(),
                    personajeImg
                );
                card.MostrarInformacionCommand = new MostrarInformacionCommand(
                    card,
                    _context,
                    personajeImg
                );

                PersonajesCards.Add(card);
            }
            else
                throw new Exception("Error al buscar al personajeImg dentro de personajeImagenes");



        }
    }
}
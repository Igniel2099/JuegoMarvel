using JuegoMarvel.ModuloJuego.Model;
using JuegoMarvel.ModuloJuego.ViewModel;
using JuegoMarvel.ModuloTienda.Model;
using JuegoMarvel.Services;
using JuegoMarvelData.Data;

namespace JuegoMarvel.ModuloJuego.View;

public partial class Juego : ContentPage
{
    private PersonajesImagenes _pi;

    public Juego(ClienteJuego cliente, string nombrePersonaje, string nombrePersonajeContrario, BbddjuegoMarvelContext? context = null)
    {
        InitializeComponent();
        
        CargarYBindear(cliente, nombrePersonaje, nombrePersonajeContrario);
    }

    private async void CargarYBindear(ClienteJuego cliente, string nombrePersonaje, string nombrePersonajeContrario)
    {
        _pi = await GestionPersonajes.CargarPersonajesJsonAsync();

        PersonajeImg personajePropio = _pi[nombrePersonaje];
        PersonajeImg personajeContrario = _pi[nombrePersonajeContrario];

        JuegoViewModel vm = new(cliente, nombrePersonaje, nombrePersonajeContrario, personajePropio, personajeContrario);
        BindingContext = vm;
    }
}
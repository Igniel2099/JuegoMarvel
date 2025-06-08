using JuegoMarvel.ModuloJuego.ViewModel;
using JuegoMarvel.ModuloTienda.Model;
using JuegoMarvel.Services;

namespace JuegoMarvel.ModuloJuego.View;

public partial class Juego : ContentPage
{
    private PersonajesImagenes _pi;

    public Juego()
    {
        InitializeComponent();
        CargarYBindear();
    }

    private async void CargarYBindear()
    {
        // 1. Carga asíncrona de los datos
        _pi = await GestionPersonajes.CargarPersonajesJsonAsync();

        // 2. Obtén los PersonajeImg que necesites
        PersonajeImg personajePropio = _pi["DareDevil"];
        PersonajeImg personajeContrario = _pi["Deadpool"];

        // 3. Asigna el BindingContext una vez que ya están cargados
        BindingContext = new JuegoViewModel(personajePropio, personajeContrario);
    }
}
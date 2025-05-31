using CommunityToolkit.Maui.Views;
using JuegoMarvel.ModuloTienda.Model;
using JuegoMarvel.Views;
using JuegoMarvelData.Data;
using JuegoMarvelData.Models;

namespace JuegoMarvel.ModuloTienda.ViewModel.Comandos;

public class MostrarInformacionCommand(
    CardViewModel card, 
    BbddjuegoMarvelContext context,
    PersonajeImg personajeImg) : BaseCommand
{
    private readonly CardViewModel _vm = card;
    private readonly BbddjuegoMarvelContext _context = context;
    private readonly PersonajeImg _personajeImg = personajeImg;
    public async override void Execute(object? parameter)
    {
        Personaje personjeBuscado = 
            _context.Personajes.FirstOrDefault(p => p.IdPersonaje == _vm.IdPersonaje)
            ?? throw new Exception("No se encontro al personaje");

        List<Habilidade> habilidades = [.. _context.Habilidade.Where(h => h.IdPersonaje == personjeBuscado.IdPersonaje)];
        
        if ( parameter is Tienda tienda)
        {
            var popup = new InformacionPopup(
                new InformacionPoupViewModel(
                        personjeBuscado.NombreCompleto,
                        habilidades,
                        _personajeImg
                    )
            );
            await tienda.ShowPopupAsync(popup);
        }

    }
}

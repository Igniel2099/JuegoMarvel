using JuegoMarvel.ModuloTienda.View;
using JuegoMarvel.Services;
using JuegoMarvelData.Data;
using JuegoMarvelData.Models;

namespace JuegoMarvel.ModuloTienda.ViewModel.Comandos;

public class ComandoComprar(CompraViewModel vm, BbddjuegoMarvelContext context) : BaseCommand
{
    private readonly CompraViewModel _compraViewModel = vm;
    private readonly BbddjuegoMarvelContext _context = context;

    public async override void Execute(object? parameter)
    {
        if (parameter is CompraPopup compraPopup)
        {
            var gridTocado = compraPopup.FindByName<Grid>("GridCompra");
            await AnimacionesComunes.AnimacionGrid(gridTocado);

            Usuario usuarioActual = _context.Usuarios.FirstOrDefault()
                ?? throw new Exception("No se ha encontrado el unico usuario que deberia tener.");
            Personaje personajeActual = _context.Personajes.FirstOrDefault( p => p.IdPersonaje == _compraViewModel.IdPersonaje)
                ?? throw new Exception("No se ha encontrado al personaje ha ocurrido un error.");

            if( _compraViewModel.Costo <= usuarioActual.Monedas)
            {
                usuarioActual.Monedas -= _compraViewModel.Costo;
                _context.PersonajeUsuarios.Add(
                    new PersonajeUsuario
                    {
                        IdPersonaje = personajeActual.IdPersonaje,
                        Nivel = 1,
                        ValorHabilidad1 = 0,
                        ValorHabilidad2 = 0,
                        ValorHabilidad3 = 0,
                        IdPersonajeNavigation = personajeActual
                    }
                );

                _context.SaveChanges();

                _compraViewModel.OnAvisarIdComprado();

                compraPopup.ClickedBotonCerrar(
                    compraPopup.FindByName<ImageButton>("BotonCerrar"),
                    new EventArgs()
                );
            }
            else
            {
                await AnimacionesComunes.VibrarGrid(gridTocado);
            }
        }
    }
}

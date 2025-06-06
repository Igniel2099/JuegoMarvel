using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloTienda.View;
using JuegoMarvel.Services;
using JuegoMarvelData.Data;
using JuegoMarvelData.Models;

namespace JuegoMarvel.ModuloTienda.ViewModel.Comandos;

/// <summary>
/// Comando para gestionar la compra de un personaje en la tienda.
/// Valida si el usuario tiene suficientes monedas, realiza la compra y actualiza la base de datos.
/// Si la compra es exitosa, descuenta el costo, añade el personaje al usuario y cierra el popup.
/// Si no hay suficientes monedas, muestra una animación de vibración en el grid de compra.
/// </summary>
/// <remarks>
/// Utiliza <see cref="CompraViewModel"/> para obtener los datos del personaje a comprar y <see cref="BbddjuegoMarvelContext"/>
/// para acceder y modificar la base de datos local. El comando se ejecuta desde el popup de compra (<see cref="CompraPopup"/>).
/// </remarks>
public class ComandoComprar(CompraViewModel vm, BbddjuegoMarvelContext context) : BaseCommand
{
    /// <summary>
    /// Propiedad privada del View Model de la Compra
    /// </summary>
    private readonly CompraViewModel _compraViewModel = vm;

    /// <summary>
    /// Propiedad Privda del DbConttext de la base de datos local
    /// </summary>
    private readonly BbddjuegoMarvelContext _context = context;

    /// <summary>
    /// Ejecuta la lógica de compra de personaje.
    /// Si el usuario tiene suficientes monedas, realiza la compra y cierra el popup.
    /// Si no, muestra una animación de vibración en el grid de compra.
    /// </summary>
    /// <param name="parameter">Debe ser una instancia de <see cref="CompraPopup"/> (la vista actual).</param>
    public async override void Execute(object? parameter)
    {
        if (parameter is CompraPopup compraPopup)
        {
            var gridTocado = compraPopup.FindByName<Grid>("GridCompra");
            await AnimacionesComunes.AnimacionGrid(gridTocado);

            Usuario usuarioActual = _context.Usuarios.FirstOrDefault()
                ?? throw new Exception("No se ha encontrado el unico usuario que deberia tener.");
            Personaje personajeActual = _context.Personajes.FirstOrDefault(p => p.IdPersonaje == _compraViewModel.IdPersonaje)
                ?? throw new Exception("No se ha encontrado al personaje ha ocurrido un error.");

            if (_compraViewModel.Costo <= usuarioActual.Monedas)
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

                await _context.SaveChangesAsync();

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
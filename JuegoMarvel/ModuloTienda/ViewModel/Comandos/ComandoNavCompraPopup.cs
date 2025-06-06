using CommunityToolkit.Maui.Views;
using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloTienda.View;
using JuegoMarvel.Services;
using JuegoMarvel.Views;
using JuegoMarvelData.Data;

namespace JuegoMarvel.ModuloTienda.ViewModel.Comandos;

/// <summary>
/// Comando para mostrar el popup de compra de un personaje desde la tienda.
/// Crea y muestra el <see cref="CompraPopup"/> con el ViewModel correspondiente, enlazando los eventos de compra.
/// </summary>
/// <remarks>
/// Utiliza <see cref="CardViewModel"/> para obtener los datos del personaje seleccionado, <see cref="BbddjuegoMarvelContext"/>
/// para el acceso a la base de datos y la ruta de la imagen del cuerpo del personaje.
/// </remarks>
public class ComandoNavCompraPopup(CardViewModel vm, BbddjuegoMarvelContext context, string imgCuerpo) : BaseCommand
{
    /// <summary>
    /// Propiedad privada de la CardViewModel
    /// </summary>
    private readonly CardViewModel _vm = vm;

    /// <summary>
    /// Propiedad privada del DbContext de la base de datos local
    /// </summary>
    private readonly BbddjuegoMarvelContext _context = context;

    /// <summary>
    /// Propiedad privada de la imagen del cuerpo del personaje.
    /// </summary>
    private readonly string _pathImageCuerpo = imgCuerpo;

    /// <summary>
    /// Ejecuta el comando mostrando el popup de compra y enlazando el evento de compra realizada.
    /// </summary>
    /// <param name="parameter">Debe ser un <see cref="Grid"/> de la tienda que dispara la acción.</param>
    public async override void Execute(object? parameter)
    {
        if (parameter is Grid gridMoneda)
        {
            await AnimacionesComunes.AnimacionGrid(gridMoneda);

            CompraViewModel compraVm = new(
                    _context,
                    _vm.IdPersonaje,
                    _vm.Nombre,
                    _pathImageCuerpo,
                    _vm.Costo
            );

            compraVm.AvisarIdCompradol += CompraVm_AvisarIdCompradol;
            var popup = new CompraPopup(compraVm);

            var tiendaPage = gridMoneda.GetParentPage();
            if (tiendaPage is Tienda tienda)
                await tienda.ShowPopupAsync(popup);

            return;
        }
    }

    /// <summary>
    /// Evento que se ejecuta cuando se realiza la compra, notificando al <see cref="CardViewModel"/> correspondiente.
    /// </summary>
    /// <param name="sender">Origen del evento.</param>
    /// <param name="e">Identificador del personaje comprado.</param>
    private void CompraVm_AvisarIdCompradol(object? sender, int e)
    {
        _vm.OnAvisarIdComprado(sender, e);
    }
}
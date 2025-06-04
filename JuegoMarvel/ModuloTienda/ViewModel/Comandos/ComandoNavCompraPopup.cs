using CommunityToolkit.Maui.Views;
using JuegoMarvel.ModuloTienda.View;
using JuegoMarvel.Services;
using JuegoMarvel.Views;
using JuegoMarvelData.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoMarvel.ModuloTienda.ViewModel.Comandos;

public class ComandoNavCompraPopup(CardViewModel vm, BbddjuegoMarvelContext context, string imgCuerpo) : BaseCommand
{
    private readonly CardViewModel _vm = vm;
    private readonly BbddjuegoMarvelContext _context = context;
    private readonly string _pathImageCuerpo = imgCuerpo;
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

    private void CompraVm_AvisarIdCompradol(object? sender, int e)
    {
        _vm.OnAvisarIdComprado(sender, e);
    }
}

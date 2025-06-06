using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloAuxiliares.ModuloConfiguracion.ViewModel.Comandos;
using JuegoMarvel.Services;
using JuegoMarvelData.Data;

namespace JuegoMarvel.ModuloAuxiliares.ModuloConfiguracion.ViewModel;

public class ConfiguracionViewModel(BbddjuegoMarvelContext context) : BaseViewModel
{
    public ComandoCerrarSesion ComandoCerrarSesion { get; set; } = new(context);
    public ComandoNavegarVolverAtras NavAtras { get; set; } = new();
}

using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloAuxiliares.ModuloConfiguracion.ViewModel.Comandos;
using JuegoMarvel.Services;

namespace JuegoMarvel.ModuloAuxiliares.ModuloConfiguracion.ViewModel;

public class ConfiguracionViewModel : BaseViewModel
{
    public ComandoCerrarSesion ComandoCerrarSesion {  get; set; }
    public ComandoNavegarVolverAtras NavAtras { get; set; } = new();
}

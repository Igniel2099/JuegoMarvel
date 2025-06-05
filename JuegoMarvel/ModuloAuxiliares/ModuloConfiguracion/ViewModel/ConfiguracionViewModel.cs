using JuegoMarvel.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JuegoMarvel.ModuloAuxiliares.ModuloConfiguracion.ViewModel;

public class ConfiguracionViewModel : BaseViewModel
{
    public ICommand ComandoCerrarSesion {  get; set; }
    public ComandoNavegarVolverAtras NavAtras { get; set; } = new();
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoMarvel.ModuloEquipo.ViewModel.Comandos;

public class ComandoSeleccionarPersonaje(
    EquipoViewModel vm,
    PersonajeUsuarioViewModel perUsViewModel) : BaseCommand
{
    private readonly EquipoViewModel _viewModel = vm;

    private readonly PersonajeUsuarioViewModel _perUsViewModel = perUsViewModel;
    public override void Execute(object? parameter)
    {
        ComprobacionGenerarAnimacion(parameter);
    }

    private void ComprobacionGenerarAnimacion(object? parameter)
    {
        if (parameter is Grid gridTocado)
            AnimacionSeleccionGrid(gridTocado);
        else
            throw new Exception("Se esta registrando un Control no valido");

    }

    private void AnimacionSeleccionGrid(Grid gridTocado)
    {
        if (_viewModel.PersonajeUsuarioSeleccionado != null)
            _viewModel.PersonajeUsuarioSeleccionado.ImagenContenedor = "fondo_contenedor_equipo.png";

        _perUsViewModel.ImagenContenedor = "fondo_seleccion_equipo.png";

        _viewModel.PersonajeUsuarioSeleccionado = _perUsViewModel;
    }

    
}

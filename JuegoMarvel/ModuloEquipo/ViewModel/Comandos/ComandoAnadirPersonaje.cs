using JuegoMarvelData.Models;
using System;
using System.Linq;

namespace JuegoMarvel.ModuloEquipo.ViewModel.Comandos;

public class ComandoAnadirPersonaje : BaseCommand
{
    private readonly EquipoViewModel _vm;

    public ComandoAnadirPersonaje(EquipoViewModel vm)
    {
        _vm = vm ?? throw new ArgumentNullException(nameof(vm));
    }

    public override void Execute(object? parameter)
    {
        // 1) Validar que el usuario haya seleccionado un PersonajeUsuario
        if (_vm.PersonajeUsuarioSeleccionado == null)
            return;

        // 2) Recuperar (o crear) la entidad Equipo desde el contexto
        var equipo = _vm.Context.Equipos
            .FirstOrDefault()
            ?? throw new Exception("No existe ningún Equipo en la base de datos.");

        // 3) Verificar si ya se ha agregado ese PersonajeUsuario antes
        //    (para que no se duplique en la ranura)
        var idSeleccionado = _vm.PersonajeUsuarioSeleccionado.IdPersonajeUsuario;
        if (equipo.IdPersonajeUsuario1 == idSeleccionado ||
            equipo.IdPersonajeUsuario2 == idSeleccionado ||
            equipo.IdPersonajeUsuario3 == idSeleccionado)
        {
            // Ya está agregado, salimos
            return;
        }

        // 4) Añadirlo en la primera ranura libre (1, luego 2, luego 3)
        if (_vm.PersonajeUnoEquipo == string.Empty)
        {
            _vm.PersonajeUnoEquipo = _vm.PersonajeUsuarioSeleccionado.ImgCuerpo;
            equipo.IdPersonajeUsuario1 = idSeleccionado;
        }
        else if (_vm.PersonajeDosEquipo == string.Empty)
        {
            _vm.PersonajeDosEquipo = _vm.PersonajeUsuarioSeleccionado.ImgCuerpo;
            equipo.IdPersonajeUsuario2 = idSeleccionado;
        }
        else if (_vm.PersonajeTresEquipo == string.Empty)
        {
            _vm.PersonajeTresEquipo = _vm.PersonajeUsuarioSeleccionado.ImgCuerpo;
            equipo.IdPersonajeUsuario3 = idSeleccionado;
        }
        else
        {
            // Las tres ranuras están ocupadas; podrías mostrar un mensaje o simplemente salir
            return;
        }

        // 5) Guardar inmediatamente los cambios en base de datos
        _vm.Context.SaveChanges();
    }
}

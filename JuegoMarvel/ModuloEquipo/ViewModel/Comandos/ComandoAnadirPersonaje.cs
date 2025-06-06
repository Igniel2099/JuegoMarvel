using JuegoMarvel.ClasesBase;
using JuegoMarvelData.Models;
using System;
using System.Linq;

namespace JuegoMarvel.ModuloEquipo.ViewModel.Comandos;

/// <summary>
/// Comando para añadir un personaje seleccionado al equipo del usuario.
/// </summary>
public class ComandoAnadirPersonaje : BaseCommand
{
    private readonly EquipoViewModel _vm;

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="ComandoAnadirPersonaje"/>.
    /// </summary>
    /// <param name="vm">ViewModel del equipo al que se añadirá el personaje.</param>
    /// <exception cref="ArgumentNullException">Se lanza si el ViewModel es nulo.</exception>
    public ComandoAnadirPersonaje(EquipoViewModel vm)
    {
        _vm = vm ?? throw new ArgumentNullException(nameof(vm));
    }

    /// <summary>
    /// Ejecuta la lógica para añadir el personaje seleccionado al equipo.
    /// <param name="parameter">Parámetro opcional (no utilizado).</param>
    public override void Execute(object? parameter)
    {
        if (_vm.PersonajeUsuarioSeleccionado == null)
            return;

        var equipo = _vm.Context.Equipos
            .FirstOrDefault()
            ?? throw new Exception("No existe ningún Equipo en la base de datos.");

        var idSeleccionado = _vm.PersonajeUsuarioSeleccionado.IdPersonajeUsuario;
        if (equipo.IdPersonajeUsuario1 == idSeleccionado ||
            equipo.IdPersonajeUsuario2 == idSeleccionado ||
            equipo.IdPersonajeUsuario3 == idSeleccionado)
        {
            // El personaje que quieres agregar ya esta agredado
            return;
        }

        if (_vm.PersonajeUnoEquipo?.Length == 0)
        {
            _vm.PersonajeUnoEquipo = _vm.PersonajeUsuarioSeleccionado.ImgCuerpo;
            equipo.IdPersonajeUsuario1 = idSeleccionado;
        }
        else if (_vm.PersonajeDosEquipo?.Length == 0)
        {
            _vm.PersonajeDosEquipo = _vm.PersonajeUsuarioSeleccionado.ImgCuerpo;
            equipo.IdPersonajeUsuario2 = idSeleccionado;
        }
        else if (_vm.PersonajeTresEquipo?.Length == 0)
        {
            _vm.PersonajeTresEquipo = _vm.PersonajeUsuarioSeleccionado.ImgCuerpo;
            equipo.IdPersonajeUsuario3 = idSeleccionado;
        }
        else
        {
            // Todas estan ocupadas
            return;
        }

        _vm.Context.SaveChanges();
    }
}
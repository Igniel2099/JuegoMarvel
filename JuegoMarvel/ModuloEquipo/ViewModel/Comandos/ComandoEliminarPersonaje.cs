using JuegoMarvel.ClasesBase;
using JuegoMarvelData.Models;

namespace JuegoMarvel.ModuloEquipo.ViewModel.Comandos;

/// <summary>
/// Comando para eliminar el último personaje añadido al equipo.
/// Elimina el personaje de la posición más alta ocupada (tercera, luego segunda, luego primera).
/// </summary>
public class ComandoEliminarPersonaje(EquipoViewModel vm) : BaseCommand
{
    private readonly EquipoViewModel _vm = vm;

    /// <summary>
    /// Ejecuta la eliminación del personaje del equipo en la posición más alta ocupada.
    /// </summary>
    /// <param name="parameter">Parámetro opcional (no utilizado).</param>
    public override void Execute(object? parameter)
    {
        Equipo equipo = _vm.Context.Equipos.FirstOrDefault()
            ?? throw new Exception("No existe equipos");

        if (_vm.PersonajeTresEquipo != string.Empty)
        {
            _vm.PersonajeTresEquipo = string.Empty;
            equipo.IdPersonajeUsuario3 = null;
        }
        else if (_vm.PersonajeDosEquipo != string.Empty)
        {
            _vm.PersonajeDosEquipo = string.Empty;
            equipo.IdPersonajeUsuario2 = null;
        }
        else
        {
            _vm.PersonajeUnoEquipo = string.Empty;
            equipo.IdPersonajeUsuario1 = null;
        }

        _vm.Context.SaveChanges();
    }
}
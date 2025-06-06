using CommunityToolkit.Maui.Views;
using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloTienda.Model;
using JuegoMarvel.Views;
using JuegoMarvelData.Data;
using JuegoMarvelData.Models;

namespace JuegoMarvel.ModuloTienda.ViewModel.Comandos;

/// <summary>
/// Comando para mostrar la información detallada de un personaje en la tienda.
/// Abre un popup con los datos completos del personaje y sus habilidades, utilizando el modelo gráfico correspondiente.
/// </summary>
/// <remarks>
/// Utiliza <see cref="CardViewModel"/> para obtener el identificador del personaje, <see cref="BbddjuegoMarvelContext"/>
/// para acceder a los datos y <see cref="PersonajeImg"/> para la información visual. El popup mostrado es <see cref="InformacionPopup"/>.
/// </remarks>
public class MostrarInformacionCommand(
    CardViewModel card,
    BbddjuegoMarvelContext context,
    PersonajeImg personajeImg) : BaseCommand
{
    /// <summary>
    /// ViewModel de la tarjeta del personaje seleccionado.
    /// </summary>
    private readonly CardViewModel _vm = card;

    /// <summary>
    /// Contexto de la base de datos para acceder a los datos del personaje y sus habilidades.
    /// </summary>
    private readonly BbddjuegoMarvelContext _context = context;

    /// <summary>
    /// Modelo gráfico con la información visual del personaje.
    /// </summary>
    private readonly PersonajeImg _personajeImg = personajeImg;

    /// <summary>
    /// Ejecuta el comando mostrando un popup con la información detallada del personaje y sus habilidades.
    /// </summary>
    /// <param name="parameter">Debe ser una instancia de <see cref="Tienda"/> (la vista actual de la tienda).</param>
    public async override void Execute(object? parameter)
    {
        Personaje personjeBuscado =
            _context.Personajes.FirstOrDefault(p => p.IdPersonaje == _vm.IdPersonaje)
            ?? throw new Exception("No se encontro al personaje");

        List<Habilidade> habilidades = [.. _context.Habilidade.Where(h => h.IdPersonaje == personjeBuscado.IdPersonaje)];

        if (parameter is Tienda tienda)
        {
            var popup = new InformacionPopup(
                new InformacionPoupViewModel(
                        personjeBuscado.NombreCompleto,
                        habilidades,
                        _personajeImg
                    )
            );
            await tienda.ShowPopupAsync(popup);
        }
    }
}
using JuegoMarvel.ModuloTienda.Model;
using JuegoMarvelData.Data;
using JuegoMarvelData.Models;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Text.Json;

namespace JuegoMarvel.Services;

/// <summary>
/// Servicio encargado de gestionar los personajes, habilidades y datos relacionados
/// que se usan en la tienda y otros módulos del juego.
/// Utiliza Entity Framework para acceder a la base de datos.
/// </summary>
public class GestionPersonajes(BbddjuegoMarvelContext context)
{
    /// <summary>
    /// Propiedad privada que representa el contexto de base de datos.
    /// </summary>
    private readonly BbddjuegoMarvelContext _context = context;

    /// <summary>
    /// Obtiene un diccionario con los IDs y nombres de los personajes asociados al usuario.
    /// </summary>
    /// <param name="listPersonajeUsuarios">Lista de personajes del usuario.</param>
    /// <returns>Diccionario con ID de personaje como clave y su nombre completo como valor.</returns>
    public Dictionary<int, string> ObtenerNombresPersonajesUsuario(List<PersonajeUsuario> listPersonajeUsuarios)
    {
        var personajeIds = listPersonajeUsuarios
            .Select(pu => pu.IdPersonaje)
            .Distinct()
            .ToList(); // Propiedad privada: lista de IDs únicos

        var dict = _context.Personajes
            .Where(p => personajeIds.Contains(p.IdPersonaje))
            .ToDictionary(
                p => p.IdPersonaje,
                p => p.NombreCompleto
            );

        return dict;
    }

    /// <summary>
    /// Devuelve todos los personajes disponibles en la base de datos.
    /// </summary>
    /// <returns>Lista de objetos <see cref="Personaje"/>.</returns>
    public List<Personaje> ObtenerPersonajes()
    {
        return [.. _context.Personajes];
    }

    /// <summary>
    /// Obtiene la lista de personajes que aún no están vinculados a ningún usuario.
    /// </summary>
    /// <returns>Lista de personajes no adquiridos por el usuario.</returns>
    public async Task<List<Personaje>> ObtenerPersonajesSinPersonajesUsuarioAsync() => await _context.Personajes
        .Where(p => !_context.PersonajeUsuarios
            .Any(pu => pu.IdPersonaje == p.IdPersonaje))
        .ToListAsync();

    /// <summary>
    /// Devuelve todos los personajes que tiene el usuario registrados en la base de datos.
    /// </summary>
    /// <returns>Lista de objetos <see cref="PersonajeUsuario"/>.</returns>
    public List<PersonajeUsuario> ObtenerPersonajesUsuario()
    {
        return [.. _context.PersonajeUsuarios];
    }

    /// <summary>
    /// Devuelve todas las habilidades de todos los personajes disponibles.
    /// </summary>
    /// <returns>Lista de objetos <see cref="Habilidade"/>.</returns>
    public List<Habilidade> ObtenerTodasLasHabilidades()
    {
        return [.. _context.Habilidade];
    }

    /// <summary>
    /// Obtiene las habilidades relacionadas con una lista de personajes del usuario.
    /// </summary>
    /// <param name="listPersonajesUsuario">Lista de personajes del usuario.</param>
    /// <returns>Lista de habilidades que pertenecen a esos personajes.</returns>
    public List<Habilidade> ObtenerHabilidadesPersonajesUsuarios(List<PersonajeUsuario> listPersonajesUsuario)
    {
        var personajeIds = listPersonajesUsuario
            .Select(pu => pu.IdPersonaje)
            .Distinct()
            .ToList(); // Propiedad privada: IDs únicos de personajes

        var habilidades = _context.Habilidade
            .Where(h => personajeIds.Contains(h.IdPersonaje))
            .ToList();

        return habilidades;
    }

    /// <summary>
    /// Carga el archivo JSON que contiene las rutas de imagen de cada personaje y lo convierte a un diccionario.
    /// </summary>
    /// <returns>Un objeto <see cref="PersonajesImagenes"/> con los datos deserializados.</returns>
    public static async Task<PersonajesImagenes> CargarPersonajesJsonAsync()
    {
        try
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("dict_imagenes_personajes.json"); // Propiedad privada: flujo del archivo JSON
            using var reader = new StreamReader(stream);
            string json = await reader.ReadToEndAsync();

            var opciones = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            var personajesData = JsonSerializer.Deserialize<PersonajesImagenes>(json, opciones);
            return personajesData ?? new PersonajesImagenes();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error cargando JSON: {ex.Message}");
            return new PersonajesImagenes();
        }
    }

    /// <summary>
    /// Devuelve una lista con los nombres de los personajes actualmente en el equipo del usuario.
    /// </summary>
    /// <returns>Lista de nombres de personajes en el equipo actual, o <c>null</c> si el equipo está vacío.</returns>
    public async Task<List<string>?> CargarNombresEquipoPersonajesUsuario()
    {
        var equipo = await _context.Equipos.FirstOrDefaultAsync(); // Propiedad privada: equipo único actual
        if (equipo == null) return null;

        var idsEnEquipo = new List<int>();
        if (equipo.IdPersonajeUsuario1.HasValue)
            idsEnEquipo.Add(equipo.IdPersonajeUsuario1.Value);
        if (equipo.IdPersonajeUsuario2.HasValue)
            idsEnEquipo.Add(equipo.IdPersonajeUsuario2.Value);
        if (equipo.IdPersonajeUsuario3.HasValue)
            idsEnEquipo.Add(equipo.IdPersonajeUsuario3.Value);

        if (idsEnEquipo.Count == 0) return null;

        var nombres = await _context.PersonajeUsuarios
           .Where(pu => idsEnEquipo.Contains(pu.IdPersonajeUsuario))
           .Select(pu => pu.IdPersonajeNavigation!.NombreCompleto)
           .ToListAsync();

        return nombres;
    }
}

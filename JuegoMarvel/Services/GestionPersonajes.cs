using JuegoMarvel.ModuloTienda.Model;
using JuegoMarvelData.Data;
using JuegoMarvelData.Models;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;
using System;
using System.Text.Json;

namespace JuegoMarvel.Services;

public class GestionPersonajes(BbddjuegoMarvelContext context)
{
    private readonly BbddjuegoMarvelContext _context = context;

    public Dictionary<int, string> ObtenerNombresPersonajesUsuario(List<PersonajeUsuario> listPersonajeUsuarios)
    {
        var personajeIds = listPersonajeUsuarios
            .Select(pu => pu.IdPersonaje)
            .Distinct()
            .ToList();

        var dict = _context.Personajes
            .Where(p => personajeIds.Contains(p.IdPersonaje))
            .ToDictionary(
                p => p.IdPersonaje,
                p => p.NombreCompleto
            );

        return dict;
    }


    public List<Personaje> ObtenerPersonajes()
    {
        return [.. _context.Personajes];
    }

    public async Task<List<Personaje>> ObtenerPersonajesSinPersonajesUsuarioAsync() => await _context.Personajes
            .Where(p => !_context.PersonajeUsuarios
            .Any(pu => pu.IdPersonaje == p.IdPersonaje))
            .ToListAsync();

    public List<PersonajeUsuario> ObtenerPersonajesUsuario()
    {
        return [.. _context.PersonajeUsuarios];
    }

    public List<Habilidade> ObtenerTodasLasHabilidades()
    {
        return [.. _context.Habilidade];
    }

    public List<Habilidade> ObtenerHabilidadesPersonajesUsuarios(List<PersonajeUsuario> listPersonajesUsuario)
    {
        var personajeIds = listPersonajesUsuario
        .Select(pu => pu.IdPersonaje)
        .Distinct()
        .ToList();

        var habilidades = _context.Habilidade
            .Where(h => personajeIds.Contains(h.IdPersonaje))
            .ToList();

        return habilidades;
    }

    public static async Task<PersonajesImagenes> CargarPersonajesJsonAsync()
    {
        try
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("dict_imagenes_personajes.json");
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

    public async Task<List<string>?> CargarNombresEquipoPersonajesUsuario()
    {
        // 1. Cargar el equipo de forma asíncrona
        var equipo = await _context.Equipos.FirstOrDefaultAsync();
        if (equipo == null) return null;

        // 2. Si existen varias ranuras nulas, filtramos solo los IDs no nulos
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

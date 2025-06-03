using JuegoMarvelData.Data;
using JuegoMarvelData.Models;
using SQLitePCL;
using System;
using System.Text.Json;

namespace JuegoMarvel.ModuloTienda.Model;

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
}

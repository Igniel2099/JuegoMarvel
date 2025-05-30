using JuegoMarvelData.Data;
using JuegoMarvelData.Models;
using SQLitePCL;
using System;
using System.Text.Json;

namespace JuegoMarvel.ModuloTienda.Model;

public class GestionPersonajes(BbddjuegoMarvelContext context)
{
    private readonly BbddjuegoMarvelContext _context = context;

    public List<Personaje> ObtenerPersonajes()
    {
        return [.. _context.Personajes];
    }

    public async Task<PersonajesImagenes> CargarPersonajesAsync()
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
            return new PersonajesImagenes(); // Devuelve un objeto vacío si hay error
        }
    }
}

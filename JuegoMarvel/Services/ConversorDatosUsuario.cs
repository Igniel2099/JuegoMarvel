using MensajesServidor;
using JuegoMarvelData.Models;

namespace JuegoMarvel.Services;

public static class ConversorDatosUsuario
{
    public static Usuario MapUsuario(MandarDatosUsuario dto)
    {
        return new Usuario
        {
            IdUsuario = dto.IdUsuario,
            NombreUsuario = dto.NombreUsuario,
            Correo = dto.Correo,
            Experiencia = dto.Experiencia,
            Monedas = dto.Monedas,
            SuperPuntos = dto.SuperPuntos,
        };
    }

    public static Personaje MapPersonaje(PersonajeDTO dto)
    {
        return new Personaje
        {
            IdPersonaje = dto.IdPersonaje,
            NombreCompleto = dto.NombreCompleto,
            Tipo = dto.Tipo,
            Grupo = dto.Grupo,
            Coste = dto.Coste
        };
    }

    public static Habilidade MapHabilidade(HabilidadeDTO dto)
    {
        return new Habilidade
        {
            IdHabilidades = dto.IdHabilidades,
            IdPersonaje = dto.IdPersonaje,
            Nombre = dto.Nombre,
            Valor = dto.Valor,
            Tipo = dto.Tipo
        };
    }

    public static PersonajeUsuario MapPersonajeUsuario(PersonajeUsuarioDTO dto)
    {
        return new PersonajeUsuario
        {
            IdPersonajeUsuario = dto.IdPersonajeUsuario,
            IdPersonaje = dto.IdPersonaje,
            Nivel = dto.Nivel,
            ValorHabilidad1 = dto.ValorHabilidad1,
            ValorHabilidad2 = dto.ValorHabilidad2,
            ValorHabilidad3 = dto.ValorHabilidad3
        };
    }

    public static Equipo MapEquipo(EquipoDTO dto)
    {
        return new Equipo
        {
            IdEquipo = dto.IdEquipo,
            IdPersonajeUsuario1 = dto.IdPersonajeUsuario1,
            IdPersonajeUsuario2 = dto.IdPersonajeUsuario2,
            IdPersonajeUsuario3 = dto.IdPersonajeUsuario3
        };
    }

    public static Pelea MapPelea(PeleaDTO dto)
    {
        return new Pelea
        {
            IdPeleas = dto.IdPeleas,
            ContrincanteUsuario = dto.ContrincanteUsuario,
            SoyGanador = dto.SoyGanador ? 1 : 0 // Convertir booleano a entero (1 o 0)
        };
    }
}


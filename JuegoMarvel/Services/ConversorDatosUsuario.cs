using MensajesServidor;
using JuegoMarvelData.Models;

namespace JuegoMarvel.Services;

/// <summary>
/// Clase estática encargada de convertir objetos de transferencia de datos (DTOs) recibidos del servidor
/// en modelos de entidad que se usan internamente en la aplicación y en la base de datos.
/// </summary>
public static class ConversorDatosUsuario
{
    /// <summary>
    /// Convierte un objeto <see cref="MandarDatosUsuario"/> en un objeto <see cref="Usuario"/>.
    /// </summary>
    /// <param name="dto">DTO con los datos del usuario.</param>
    /// <returns>Instancia de <see cref="Usuario"/> con los datos mapeados.</returns>
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

    /// <summary>
    /// Convierte un objeto <see cref="PersonajeDTO"/> en un objeto <see cref="Personaje"/>.
    /// </summary>
    /// <param name="dto">DTO con los datos del personaje.</param>
    /// <returns>Instancia de <see cref="Personaje"/> con los datos mapeados.</returns>
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

    /// <summary>
    /// Convierte un objeto <see cref="HabilidadeDTO"/> en un objeto <see cref="Habilidade"/>.
    /// </summary>
    /// <param name="dto">DTO con los datos de la habilidad.</param>
    /// <returns>Instancia de <see cref="Habilidade"/> con los datos mapeados.</returns>
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

    /// <summary>
    /// Convierte un objeto <see cref="PersonajeUsuarioDTO"/> en un objeto <see cref="PersonajeUsuario"/>.
    /// </summary>
    /// <param name="dto">DTO con los datos del personaje del usuario.</param>
    /// <returns>Instancia de <see cref="PersonajeUsuario"/> con los datos mapeados.</returns>
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

    /// <summary>
    /// Convierte un objeto <see cref="EquipoDTO"/> en un objeto <see cref="Equipo"/>.
    /// </summary>
    /// <param name="dto">DTO con los datos del equipo del usuario.</param>
    /// <returns>Instancia de <see cref="Equipo"/> con los datos mapeados.</returns>
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

    /// <summary>
    /// Convierte un objeto <see cref="PeleaDTO"/> en un objeto <see cref="Pelea"/>.
    /// </summary>
    /// <param name="dto">DTO con los datos de una pelea.</param>
    /// <returns>Instancia de <see cref="Pelea"/> con los datos mapeados.</returns>
    public static Pelea MapPelea(PeleaDTO dto)
    {
        return new Pelea
        {
            IdPeleas = dto.IdPeleas,
            ContrincanteUsuario = dto.ContrincanteUsuario,
            SoyGanador = dto.SoyGanador ? 1 : 0 // Propiedad privada: conversión de bool a int (1/0)
        };
    }
}

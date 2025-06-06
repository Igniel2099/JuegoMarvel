using System.Net;
using DnsClient;
using Microsoft.Extensions.Caching.Memory;

namespace JuegoMarvel.ModuloLogin.Model;

/// <summary>
/// Proporciona utilidades para comprobar si un dominio de correo electrónico puede recibir mensajes,
/// consultando los registros DNS MX, A o AAAA y utilizando caché para mejorar el rendimiento.
/// </summary>
public class ComprobadorDominio
{
    private readonly LookupClient _dnsClient;
    private readonly IMemoryCache _cache;
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="ComprobadorDominio"/> con un sistema de caché.
    /// </summary>
    /// <param name="cache">Instancia de <see cref="IMemoryCache"/> para almacenar resultados de dominios consultados.</param>
    public ComprobadorDominio(IMemoryCache cache)
    {
        _cache = cache;

        // 1. Definimos los endpoints de los servidores DNS
        var dnsEndpoints = new[]
        {
            new IPEndPoint(IPAddress.Parse("8.8.8.8"), 53),   // Google DNS
            new IPEndPoint(IPAddress.Parse("1.1.1.1"), 53)    // Cloudflare DNS
        };

        // 2. Creamos las opciones pasando los endpoints (auto-resolve queda en false)
        var options = new LookupClientOptions(dnsEndpoints)
        {
            Timeout = TimeSpan.FromSeconds(5),
            Retries = 1
        };

        // 3. Instanciamos el cliente con esas opciones
        _dnsClient = new LookupClient(options);
    }

    /// <summary>
    /// Comprueba si el dominio de un correo electrónico puede recibir mensajes (tiene registros MX, A o AAAA).
    /// Utiliza caché para evitar consultas repetidas.
    /// </summary>
    /// <param name="correoElectronico">Correo electrónico a comprobar.</param>
    /// <param name="cancellationToken">Token de cancelación opcional.</param>
    /// <returns>True si el dominio puede recibir correos, false en caso contrario o si el correo es inválido.</returns>
    public async Task<bool> ComprobarDominioCorreoElectronicoAsync(
        string correoElectronico,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(correoElectronico))
            return false;

        var partes = correoElectronico.Trim().Split('@');
        if (partes.Length != 2 || string.IsNullOrWhiteSpace(partes[1]))
            return false;

        var dominio = partes[1].ToLowerInvariant();

        if (_cache.TryGetValue(dominio, out bool cachedResult))
            return cachedResult;

        bool puedeRecibir;
        try
        {
            var resultado = await _dnsClient.QueryAsync(
                dominio,
                QueryType.MX,
                cancellationToken: cancellationToken);

            var mx = resultado.Answers.MxRecords();
            if (mx.Any())
            {
                puedeRecibir = true;
            }
            else
            {
                puedeRecibir = resultado.Answers.ARecords().Any()
                              || resultado.Answers.AaaaRecords().Any();
            }
        }
        catch
        {
            // Si ocurre un error de red o DNS, se asume que el dominio es válido para evitar falsos negativos.
            puedeRecibir = true;
        }

        _cache.Set(dominio, puedeRecibir, CacheDuration);
        return puedeRecibir;
    }
}
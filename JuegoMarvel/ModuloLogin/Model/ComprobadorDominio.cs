using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using DnsClient;
using Microsoft.Extensions.Caching.Memory;

namespace JuegoMarvel.ModuloLogin.Model;

public class ComprobadorDominio
{
    private readonly LookupClient _dnsClient;
    private readonly IMemoryCache _cache;
    private static readonly TimeSpan CacheDuration = TimeSpan.FromMinutes(10);

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
            puedeRecibir = true;
        }

        _cache.Set(dominio, puedeRecibir, CacheDuration);
        return puedeRecibir;
    }
}

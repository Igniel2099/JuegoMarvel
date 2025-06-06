using CommunityToolkit.Maui;
using JuegoMarvel.ModuloInicio.ViewModel;
using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.ViewModel;
using JuegoMarvel.Services;
using JuegoMarvel.Views;
using JuegoMarvelData.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace JuegoMarvel;

/// <summary>
/// Clase estática encargada de configurar y construir la aplicación MAUI.
/// Registra servicios, ViewModels, páginas y configuraciones necesarias para la app.
/// </summary>
public static class MauiProgram
{
    /// <summary>
    /// Método principal que configura el entorno de ejecución de la aplicación MAUI.
    /// </summary>
    /// <returns>Una instancia configurada de <see cref="MauiApp"/>.</returns>
    public static MauiApp CreateMauiApp()
    {
        /// <summary>
        /// Copia la base de datos SQLite si aún no existe en el sistema de archivos local.
        /// </summary>
        BaseDeDatosHelper.CopiarBaseDeDatosSiNoExiste();

        var builder = MauiApp.CreateBuilder();

        /// <summary>
        /// Registra el contexto de base de datos usando SQLite como proveedor.
        /// </summary>
        builder.Services.AddDbContext<BbddjuegoMarvelContext>(options =>
            options.UseSqlite($"Data Source={BaseDeDatosHelper.RutaBaseDeDatos}")
        );

        /// <summary>
        /// Agrega servicios auxiliares como caché en memoria y comprobador de dominios.
        /// </summary>
        builder.Services.AddMemoryCache();
        builder.Services.AddSingleton<ComprobadorDominio>();

        /// <summary>
        /// Carga el archivo de configuración appsettings.json desde los recursos de la app.
        /// </summary>
        using var stream = FileSystem
            .OpenAppPackageFileAsync("appsettings.json")
            .GetAwaiter()
            .GetResult();

        builder.Configuration
            .AddJsonStream(stream)
            .AddEnvironmentVariables();

        /// <summary>
        /// Mapea la sección "Servidor" a la clase <see cref="AppSettings"/> para inyección de dependencias.
        /// </summary>
        builder.Services
            .Configure<AppSettings>(builder.Configuration.GetSection("Servidor"));

        /// <summary>
        /// Registra <see cref="AppSettings"/> como singleton para acceso global a la configuración.
        /// </summary>
        builder.Services
            .AddSingleton(sp => sp.GetRequiredService<IOptions<AppSettings>>().Value);

        /// <summary>
        /// Registra páginas y ViewModels que se inyectarán por dependencias.
        /// </summary>
        builder.Services
            .AddTransient<LoginViewModel>();
        builder.Services
            .AddTransient<Login>();
        builder.Services
            .AddTransient<InicioViewModel>();
        builder.Services
            .AddTransient<Inicio>();

        /// <summary>
        /// Registra la clase principal <see cref="App"/> como singleton.
        /// </summary>
        builder.Services.AddSingleton<App>();

        /// <summary>
        /// Configura la app MAUI, CommunityToolkit y fuentes tipográficas.
        /// </summary>
        builder
            .UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("LuckiestGuy-Regular.ttf", "LuckiestGuy");
            });

#if DEBUG
        /// <summary>
        /// Agrega logging de depuración en modo DEBUG.
        /// </summary>
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}

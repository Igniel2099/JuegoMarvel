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

namespace JuegoMarvel
{

    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            BaseDeDatosHelper.CopiarBaseDeDatosSiNoExiste();

            var builder = MauiApp.CreateBuilder();

            builder.Services.AddDbContext<BbddjuegoMarvelContext>(options =>
                options.UseSqlite($"Data Source={BaseDeDatosHelper.RutaBaseDeDatos}")
            );

            // Añadir el servicio de comprobar el Dominio de un Correo Electronico
            builder.Services.AddMemoryCache();
            builder.Services.AddSingleton<ComprobadorDominio>();

            // Añadir el archivo de configuracion
            using var stream = FileSystem
                .OpenAppPackageFileAsync("appsettings.json")
                .GetAwaiter()
                .GetResult();

            builder.Configuration
               .AddJsonStream(stream)
               .AddEnvironmentVariables(); // opcional

            // Mapear la sección "Servidor" del appsettings.json a la clase AppSettings
            builder.Services
                .Configure<AppSettings>(builder.Configuration.GetSection("Servidor"));

            // Registrar la clase AppSettings como singleton
            builder.Services
                .AddSingleton(sp => sp.GetRequiredService<IOptions<AppSettings>>().Value);

            // Paginas y ViewModels Añadidas

            builder.Services
                .AddTransient<LoginViewModel>();    
            builder.Services
                .AddTransient<Login>();             

            builder.Services.AddTransient<InicioViewModel>();
            builder.Services.AddTransient<Inicio>();

            builder.Services
                .AddSingleton<App>(); // app principal

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
    		builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }    

}

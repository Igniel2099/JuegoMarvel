using CommunityToolkit.Maui;
using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.View;
using JuegoMarvel.ModuloLogin.ViewModel;
using JuegoMarvel.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.IO;
using Microsoft.Maui.Storage;

namespace JuegoMarvel
{

    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {

            var builder = MauiApp.CreateBuilder();

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

            builder.Services
                .AddTransient<LoginViewModel>();      // el ViewModel
            builder.Services
                .AddTransient<Login>();               // la página que recibe el VM por ctor
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

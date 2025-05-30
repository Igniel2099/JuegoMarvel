
using System.Diagnostics;

namespace JuegoMarvel.Services;

public static class BaseDeDatosHelper
{
    public static string RutaBaseDeDatos => Path.Combine(FileSystem.AppDataDirectory, "base_datos_juego_marvel.db");

    public static void CopiarBaseDeDatosSiNoExiste()
    {
        string destino = RutaBaseDeDatos;
        Debug.WriteLine(FileSystem.AppDataDirectory);

        if (!File.Exists(destino))
        {
            using var stream = FileSystem.OpenAppPackageFileAsync("Resources/BaseDeDatos/base_datos_juego_marvel.db").Result;
            using var archivoDestino = File.Create(destino);
            stream.CopyTo(archivoDestino);
        }
    }
}

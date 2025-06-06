using System.Diagnostics;

namespace JuegoMarvel.Services;

/// <summary>
/// Clase auxiliar para gestionar la base de datos local del juego.
/// Proporciona utilidades para obtener la ruta de almacenamiento y copiar el archivo de base de datos al dispositivo si aún no existe.
/// </summary>
public static class BaseDeDatosHelper
{
    /// <summary>
    /// Ruta absoluta del archivo de base de datos local, ubicada en el directorio de datos de la aplicación.
    /// </summary>
    public static string RutaBaseDeDatos => Path.Combine(FileSystem.AppDataDirectory, "base_datos_juego_marvel.db");

    /// <summary>
    /// Copia el archivo de base de datos desde los recursos del paquete de la app a la ubicación local del dispositivo,
    /// únicamente si aún no existe.
    /// </summary>
    public static void CopiarBaseDeDatosSiNoExiste()
    {
        string destino = RutaBaseDeDatos; // Propiedad privada: ruta del archivo de destino

        Debug.WriteLine(FileSystem.AppDataDirectory);

        if (!File.Exists(destino))
        {
            using var stream = FileSystem.OpenAppPackageFileAsync("Resources/BaseDeDatos/base_datos_juego_marvel.db").Result;

            using var archivoDestino = File.Create(destino);

            stream.CopyTo(archivoDestino);
        }
    }
}

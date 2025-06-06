namespace JuegoMarvel.ModuloLogin.Model;

/// <summary>
/// Representa la configuración de la aplicación para diferentes funciones de la app.
/// </summary>
public class AppSettings
{
    /// <summary>
    /// Dirección IP del servidor al que se conecta la aplicación.
    /// </summary>
    public string IpServidor { get; set; } = default!;

    /// <summary>
    /// Puerto del servidor al que se conecta la aplicación.
    /// </summary>
    public int PuertoServidor { get; set; }
}
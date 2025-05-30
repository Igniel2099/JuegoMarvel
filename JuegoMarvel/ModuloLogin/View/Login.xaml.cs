using JuegoMarvel.ModuloLogin.ViewModel;
using System.Diagnostics;

namespace JuegoMarvel.Views;

public partial class Login : ContentPage
{
	public Login(LoginViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
    }

    private  void OnConectarClicked(object sender, EventArgs e)
    {
    }

    private async void Button_Clicked(object sender, EventArgs e)
    {
        await CopiarBaseDeDatosAsync();
        Debug.WriteLine("\n\n\nCopiar Base de datos en Fichero Accesible\n\n\n");
    }

    // Update the method to fix the error
    private async Task CopiarBaseDeDatosAsync()
    {
        try
        {
#if ANDROID
        // Solicitar permisos de almacenamiento (para Android 13 o inferior)
        var status = await Permissions.RequestAsync<Permissions.StorageWrite>();

        if (status != PermissionStatus.Granted)
        {
            await DisplayAlert("Permiso Denegado", "Debes conceder el permiso de almacenamiento para copiar el archivo.", "OK");
            return;
        }

        string archivoOrigen = Path.Combine(FileSystem.AppDataDirectory, "base_datos_juego_marvel.db");

        // Puedes elegir una de las rutas accesibles:
        string rutaDestino = Path.Combine("/storage/emulated/0/Download", "base_datos_juego_marvel.db"); // Descargas
        // O: string rutaDestino = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "base_datos_juego_marvel.db");

        File.Copy(archivoOrigen, rutaDestino, true);
        await DisplayAlert("Éxito", $"Archivo copiado a {rutaDestino}", "OK");
#else
            await DisplayAlert("Error", "Esta función solo está disponible en Android.", "OK");
#endif
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo copiar el archivo: {ex.Message}", "OK");
        }
    }


}
using JuegoMarvel.ModuloAuxiliares.ModuloCarga.ViewModels;

namespace JuegoMarvel.ModuloAuxiliares.ModuloCarga;

/// <summary>
/// View de contenido que muestra la pantalla de carga y gestiona la animación y la carga de datos.
/// </summary>
public partial class PantallaDeCarga : ContentPage
{
    /// <summary>
    /// Inicializa una nueva instancia de <see cref="PantallaDeCarga"/> con el ViewModel proporcionado.
    /// </summary>
    /// <param name="vm">Instancia de <see cref="PantallaCargaViewModel"/> para el binding de la vista.</param>
    public PantallaDeCarga(PantallaCargaViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    /// <summary>
    /// Inicia la animación de rotación de la imagen de carga de forma indefinida.
    /// </summary>
    private async void StartRotation()
    {
        while (true)
        {
            await ImagenDeCarga.RotateTo(360, 2000, Easing.Linear);  // 360 grados en 2 segundos
            ImagenDeCarga.Rotation = 0;  // Reiniciar la rotación
        }
    }

    /// <summary>
    /// Se ejecuta cuando la página aparece en pantalla. Inicia la animación y la carga de datos en paralelo.
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        if (BindingContext is PantallaCargaViewModel vm)
        {
            // Creamos dos tareas para que se ejecuten en paralelo
            var tareaRotacion = Task.Run(() => StartRotation());
            var tareaCarga = Task.Run(() => vm.CargarDatosAsync());

            // Esperamos que ambas terminen
            await Task.WhenAll(tareaRotacion, tareaCarga);
        }
    }
}
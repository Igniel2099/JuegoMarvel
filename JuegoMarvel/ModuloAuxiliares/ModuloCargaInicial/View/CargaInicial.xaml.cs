using JuegoMarvel.ModuloAuxiliares.ModuloCargaInicial.ViewModel;

namespace JuegoMarvel.ModuloAuxiliares.ModuloCargaInicial.View;

public partial class CargaInicial : ContentPage
{
	public CargaInicial(CargaInicialViewModel vm)
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

        if (BindingContext is CargaInicialViewModel vm)
        {
            // Creamos dos tareas para que se ejecuten en paralelo
            var tareaRotacion = Task.Run(() => StartRotation());
            var tareaCarga = Task.Run(() => vm.CargarDatosAsync());

            // Esperamos que ambas terminen
            await Task.WhenAll(tareaRotacion, tareaCarga);
        }
    }
}
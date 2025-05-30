using JuegoMarvel.ModuloAuxiliares.ModuloCarga.ViewModels;

namespace JuegoMarvel.ModuloAuxiliares.ModuloCarga;

public partial class PantallaDeCarga : ContentPage
{
	public PantallaDeCarga(PantallaCargaViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
    }

    private async void StartRotation()
    {
        while (true)
        {
            await ImagenDeCarga.RotateTo(360, 2000, Easing.Linear);  // 360 grados en 2 segundos
            ImagenDeCarga.Rotation = 0;  // Reiniciar la rotación
        }
    }

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
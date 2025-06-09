using JuegoMarvel.ModuloJuego.Model;
using JuegoMarvel.ModuloJuego.ViewModel;
using MensajesServidor;

namespace JuegoMarvel.ModuloJuego.View;

/// <summary>
/// Página para buscar un jugador y mostrar animaciones de espera.
/// </summary>
public partial class BuscarJugador : ContentPage
{
    private readonly ClienteJuego _cliente;
    private readonly BuscarJugadorViewModel _vm;

    // Para controlar la cancelación de la animación
    private CancellationTokenSource _animCts;

    public BuscarJugador(BuscarJugadorViewModel vm, ClienteJuego cliente)
    {
        _cliente = cliente;
        _vm = vm;
        InitializeComponent();
        BindingContext = vm;
    }

    /// <summary>
    /// Inicia una animación infinita de los bordes, repitiendo la secuencia indefinidamente.
    /// </summary>
    private async Task RepetirAnimacionBorderAsync(CancellationToken ct)
    {
        while (!ct.IsCancellationRequested)
        {
            await AnimarBordersSecuencialmente(ct);
            // Delay entre repeticiones completas
            await Task.Delay(1000, ct);
        }
    }

    /// <summary>
    /// Anima los bordes subiéndolos arriba y abajo de manera secuencial, uno por uno.
    /// </summary>
    private async Task AnimarBordersSecuencialmente(CancellationToken ct)
    {
        var borders = new[] { Border1, Border2, Border3 };

        foreach (var border in borders)
        {
            if (ct.IsCancellationRequested)
                return;

            await border.TranslateTo(0, -10, 150, Easing.CubicOut);
            await border.TranslateTo(0, 0, 150, Easing.CubicIn);

            // Retardo entre cada uno
            await Task.Delay(200, ct);
        }
    }

    private async Task<string> IniciarConexionJuegoAsync()
    {
        await Task.Run(() => _cliente.Conectar());

        await _cliente.Enviar(new MensajesModuloJuego
        {
            TipoMensaje = EnumMensajeJuego.MandarPersonaje,
            Valor = _vm.NombrePersonajeDos
        });

        var msgOponente = await _cliente.Recibir();
        return msgOponente.Valor;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();

        // 1) Creamos un nuevo CancellationTokenSource
        _animCts?.Cancel();
        _animCts = new CancellationTokenSource();

        // 2) Arrancamos la animación en paralelo
        _ = RepetirAnimacionBorderAsync(_animCts.Token);

        // 3) Llamamos a conectar y navegar
        _ = ConectarYNavegarAsync();
    }

    protected override void OnDisappearing()
    {
        base.OnDisappearing();

        // Cancelamos la animación al cambiar de página
        _animCts?.Cancel();
    }

    private async Task ConectarYNavegarAsync()
    {
        try
        {
            string nombreOponente = await IniciarConexionJuegoAsync();

            // Antes de reemplazar la MainPage, cancelamos la animación por si acaso
            _animCts?.Cancel();

            // Crea la nueva página de juego y la pone como raíz
            var juegoPage = new Juego(_cliente, _vm.NombrePersonajeDos, nombreOponente);
            NavigationPage.SetHasNavigationBar(juegoPage, false);
            Application.Current.MainPage = new NavigationPage(juegoPage);
        }
        catch (Exception ex)
        {
            await DisplayAlert("Error", $"No se pudo conectar: {ex.Message}", "OK");
        }
    }
}

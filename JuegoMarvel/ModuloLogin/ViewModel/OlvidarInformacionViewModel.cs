using JuegoMarvel.ModuloLogin.ViewModel.Comandos;
using System.Diagnostics;

namespace JuegoMarvel.ModuloLogin.ViewModel;

public class OlvidarInformacionViewModel : BaseViewModel
{
    private string? _correoElectronico;
    public string? CorreoElectronico
    {
        get => _correoElectronico;
        set
        {
            if (_correoElectronico != value)
            {
                _correoElectronico = value;
                OnPropertyChanged();
                Debug.WriteLine(value);
            }
        }
    }

    private string? _codigoConfirmacion;
    public string? CodigoConfirmacion
    {
        get => _codigoConfirmacion;
        set
        {
            if (_codigoConfirmacion != value)
            {
                _codigoConfirmacion = value;
                OnPropertyChanged();
                Debug.WriteLine(value);
            }
        }
    }

    public ComandoEnviar ComandoEnviar { get; set; }
    public ComandoConfirmarOlvidarInformacion ComandoConfirmar { get; set; }
    public ComandoVolverEnviar ComandoVolverEnviar { get; set; }
    public ComandoNavegarVolverAtras ComandoNavVolverAtras { get; set; }

    public OlvidarInformacionViewModel()
    {
        CorreoElectronico = string.Empty;
        CodigoConfirmacion = string.Empty;
        ComandoEnviar = new();
        ComandoConfirmar = new();
        ComandoVolverEnviar = new();
        ComandoNavVolverAtras = new();
    }
}

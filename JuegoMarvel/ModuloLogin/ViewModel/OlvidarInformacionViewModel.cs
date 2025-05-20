using JuegoMarvel.ModuloLogin.Model;
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


    private bool _editable;
    public bool Editable
    {
        get => _editable;
        set
        {
            if (_editable != value)
            {
                _editable = value;
                OnPropertyChanged();
            }
        }
    }
    public ComandoEnviar ComandoEnviar { get; set; }
    public ComandoConfirmarOlvidarInformacion ComandoConfirmar { get; set; }
    public ComandoNavegarVolverAtras ComandoNavVolverAtras { get; set; }

    public OlvidarInformacionViewModel(AppSettings settings, ComprobadorDominio comprobador)
    {
        _editable = false;
        CorreoElectronico = string.Empty;
        CodigoConfirmacion = string.Empty;
        ComandoEnviar = new(this, settings, comprobador);
        ComandoConfirmar = new(this,settings);
        ComandoNavVolverAtras = new();
    }
}

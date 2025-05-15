
using JuegoMarvel.ModuloLogin.Model;
using System.Collections.ObjectModel;

namespace JuegoMarvel.ModuloLogin.ViewModel;

public class PopupErroresCrearCuentaViewModel : BaseViewModel
{
    private string? _tituloPantalla;
    public string? TituloPantalla
    {
        get => _tituloPantalla;
        set
        {
            if (_tituloPantalla != value)
            {
                _tituloPantalla = value;
                OnPropertyChanged();
            }
        }
    }

    private string? _textoErrorNombreUsuario;
    public string? TextoErrorNombreUsuario
    {
        get => _textoErrorNombreUsuario;
        set
        {
            if (_textoErrorNombreUsuario != value)
            {
                _textoErrorNombreUsuario = value;
                OnPropertyChanged();
            }
        }
    }

    private string? _textoErrorCorreoElectronico;
    public string? TextoErrorCorreoElectronico
    {
        get => _textoErrorCorreoElectronico;
        set
        {
            if (_textoErrorCorreoElectronico != value)
            {
                _textoErrorCorreoElectronico = value;
                OnPropertyChanged();
            }
        }
    }


    private string? _textoErrorContrasena;
    public string? TextoErrorContrasena
    {
        get => _textoErrorContrasena;
        set
        {
            if (_textoErrorContrasena != value)
            {
                _textoErrorContrasena = value;
                OnPropertyChanged();
            }
        }
    }

    private bool _contendorTextosVisible;
    public bool ContendorTextosVisible
    {
        get => _contendorTextosVisible;
        set
        {
            if (_contendorTextosVisible != value)
            {
                _contendorTextosVisible = value;
                OnPropertyChanged();
            }
        }
    }

    public ObservableCollection<ErrorItem> MensajesError { get; } = [];

    public void SetErrores(params string?[] mensajes)
    {
        MensajesError.Clear();
        foreach (var msg in mensajes)
            if (!string.IsNullOrWhiteSpace(msg))
                MensajesError.Add(new ErrorItem { Texto = msg });
    }

    public PopupErroresCrearCuentaViewModel(
        string? txtErrorNombreUsuario, 
        string? txtErrorCorreoElec, 
        string? txtErrorContrasena)
    {

        _textoErrorNombreUsuario = txtErrorNombreUsuario;
        _textoErrorCorreoElectronico= txtErrorCorreoElec;
        _textoErrorContrasena = txtErrorContrasena;

        SetErrores(
            _textoErrorNombreUsuario,
           _textoErrorCorreoElectronico,
            _textoErrorContrasena
        );



        if (string.IsNullOrEmpty(txtErrorNombreUsuario) && string.IsNullOrEmpty(txtErrorCorreoElec) && string.IsNullOrEmpty(txtErrorContrasena))
        {
            _tituloPantalla = "FELICIDADES! \n SE CREO LA CUENTA DE FORMA EXITOSA";
            _contendorTextosVisible = false;
        }
        else
        {
            _contendorTextosVisible = true;
            _tituloPantalla = "ERROR AL CREAR CUENTA";
        }
    }

    public PopupErroresCrearCuentaViewModel()
    {
    }
}

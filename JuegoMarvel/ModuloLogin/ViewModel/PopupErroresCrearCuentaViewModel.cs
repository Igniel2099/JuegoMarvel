using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JuegoMarvel.ModuloLogin.ViewModel;

public class PopupErroresCrearCuentaViewModel : BaseViewModel
{
    private string? _contenidoTexto;
    public string? ContenidoTexto
    {
        get => _contenidoTexto;
        set
        {
            if (_contenidoTexto != null && _contenidoTexto != value)
            {
                _contenidoTexto = value;
                OnPropertyChanged();
            }
        }
    }

    public PopupErroresCrearCuentaViewModel(string contenidoTexto)
    {
        ContenidoTexto = contenidoTexto;
    }

    public PopupErroresCrearCuentaViewModel()
    {
    }
}

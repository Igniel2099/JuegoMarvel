using JuegoMarvel.ClasesBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace JuegoMarvel.ModuloJuego.ViewModel;

public class ResultadoJugadorViewModel : BaseViewModel
{
    private string _resultado;
    /// <summary>
    /// Texto a mostrar en la etiqueta, por ejemplo "Ganaste" o "Perdiste".
    /// </summary>
    public string Resultado
    {
        get => _resultado;
        set
        {
            if (_resultado != value)
            {
                _resultado = value;
                OnPropertyChanged();
            }
        }
    }

    private string _imgResultado;
    /// <summary>
    /// Ruta de la imagen de resultado ("png", "jpg", etc.).
    /// </summary>
    public string ImgResultado
    {
        get => _imgResultado;
        set
        {
            if (_imgResultado != value)
            {
                _imgResultado = value;
                OnPropertyChanged();
            }
        }
    }

    /// <summary>
    /// Constructor del ViewModel de la página de resultado.
    /// </summary>
    /// <param name="resultadoJugador">Texto recibido si es Ganaste o Perdi.</param>
    public ResultadoJugadorViewModel(string resultadoJugador)
    {
        Resultado = resultadoJugador == "Ganaste"
            ? "Felicidades Ganaste"
            : "Mala suerte Perdiste";
            
        ImgResultado = resultadoJugador == "Ganaste"
            ? "icono_ganaste.png"
            : "icono_perdiste.png";
    }
}

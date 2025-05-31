using System.Globalization;
namespace JuegoMarvel.Services;

public class TipoHabilidadAImagenConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string tipo)
        {
            return tipo.ToLowerInvariant() switch
            {
                "curacion" => "icono_vitality.png",
                "ataque" => "icono_attack.png",
                "escudo" => "icono_shield.png",
                _ => "icono_default.png",// Imagen por defecto si no coincide
            };
        }
        return "icono_default.png"; // Imagen por defecto si no es string
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException(); // Solo usamos OneWay binding
    }
}



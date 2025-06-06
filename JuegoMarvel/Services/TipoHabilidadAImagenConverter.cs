using System.Globalization;

namespace JuegoMarvel.Services;

/// <summary>
/// Conversor de valores para traducir un tipo de habilidad (string) a la ruta de su icono correspondiente.
/// Implementa <see cref="IValueConverter"/> y se utiliza en bindings OneWay en la UI.
/// </summary>
public class TipoHabilidadAImagenConverter : IValueConverter
{
    /// <summary>
    /// Convierte un valor de tipo string (tipo de habilidad) a la ruta de una imagen.
    /// </summary>
    /// <param name="value">El valor a convertir. Debe ser un string como "curacion", "ataque" o "escudo".</param>
    /// <param name="targetType">Tipo de destino (usualmente <see cref="ImageSource"/>).</param>
    /// <param name="parameter">Parámetro opcional no utilizado.</param>
    /// <param name="culture">Información cultural para la conversión (no usada).</param>
    /// <returns>Nombre del archivo de imagen correspondiente al tipo de habilidad.</returns>
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string tipo)
        {
            return tipo.ToLowerInvariant() switch
            {
                "curacion" => "icono_vitality.png",
                "ataque" => "icono_attack.png",
                "escudo" => "icono_shield.png",
                _ => "icono_default.png" // Imagen por defecto si el tipo no es reconocido
            };
        }

        return "icono_default.png"; // Imagen por defecto si no se recibe un string
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException(); // Solo se usa en binding OneWay
    }
}

using System.Text.Json.Serialization;

namespace JuegoMarvel.ModuloTienda.Model;

/// <summary>
/// Diccionario que almacena la información visual de todos los personajes, indexados por su nombre (por ejemplo, "Cyclops", "Deadpool", etc.).
/// Cada entrada contiene un objeto <see cref="PersonajeImg"/> con los datos gráficos y habilidades del personaje.
/// Esta clase sirve principalmente para Deserializar el Json que contiene toda la información de los personajes.
/// </summary>
public class PersonajesImagenes : Dictionary<string, PersonajeImg>
{
}

/// <summary>
/// Representa la información gráfica y las habilidades de un personaje.
/// Incluye imágenes principales y una lista de habilidades con sus respectivos sprites y casillas.
/// </summary>
public class PersonajeImg
{
    /// <summary>
    /// Ruta de la imagen principal del personaje.
    /// </summary>
    [JsonPropertyName("ImgPrincipal")]
    public string ImgPrincipal { get; set; }

    /// <summary>
    /// Ruta de la imagen del cuerpo del personaje.
    /// </summary>
    [JsonPropertyName("ImgCuerpo")]
    public string ImgCuerpo { get; set; }

    /// <summary>
    /// Lista de habilidades del personaje, cada una con su información gráfica y de animación.
    /// </summary>
    [JsonPropertyName("Habilidades")]
    public List<HabilidadImg> Habilidades { get; set; }
}

/// <summary>
/// Representa la información gráfica de una habilidad de un personaje.
/// Incluye nombre, distancia, casillas y sprites animados.
/// </summary>
public class HabilidadImg
{
    /// <summary>
    /// Nombre de la habilidad.
    /// </summary>
    [JsonPropertyName("Nombre")]
    public string Nombre { get; set; }

    /// <summary>
    /// Distancia de la habilidad (por ejemplo, "corto", "largo").
    /// </summary>
    [JsonPropertyName("Distancia")]
    public string Distancia { get; set; }

    /// <summary>
    /// Información de las casillas asociadas a la habilidad (original, pulsada, deshabilitada).
    /// </summary>
    [JsonPropertyName("Casillas")]
    public Casillas Casillas { get; set; }

    /// <summary>
    /// Sprites animados de la habilidad, organizados por categoría (estático, adelante, ataque, atrás).
    /// </summary>
    [JsonPropertyName("Sprites")]
    public Sprites Sprites { get; set; }
}

/// <summary>
/// Representa las diferentes imágenes de casillas asociadas a una habilidad.
/// </summary>
public class Casillas
{
    /// <summary>
    /// Imagen de la casilla en estado original.
    /// </summary>
    [JsonPropertyName("Original")]
    public string Original { get; set; }

    /// <summary>
    /// Imagen de la casilla cuando está pulsada.
    /// </summary>
    [JsonPropertyName("Pulsada")]
    public string Pulsada { get; set; }

    /// <summary>
    /// Imagen de la casilla cuando está deshabilitada.
    /// </summary>
    [JsonPropertyName("Deshabilitada")]
    public string Deshabilitada { get; set; }
}

/// <summary>
/// Contenedor de las diferentes categorías de sprites animados de una habilidad.
/// Incluye animaciones para los estados: estático, adelante, ataque y atrás.
/// </summary>
public class Sprites
{
    /// <summary>
    /// Categoria del Sprite cuando el personaje esta Estatico
    /// </summary>
    [JsonPropertyName("Estatico")]
    public CategoriaSprite Estatico { get; set; }

    /// <summary>
    /// Categoria del Sprite cuando el personaje se mueve hacia adelante
    /// </summary>
    [JsonPropertyName("Adelante")]
    public CategoriaSprite Adelante { get; set; }

    /// <summary>
    /// Categoria del Sprite cuando el personaje esta atacando
    /// </summary>
    [JsonPropertyName("Ataque")]
    public CategoriaSprite Ataque { get; set; }

    /// <summary>
    /// Categoria del Sprite cuando el personaje se mueve hacia atras.
    /// </summary>
    [JsonPropertyName("Atras")]
    public CategoriaSprite Atras { get; set; }
}

/// <summary>
/// Representa una categoría de animación de sprites (por ejemplo, estático, ataque).
/// Incluye el tiempo de animación y la lista de frames.
/// </summary>
public class CategoriaSprite
{
    /// <summary>
    /// Tiempo de duración de la animación (en milisegundos o formato definido en el JSON).
    /// </summary>
    [JsonPropertyName("Tiempo")]
    public string Tiempo { get; set; }

    /// <summary>
    /// Lista de frames (imágenes) que componen la animación.
    /// </summary>
    [JsonPropertyName("Frames")]
    public List<SpriteFrame> Frames { get; set; }
}

/// <summary>
/// Representa un frame individual de una animación de sprite.
/// </summary>
public class SpriteFrame
{
    /// <summary>
    /// Ruta de la imagen del frame.
    /// </summary>
    [JsonPropertyName("path")]
    public string Path { get; set; }
}
using System.Text.Json.Serialization;

namespace JuegoMarvel.ModuloTienda.Model;

// Este diccionario contendrá todas las entradas bajo las claves "Cyclops", "Deadpool", etc.
public class PersonajesImagenes : Dictionary<string, PersonajeImg>
{
}

public class PersonajeImg
{
    [JsonPropertyName("ImgPrincipal")]
    public string ImgPrincipal { get; set; }

    [JsonPropertyName("ImgCuerpo")]
    public string ImgCuerpo { get; set; }

    // Coincide con la propiedad "Habilidades": un arreglo de objetos.
    [JsonPropertyName("Habilidades")]
    public List<HabilidadImg> Habilidades { get; set; }
}

public class HabilidadImg
{
    // Coincide con "Nombre"
    [JsonPropertyName("Nombre")]
    public string Nombre { get; set; }

    // Coincide con "Distancia"
    [JsonPropertyName("Distancia")]
    public string Distancia { get; set; }

    // Coincide con "Casillas"
    [JsonPropertyName("Casillas")]
    public Casillas Casillas { get; set; }

    // Coincide con el nuevo objeto "Sprites", que contiene cuatro categorías.
    [JsonPropertyName("Sprites")]
    public Sprites Sprites { get; set; }
}

public class Casillas
{
    // Coincide con "Original"
    [JsonPropertyName("Original")]
    public string Original { get; set; }

    // Coincide con "Pulsada"
    [JsonPropertyName("Pulsada")]
    public string Pulsada { get; set; }

    // Coincide con "Deshabilitada"
    [JsonPropertyName("Deshabilitada")]
    public string Deshabilitada { get; set; }
}

// Representa el contenedor de las cuatro categorías dentro de "Sprites"
public class Sprites
{
    [JsonPropertyName("Estatico")]
    public CategoriaSprite Estatico { get; set; }

    [JsonPropertyName("Adelante")]
    public CategoriaSprite Adelante { get; set; }

    [JsonPropertyName("Ataque")]
    public CategoriaSprite Ataque { get; set; }

    [JsonPropertyName("Atras")]
    public CategoriaSprite Atras { get; set; }
}

// Cada categoría ("Estatico", "Adelante", "Ataque", "Atras") tiene un tiempo y una lista de frames.
public class CategoriaSprite
{
    [JsonPropertyName("Tiempo")]
    public string Tiempo { get; set; }

    [JsonPropertyName("Frames")]
    public List<SpriteFrame> Frames { get; set; }
}

// Cada frame ahora se deserializa a este tipo, que coincide con { "path": "..." } en el JSON
public class SpriteFrame
{
    [JsonPropertyName("path")]
    public string Path { get; set; }
}

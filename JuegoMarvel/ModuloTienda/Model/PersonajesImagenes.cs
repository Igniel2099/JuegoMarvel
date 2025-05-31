using System;
using System.Collections.Generic;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace JuegoMarvel.ModuloTienda.Model;

public class PersonajeImg
{
    [JsonPropertyName("ImgPrincipal")]
    public string ImgPrincipal { get; set; }
    public string ImgCuerpo { get; set; }
    [JsonPropertyName("ImgHabilidades")]
    public Dictionary<string, string> ImgHabilidades { get; set; }
}

public class PersonajesImagenes : Dictionary<string, PersonajeImg>
{
}

 


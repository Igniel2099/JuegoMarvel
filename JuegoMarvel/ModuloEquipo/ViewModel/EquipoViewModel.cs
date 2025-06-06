using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloEquipo.ViewModel.Comandos;
using JuegoMarvel.ModuloTienda.Model;
using JuegoMarvel.Services;
using JuegoMarvelData.Data;
using JuegoMarvelData.Models;
using System.Collections.ObjectModel;

namespace JuegoMarvel.ModuloEquipo.ViewModel;

/// <summary>
/// ViewModel para la gestión del equipo de personajes del usuario.
/// Permite añadir, eliminar y seleccionar personajes, así como inicializar la información necesaria.
/// </summary>
public class EquipoViewModel : BaseViewModel
{
    #region Region CamposViewModel

    /// <summary>
    /// Propiedad privada de la imagen del primero personaje Equipo
    /// </summary>
    private string? _personajeUnoEquipo;

    /// <summary>
    /// Imagen del primer personaje del equipo.
    /// </summary>
    public string? PersonajeUnoEquipo
    {
        get => _personajeUnoEquipo;
        set
        {
            if (value == _personajeUnoEquipo) return;
            _personajeUnoEquipo = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad privada de la imagen del segundo personaje Equipo
    /// </summary>
    private string? _personajeDosEquipo;
    /// <summary>
    /// Imagen del segundo personaje del equipo.
    /// </summary>
    public string? PersonajeDosEquipo
    {
        get => _personajeDosEquipo;
        set
        {
            if (value == _personajeDosEquipo) return;
            _personajeDosEquipo = value;
            OnPropertyChanged();
        }
    }

    /// <summary>
    /// Propiedad privada de la imagen del tercer personaje Equipo
    /// </summary>
    private string? _personajeTresEquipo;
    /// <summary>
    /// Imagen del tercer personaje del equipo.
    /// </summary>
    public string? PersonajeTresEquipo
    {
        get => _personajeTresEquipo;
        set
        {
            if (value == _personajeTresEquipo) return;
            _personajeTresEquipo = value;
            OnPropertyChanged();
        }
    }
    #endregion

    #region Comandos

    /// <summary>
    /// Comando para añadir un personaje al equipo.
    /// </summary>
    public ComandoAnadirPersonaje ComandoAnadirPersonaje { get; set; }

    /// <summary>
    /// Comando para eliminar un personaje del equipo.
    /// </summary>
    public ComandoEliminarPersonaje ComandoEliminarPersonaje { get; set; }

    /// <summary>
    /// Comando para navegar hacia atrás.
    /// </summary>
    public ComandoNavegarVolverAtras NavAtras { get; set; }
    #endregion

    #region CamposAuxiliares

    /// <summary>
    /// Colección de personajes de usuario disponibles para el equipo.
    /// </summary>
    public ObservableCollection<PersonajeUsuarioViewModel> PersonajesUsuarios { get; set; }

    /// <summary>
    /// Personaje de usuario actualmente seleccionado.
    /// </summary>
    public PersonajeUsuarioViewModel? PersonajeUsuarioSeleccionado { get; set; }

    /// <summary>
    /// Contexto de la base de datos.
    /// </summary>
    public readonly BbddjuegoMarvelContext Context;
    #endregion

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="EquipoViewModel"/>.
    /// </summary>
    /// <param name="context">Contexto de la base de datos.</param>
    public EquipoViewModel(BbddjuegoMarvelContext context)
    {
        Context = context;
        // Comandos
        ComandoAnadirPersonaje = new(this);
        ComandoEliminarPersonaje = new(this);
        NavAtras = new();

        // personajes
        _personajeUnoEquipo = string.Empty;
        _personajeDosEquipo = string.Empty;
        _personajeTresEquipo = string.Empty;
        PersonajesUsuarios = [];
    }

    /// <summary>
    /// Obtiene la información necesaria para inicializar los personajes del usuario.
    /// </summary>
    /// <param name="gestionPersonajes">Instancia de <see cref="GestionPersonajes"/> para obtener los datos.</param>
    /// <returns>Tupla con nombres, lista de personajes de usuario y habilidades.</returns>
    private static  async Task<(Dictionary<int, string> nombres, List<PersonajeUsuario> personajesUsuarios, List<Habilidade> habilidadesPerUsu)> ObtenerInformacionNecesaria(GestionPersonajes gestionPersonajes)
    {
        List<PersonajeUsuario> listPersonajeUsuarios = gestionPersonajes.ObtenerPersonajesUsuario();
        Dictionary<int, string> nombres = gestionPersonajes.ObtenerNombresPersonajesUsuario(listPersonajeUsuarios);
        List<Habilidade> habilidadesPerUsu = gestionPersonajes.ObtenerHabilidadesPersonajesUsuarios(listPersonajeUsuarios);

        return (nombres, listPersonajeUsuarios, habilidadesPerUsu);
    }

    /// <summary>
    /// Carga las imágenes de los personajes que forman parte del equipo.
    /// </summary>
    /// <param name="gestionPersonajes">Instancia de <see cref="GestionPersonajes"/>.</param>
    /// <param name="personajesImagenes">Diccionario de imágenes de personajes.</param>
    private async Task CargarImagenesPersonajesEquipo(GestionPersonajes gestionPersonajes, PersonajesImagenes personajesImagenes)
    {
        var listaNombresPersonajesUsuarioEquipo = await gestionPersonajes.CargarNombresEquipoPersonajesUsuario();
        if (listaNombresPersonajesUsuarioEquipo != null)
        {
            for (int i = 0; i < listaNombresPersonajesUsuarioEquipo.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        PersonajeUnoEquipo = personajesImagenes[listaNombresPersonajesUsuarioEquipo[i]].ImgCuerpo;
                        continue;
                    case 1:
                        PersonajeDosEquipo = personajesImagenes[listaNombresPersonajesUsuarioEquipo[i]].ImgCuerpo;
                        continue;
                    case 2:
                        PersonajeTresEquipo = personajesImagenes[listaNombresPersonajesUsuarioEquipo[i]].ImgCuerpo;
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Carga los ViewModels de los personajes de usuario y los añade a la colección.
    /// </summary>
    /// <param name="personajesUsuarios">Lista de personajes de usuario.</param>
    /// <param name="nombres">Diccionario de nombres de personajes.</param>
    /// <param name="personajesImagenes">Diccionario de imágenes de personajes.</param>
    /// <param name="habilidadesPerUsu">Lista de habilidades de los personajes de usuario.</param>
    private async Task CargarPersonajeUsuariosViewModel(List<PersonajeUsuario> personajesUsuarios, Dictionary<int, string> nombres, PersonajesImagenes personajesImagenes, List<Habilidade> habilidadesPerUsu)
    {
        foreach (var personajeUsuario in personajesUsuarios)
        {
            string nombre = nombres[(int)personajeUsuario.IdPersonaje!];
            PersonajeImg personjaeImg = personajesImagenes[nombre];

            var habilidadesActuales = habilidadesPerUsu
                .Where(h => h.IdPersonaje == personajeUsuario.IdPersonaje)
                .ToList();

            var diccImgHabilidades = personjaeImg.Habilidades
                .ToDictionary(h => h.Nombre, h => h.Casillas.Original);

            string imgHUno = diccImgHabilidades[habilidadesActuales[0].Nombre];
            string imgHDos = diccImgHabilidades[habilidadesActuales[1].Nombre];
            string imgHTres = diccImgHabilidades[habilidadesActuales[2].Nombre];

            PersonajesUsuarios.Add(new PersonajeUsuarioViewModel(
                personajeUsuario.IdPersonajeUsuario,
                this,
                nombre,
                habilidadesActuales[0].Tipo,
                habilidadesActuales[1].Tipo,
                habilidadesActuales[2].Tipo,
                personjaeImg.ImgCuerpo,
                imgHUno,
                imgHDos,
                imgHTres
            ));
        }
    }

    /// <summary>
    /// Inicializa los ViewModels de los personajes de usuario y carga las imágenes del equipo.
    /// </summary>
    /// <param name="context">DbContext de la base de datos.</param>
    public async Task InicializarPersonajesUsuarioViewModelAync(BbddjuegoMarvelContext context)
    {
        GestionPersonajes gestionPersonajes = new(context);

        var (nombres, personajesUsuarios, habilidadesPerUsu) = await ObtenerInformacionNecesaria(gestionPersonajes);
        var personajesImagenes = await GestionPersonajes.CargarPersonajesJsonAsync();

        await CargarImagenesPersonajesEquipo(gestionPersonajes, personajesImagenes);

        await CargarPersonajeUsuariosViewModel(personajesUsuarios, nombres, personajesImagenes, habilidadesPerUsu);
    }
}
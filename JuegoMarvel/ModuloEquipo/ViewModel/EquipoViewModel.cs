using JuegoMarvel.ModuloEquipo.ViewModel.Comandos;
using JuegoMarvel.ModuloTienda.Model;
using JuegoMarvel.Services;
using JuegoMarvelData.Data;
using JuegoMarvelData.Models;
using System.Collections.ObjectModel;

namespace JuegoMarvel.ModuloEquipo.ViewModel;

public class EquipoViewModel : BaseViewModel
{
    private string? _personajeUnoEquipo;
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

    private string? _personajeDosEquipo;
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

    private string? _personajeTresEquipo;
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

    public ObservableCollection<PersonajeUsuarioViewModel> PersonajesUsuarios { get; set; }
    public ComandoAnadirPersonaje ComandoAnadirPersonaje { get; set; }
    public ComandoEliminarPersonaje ComandoEliminarPersonaje { get; set; }
    public ComandoNavegarVolverAtras NavAtras { get; set; }
    public PersonajeUsuarioViewModel? PersonajeUsuarioSeleccionado{ get; set; }

    public readonly BbddjuegoMarvelContext Context;
    public EquipoViewModel(BbddjuegoMarvelContext context )
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

    private async Task<(Dictionary<int, string> nombres,List<PersonajeUsuario> personajesUsuarios, List<Habilidade> habilidadesPerUsu)> ObtenerInformacionNecesaria(GestionPersonajes gestionPersonajes)
    {
        List<PersonajeUsuario> listPersonajeUsuarios = gestionPersonajes.ObtenerPersonajesUsuario();
        Dictionary<int, string> nombres = gestionPersonajes.ObtenerNombresPersonajesUsuario(listPersonajeUsuarios);
        List<Habilidade> habilidadesPerUsu = gestionPersonajes.ObtenerHabilidadesPersonajesUsuarios(listPersonajeUsuarios);

        return (nombres, listPersonajeUsuarios, habilidadesPerUsu);
    }

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



    private async Task CargarPersonajeUsuariosViewModel(List<PersonajeUsuario> personajesUsuarios,  Dictionary<int,string> nombres, PersonajesImagenes personajesImagenes, List<Habilidade> habilidadesPerUsu)
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
    public async Task InicializarPersonajesUsuarioViewModelAync(BbddjuegoMarvelContext context)
    {
        GestionPersonajes gestionPersonajes = new(context);

        var (nombres, personajesUsuarios, habilidadesPerUsu) = await ObtenerInformacionNecesaria(gestionPersonajes);
        var personajesImagenes = await GestionPersonajes.CargarPersonajesJsonAsync();

        await CargarImagenesPersonajesEquipo(gestionPersonajes, personajesImagenes);

        await CargarPersonajeUsuariosViewModel(personajesUsuarios, nombres, personajesImagenes, habilidadesPerUsu);
    }
}

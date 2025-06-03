using JuegoMarvel.ModuloEquipo.ViewModel.Comandos;
using JuegoMarvel.ModuloTienda.Model;
using JuegoMarvelData.Data;
using JuegoMarvelData.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

    public PersonajeUsuarioViewModel? PersonajeUsuarioSeleccionado{ get; set; }
    public EquipoViewModel(BbddjuegoMarvelContext context )
    {
        // Comandos
        ComandoAnadirPersonaje = new();
        ComandoEliminarPersonaje = new();

        // personajes
        _personajeUnoEquipo = null;
        _personajeDosEquipo = null;
        _personajeTresEquipo = null;
    }

    private async Task<(Dictionary<int, string> nombres,List<PersonajeUsuario> personajesUsuarios, List<Habilidade> habilidadesPerUsu)> ObtenerInformacionNecesaria(BbddjuegoMarvelContext context)
    {
        GestionPersonajes gestionPersonajes = new(context);
        List<PersonajeUsuario> listPersonajeUsuarios = gestionPersonajes.ObtenerPersonajesUsuario();
        Dictionary<int, string> nombres = gestionPersonajes.ObtenerNombresPersonajesUsuario(listPersonajeUsuarios);
        List<Habilidade> habilidadesPerUsu = gestionPersonajes.ObtenerHabilidadesPersonajesUsuarios(listPersonajeUsuarios);

        return (nombres, listPersonajeUsuarios, habilidadesPerUsu);
    }

    public async Task InicializarPersonajesUsuarioViewModelAync(BbddjuegoMarvelContext context)
    {
        var (nombres, personajesUsuarios, habilidadesPerUsu) = await ObtenerInformacionNecesaria(context);
        var personajesImagenes = await GestionPersonajes.CargarPersonajesJsonAsync();

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

}

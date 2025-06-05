using System.Windows.Input;
using JuegoMarvel.Services;
using JuegoMarvelData.Data;

namespace JuegoMarvel.ModuloJuego.ViewModel;

public class BuscarJugadorViewModel : BaseViewModel
{
    private string _nombre;
    public string Nombre
    {
        get => _nombre;
        set
        {
            if (_nombre != value)
            {
                _nombre = value;
                OnPropertyChanged();
            }
        }
    }

    private string _imgCuerpoJugadorUno;
    public string ImgCuerpoJugadorUno
    {
        get => _imgCuerpoJugadorUno;
        set
        {
            if (_imgCuerpoJugadorUno == value) return;
            _imgCuerpoJugadorUno = value;
            OnPropertyChanged();
        }
    }

    private string _imgCuerpoJugadorDos;
    public string ImgCuerpoJugadorDos
    {
        get => _imgCuerpoJugadorDos ;
        set
        {
            if (_imgCuerpoJugadorDos == value) return;
            _imgCuerpoJugadorDos = value;
            OnPropertyChanged();
        }
    }

    private string _imgCuerpoJugadorTres;


    public string ImgCuerpoJugadorTres
    {
        get => _imgCuerpoJugadorTres;
        set
        {
            if (_imgCuerpoJugadorTres == value) return;
            _imgCuerpoJugadorTres = value;
            OnPropertyChanged();
        }
    }

   
    public ComandoNavegarVolverAtras NavAtras {  get; set; }
    public BuscarJugadorViewModel()
    {
        _nombre = string.Empty;
        _imgCuerpoJugadorUno = string.Empty;
        _imgCuerpoJugadorDos = string.Empty;
        _imgCuerpoJugadorTres = string.Empty;
        NavAtras = new();
    }

    public async Task CargarDatos(BbddjuegoMarvelContext context)
    {
        Nombre = context.Usuarios.FirstOrDefault()!.NombreUsuario;

        GestionPersonajes gestionPersonajes = new(context);

        var personajesImagenes = await GestionPersonajes.CargarPersonajesJsonAsync();

        var listaNombresPersonajesUsuarioEquipo = await gestionPersonajes.CargarNombresEquipoPersonajesUsuario();
        if (listaNombresPersonajesUsuarioEquipo != null)
        {
            for (int i = 0; i < listaNombresPersonajesUsuarioEquipo.Count; i++)
            {
                switch (i)
                {
                    case 0:
                        ImgCuerpoJugadorUno = personajesImagenes[listaNombresPersonajesUsuarioEquipo[i]].ImgCuerpo;
                        continue;
                    case 1:
                        ImgCuerpoJugadorDos = personajesImagenes[listaNombresPersonajesUsuarioEquipo[i]].ImgCuerpo;
                        continue;
                    case 2:
                        ImgCuerpoJugadorTres = personajesImagenes[listaNombresPersonajesUsuarioEquipo[i]].ImgCuerpo;
                        break;
                }
            }
        }
    }
}

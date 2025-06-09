using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloJuego.Model;
using JuegoMarvel.ModuloJuego.View;
using JuegoMarvel.ModuloJuego.ViewModel.Comandos;
using JuegoMarvel.ModuloTienda.Model;
using JuegoMarvel.Services;
using MensajesServidor;
using System.Windows.Input;

namespace JuegoMarvel.ModuloJuego.ViewModel;

public class JuegoViewModel : BaseViewModel
{


    private string _imgHbUno;
    public string ImgHbUno
    {
        get => _imgHbUno;
        set
        {
            if (_imgHbUno != value)
            {
                _imgHbUno = value;
                OnPropertyChanged();
            }
        }
    }

    private string _imgHbDos;
    public string ImgHbDos
    {
        get => _imgHbDos;
        set
        {
            if (_imgHbDos != value)
            {
                _imgHbDos = value;
                OnPropertyChanged();
            }
        }
    }

    private string _imgHbTres;
    public string ImgHbTres
    {
        get => _imgHbTres;
        set
        {
            if (_imgHbTres != value)
            {
                _imgHbTres = value;
                OnPropertyChanged();
            }
        }
    }



    private bool _mostrarGridAcciones;
    public bool MostrarGridAcciones
    {
        get => _mostrarGridAcciones;
        set
        {
            if (_mostrarGridAcciones == value) return;
            _mostrarGridAcciones = value;
            OnPropertyChanged();
        }
    }



    private string _imgEstaSeleccionado;
    public string ImgEstaSeleccionado
    {
        get => _imgEstaSeleccionado;
        set
        {
            if (_imgEstaSeleccionado != value)
            {
                _imgEstaSeleccionado = value;
                OnPropertyChanged();
            }
        }
    }

    private string _imgEstaSeleccionadoContrario;
    public string ImgEstaSeleccionadoContrario
    {
        get => _imgEstaSeleccionadoContrario;
        set
        {
            if (_imgEstaSeleccionadoContrario != value)
            {
                _imgEstaSeleccionadoContrario = value;
                OnPropertyChanged();
            }
        }
    }


    private string _nombrePersonajeContrario;
    public string NombrePersonajeContrario
    {
        get => _nombrePersonajeContrario;
        set
        {
            if (_nombrePersonajeContrario != value)
            {
                _nombrePersonajeContrario = value;
                OnPropertyChanged();
            }
        }
    }

    private string _imgPersonajeContrario;
    public string ImagePersonajeContrario
    {
        get => _imgPersonajeContrario;
        set
        {
            if (_imgPersonajeContrario != value)
            {
                _imgPersonajeContrario = value;
                OnPropertyChanged();
            }
        }
    }

    private string _imgPersonajePropio;
    public string ImgPersonajePropio
    {
        get => _imgPersonajePropio;
        set
        {
            if (_imgPersonajePropio != value)
            {
                _imgPersonajePropio = value;
                OnPropertyChanged();
            }
        }
    }


    private double _estaminaPropia;
    public double EstaminaPropia
    {
        get => _estaminaPropia;
        set
        {
            if (_estaminaPropia != value)
            {
                _estaminaPropia = value;
                OnPropertyChanged();
            }
        }
    }

    private double _estaminaContraria;
    public double EstaminaContraria
    {
        get => _estaminaContraria;
        set
        {
            if (_estaminaContraria != value)
            {
                _estaminaContraria = value;
                OnPropertyChanged();
            }
        }
    }

    private string _reloj;
    public string Reloj
    {
        get => _reloj;
        set
        {
            if (_reloj != value)
            {
                _reloj = value;
                OnPropertyChanged();
            }
        }
    }

    private double _ejeXPersonajePropio;
    public double EjeXPersonajePropio
    {
        get => _ejeXPersonajePropio;
        set
        {
            if (_ejeXPersonajePropio != value)
            {
                _ejeXPersonajePropio = value;
                OnPropertyChanged();
            }
        }
    }

    private double _ejeXPersonajeContrario;
    public double EjeXPersonajeContrario
    {
        get => _ejeXPersonajeContrario;
        set
        {
            if (_ejeXPersonajeContrario != value)
            {
                _ejeXPersonajeContrario = value;
                OnPropertyChanged();
            }
        }
    }

    private int? _gastarEstaminaHbUno;
    public int? GastarEstaminaHbUno
    {
        get => _gastarEstaminaHbUno;
        set
        {
            if (_gastarEstaminaHbUno != value)
            {
                _gastarEstaminaHbUno = value;
                OnPropertyChanged();
            }
        }
    }

    private int? _gastarEstaminaHbDos;
    public int? GastarEstaminaHbDos
    {
        get => _gastarEstaminaHbDos;
        set
        {
            if (_gastarEstaminaHbDos != value)
            {
                _gastarEstaminaHbDos = value;
                OnPropertyChanged();
            }
        }
    }

    private int? _gastarEstaminaHbTres;
    public int? GastarEstaminaHbTres
    {
        get => _gastarEstaminaHbTres;
        set
        {
            if (_gastarEstaminaHbTres != value)
            {
                _gastarEstaminaHbTres = value;
                OnPropertyChanged();
            }
        }
    }

    private string _nombrePersonajePropio;
    public string NombrePersonajePropio
    {
        get => _nombrePersonajePropio;
        set
        {
            if (_nombrePersonajePropio != value)
            {
                _nombrePersonajePropio = value;
                OnPropertyChanged();
            }
        }
    }

    private double _vidaPropia;
    public double VidaPropia
    {
        get => _vidaPropia;
        set
        {
            if (_vidaPropia != value)
            {
                _vidaPropia = value;
                OnPropertyChanged();
            }
        }
    }

    private double _escudoPropio;
    public double EscudoPropio
    {
        get => _escudoPropio;
        set
        {
            if (_escudoPropio != value)
            {
                _escudoPropio = value;
                OnPropertyChanged();
            }
        }
    }

    private double _vidaContraria;
    public double VidaContraria
    {
        get => _vidaContraria;
        set
        {
            if (_vidaContraria != value)
            {
                _vidaContraria = value;
                OnPropertyChanged();
            }
        }
    }

    private double _escudoContraria;
    public double EscudoContraria
    {
        get => _escudoContraria;
        set
        {
            if (_escudoContraria != value)
            {
                _escudoContraria = value;
                OnPropertyChanged();
            }
        }
    }

    private string _relacionVidaYtotal;
    public string RelacionVidaYtotal
    {
        get => _relacionVidaYtotal;
        set
        {
            if (_relacionVidaYtotal != value)
            {
                _relacionVidaYtotal = value;
                OnPropertyChanged();
            }
        }
    }

    private string _relacionEscudoYtotal;
    public string RelacionEscudoYtotal
    {
        get => _relacionEscudoYtotal;
        set
        {
            if (_relacionEscudoYtotal != value)
            {
                _relacionEscudoYtotal = value;
                OnPropertyChanged();
            }
        }
    }


    private string? _habilidadSeleccionada = null;
    public string? HabilidadSeleccionada
    {
        get => _habilidadSeleccionada;
        set
        {
            if (_habilidadSeleccionada == value) return;
            _habilidadSeleccionada  = value;
            OnPropertyChanged();
        }
    }

    private int _segundos = 0;
    public readonly ClienteJuego Cliente;

    public ICommand ComandoHbUno { get; }
    public ICommand ComandoHbDos { get; }
    public ICommand ComandoHbTres { get; }
    public ComandoPegar ComandoPegar { get; }

    public ComandoPegar ComandoPegarContrario { get; }

    public JuegoViewModel(
        ClienteJuego cliente,
        string nombrePersonajePropio, 
        string nombrePersonajeContrario, 
        PersonajeImg personajeImgPropio, 
        PersonajeImg personajeImgContrario)
    {

        Cliente = cliente;
        

        _imgPersonajePropio = personajeImgPropio.ImgCuerpo;
        _imgPersonajeContrario = personajeImgContrario.ImgCuerpo;

        

        _nombrePersonajePropio = nombrePersonajePropio;
        _nombrePersonajeContrario = nombrePersonajeContrario;

        // Valores iniciales de ejemplo
        _estaminaPropia = 1.0;
        _estaminaContraria = 1.0;
        _vidaPropia = 1;
        _escudoPropio = 1;
        _vidaContraria = 1;
        _escudoContraria = 1;
        _gastarEstaminaHbUno = 10;
        _gastarEstaminaHbDos = 20;
        _gastarEstaminaHbTres = 30;
        _relacionVidaYtotal = "100/100";
        _relacionEscudoYtotal = "50/50";

        _imgHbUno = personajeImgPropio.Habilidades[0].Casillas.Original;
        _imgHbDos = personajeImgPropio.Habilidades[1].Casillas.Original;
        _imgHbTres = personajeImgPropio.Habilidades[2].Casillas.Original;


        ComandoHbUno = new RelayCommand(() =>
        {
            GastarEstaminaHbUno = -12;
            GastarEstaminaHbDos = null;
            GastarEstaminaHbTres = null;
            HabilidadSeleccionada = personajeImgPropio.Habilidades[0].Nombre;

        });
        ComandoHbDos = new RelayCommand(() => 
            {
                HabilidadSeleccionada = personajeImgPropio.Habilidades[1].Nombre;
                GastarEstaminaHbUno = null;
                GastarEstaminaHbDos = -12;
                GastarEstaminaHbTres = null;
            });
        ComandoHbTres = new RelayCommand(() =>
        {
            HabilidadSeleccionada = personajeImgPropio.Habilidades[2].Nombre;
            GastarEstaminaHbUno = null;
            GastarEstaminaHbDos = null;
            GastarEstaminaHbTres = -12;
        });



        ComandoPegar = new ComandoPegar(
            personajeImgPropio,
            this,
            0
        );

        ComandoPegarContrario = new ComandoPegar(
            personajeImgContrario,
            this,
            0,
            false
        );

        _reloj = "0";
        // Iniciar temporizador para el reloj
        IDispatcherTimer timer = EmpezarContar();
        timer.Start();

        ComandoPegarContrario.AnimacionTerminada += async (_, __) =>
        {
            await EmpezarJuego();
        };

        ComandoPegar.AnimacionTerminada += async (_, __) =>
        {
            await EmpezarJuego();
        };

        _ = EmpezarJuego();
    }


    private IDispatcherTimer EmpezarContar()
    {
        var dispatcher = Application.Current?.Dispatcher
                         ?? throw new InvalidOperationException("Dispatcher no disponible");

        var timer = dispatcher.CreateTimer();
        timer.Interval = TimeSpan.FromSeconds(1);
        timer.IsRepeating = true;

        // Cada vez que "tic–tac", actualizamos la propiedad Reloj
        timer.Tick += (sender, args) =>
        {
            // Si quieres mostrar un contador de segundos:
            _segundos++;
            Reloj = _segundos.ToString("D2");
        };

        return timer;
    }

    public async Task EmpezarJuego()
    {
        if (VidaPropia == 0 && EscudoPropio == 0)
        {
            await Cliente.Enviar(new MensajesModuloJuego { TipoMensaje = EnumMensajeJuego.MandarEstado, Valor = "Perdi" });
            var window = Application.Current.Windows[0];
            var nav = window.Page.Navigation;
            await nav.PushModalAsync(new ResultadoJugador("Perdi"));

            return;
        }

        await Cliente.Enviar(new MensajesModuloJuego { TipoMensaje = EnumMensajeJuego.MandarEstado, Valor = "Listo" });
        var mensaje = await Cliente.Recibir();
        if (mensaje.Valor == "Ganaste")
        {
            var window = Application.Current.Windows[0];
            var nav = window.Page.Navigation;
            await nav.PushModalAsync(new ResultadoJugador("Ganaste"));
            // Cambiar pantalla a la de derrota.
            return;
        }


        MostrarGridAcciones = mensaje.Valor == "Juegas";

        ImgEstaSeleccionado = mensaje.Valor == "Juegas"
            ? "personaje_seleccion.png"
            : string.Empty;

        ImgEstaSeleccionadoContrario = mensaje.Valor == "Esperas"
            ? "personaje_seleccion.png"
            : string.Empty;

        if (mensaje.Valor == "Esperas")
        {
            _ = EmpezarEscucha();
        }
    }


    public async Task EmpezarEscucha()
    {
        var mensaje = await Cliente.Recibir();
        if (mensaje.Valor == "Ganaste")
        {
            var window = Application.Current.Windows[0];
            var nav = window.Page.Navigation;
            await nav.PushModalAsync(new ResultadoJugador("Ganaste"));
            // Cambiar pantalla a la de derrota.
            return;
        }

        ComandoPegarContrario.Execute(mensaje.Valor);
    }
}

 using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloJuego.ViewModel.Comandos;
using JuegoMarvel.ModuloTienda.Model;
using JuegoMarvel.Services;
using System.Windows.Input;

namespace JuegoMarvel.ModuloJuego.ViewModel
{
    public class JuegoViewModel : BaseViewModel
    {
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

        public ICommand ComandoHbUno { get; }
        public ICommand ComandoHbDos { get; }
        public ICommand ComandoHbTres { get; }
        public ComandoPegar ComandoPegar { get; }

        public JuegoViewModel(PersonajeImg personajeImgPropio, PersonajeImg personajeImgContrario)
        {
            _imgPersonajePropio = personajeImgPropio.ImgCuerpo;

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
            _nombrePersonajePropio = "Héroe";
            _relacionVidaYtotal = "100/100";
            _relacionEscudoYtotal = "50/50";

            ComandoHbUno = new RelayCommand(() =>
            {
                GastarEstaminaHbUno = -12;
                GastarEstaminaHbDos = null;
                GastarEstaminaHbTres = null;
                HabilidadSeleccionada = "Baritas Locas";

            });
            ComandoHbDos = new RelayCommand(() => 
                {
                    HabilidadSeleccionada = "Patadon Historico";
                    GastarEstaminaHbUno = null;
                    GastarEstaminaHbDos = -12;
                    GastarEstaminaHbTres = null;
                });
            ComandoHbTres = new RelayCommand(() =>
            {
                HabilidadSeleccionada = "MMA masivo";
                GastarEstaminaHbUno = null;
                GastarEstaminaHbDos = null;
                GastarEstaminaHbTres = -12;
            });



            ComandoPegar = new ComandoPegar(
                personajeImgPropio,
                this,
                0
            );

            _reloj = "0";
            // Iniciar temporizador para el reloj
            IDispatcherTimer timer = EmpezarContar();
            timer.Start();
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

    }
}

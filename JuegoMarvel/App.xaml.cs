using JuegoMarvel.ModuloAuxiliares.ModuloCargaInicial.View;
using JuegoMarvel.ModuloJuego.View;
using JuegoMarvel.ModuloJuego.ViewModel;
using JuegoMarvel.Views;

namespace JuegoMarvel
{
    /// <summary>
    /// Clase principal de la aplicación MAUI. Se encarga de inicializar la interfaz y definir la ventana principal.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Propiedad privada que almacena la instancia de la página de inicio (login) inyectada por el contenedor de dependencias.
        /// </summary>
        private readonly CargaInicial _cargaPage;

        public IServiceProvider Services { get; }

        /// <summary>
        /// Constructor que recibe la página de inicio (login) como dependencia.
        /// </summary>
        /// <param name="cargaPage">Instancia de la página de login proporcionada por inyección de dependencias.</param>
        public App(IServiceProvider serviceProvider, CargaInicial cargaPage)
        {
            InitializeComponent();
            _cargaPage = cargaPage;
            Services = serviceProvider;
        }



        /// <summary>
        /// Crea e inicializa la ventana principal de la aplicación.
        /// Esta implementación personaliza la ventana usando la página de login como contenido inicial.
        /// </summary>
        /// <param name="activationState">Estado de activación, proporcionado por el sistema al arrancar la app.</param>
        /// <returns>Una instancia de <see cref="Window"/> con la página de inicio como contenido.</returns>
        protected override Window CreateWindow(IActivationState? activationState)
        {
            //new JuegoViewModel(null, null)
            var window = new Window(new Juego())
            {
                Title = "Juego Marvel"
            };
            return window;
        }
    }
}

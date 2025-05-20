using JuegoMarvel.ModuloLogin.View;
using JuegoMarvel.Views;
using Microsoft.Maui.Controls;
using Microsoft.Maui.LifecycleEvents;

namespace JuegoMarvel
{
    public partial class App : Application
    {
        private readonly Login _loginPage;

        // Guardamos la página inyectada en un campo
        public App(Login loginPage)
        {
            InitializeComponent();
            _loginPage = loginPage;
        }

        // Sobrescribimos CreateWindow para inicializar la aplicación
        protected override Window CreateWindow(IActivationState? activationState)
        {
            // Creamos la ventana principal usando la página inyectada
            var window = new Window(_loginPage)
            {
                Title = "Juego Marvel"
            };
            return window;
        }
    }
}

using JuegoMarvel.ModuloLogin.View;
using JuegoMarvel.Views;
using Microsoft.Maui.Controls;

namespace JuegoMarvel
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
        }

        protected override Window CreateWindow(IActivationState? activationState)
        {
            return new Window(new Login());
        }
    }
}
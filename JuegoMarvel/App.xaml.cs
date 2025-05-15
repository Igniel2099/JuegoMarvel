using JuegoMarvel.ModuloLogin.View;
using JuegoMarvel.ModuloLogin.ViewModel;
using JuegoMarvel.Views;
using Microsoft.Maui.Controls;

namespace JuegoMarvel
{
    public partial class App : Application
    {
        public App(Login loginPage)
        {
            InitializeComponent();

            MainPage = loginPage;
        }

        
    }
}
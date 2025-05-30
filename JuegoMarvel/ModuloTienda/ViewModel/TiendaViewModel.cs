using JuegoMarvel.ModuloTienda.Model;
using JuegoMarvelData.Data;

namespace JuegoMarvel.ModuloTienda.ViewModel
{
    public class TiendaViewModel : BaseViewModel
    {
        private readonly BbddjuegoMarvelContext _context;
        public GestionPersonajes GestionPersonajes { get; }

        public TiendaViewModel(BbddjuegoMarvelContext context)
        {
            _context = context;
            GestionPersonajes = new(context);
        }
    }
}
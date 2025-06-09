using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloJuego.Model;
using JuegoMarvel.ModuloTienda.Model;
using MensajesServidor;

namespace JuegoMarvel.ModuloJuego.ViewModel.Comandos;

public class ComandoInicializarConexion(BuscarJugadorViewModel vm, ClienteJuego cliente) : BaseCommand
{
    private readonly BuscarJugadorViewModel _vm = vm;
    private readonly ClienteJuego _cliente = cliente;

    public async override void Execute(object? parameter)
    {
        _cliente.Conectar();
        await _cliente.Enviar(
            new MensajesModuloJuego 
            { 
                TipoMensaje = EnumMensajeJuego.MandarPersonaje, 
                Valor = _vm.NombrePersonajeDos 
            }
        );

        var mensajeOponente = await _cliente.Recibir() 
            ?? throw new Exception("No he recibido ningun mensaje.");
        if (mensajeOponente.TipoMensaje != EnumMensajeJuego.MandarPersonaje)
            throw new Exception("No me esperaba esta respuesta");
        PersonajeImg personajeImg = _vm.PersonajesImgs[ mensajeOponente.Valor];

    }
}

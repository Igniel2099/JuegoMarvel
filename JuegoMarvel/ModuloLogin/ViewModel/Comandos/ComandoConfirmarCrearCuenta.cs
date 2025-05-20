using CommunityToolkit.Maui.Views;
using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.View;
using MensajesServidor;

namespace JuegoMarvel.ModuloLogin.ViewModel.Comandos;
public class ComandoConfirmarCrearCuenta : BaseCommand
{
    private readonly AppSettings _settings;
    private readonly ComprobadorDominio _comprobador;
    private readonly CrearCuentaViewModel _vm;
    private readonly ValidadorNombreUsuario _validadorUsuario;
    private readonly ValidadorCorreoElectronico _validadorCorreo;
    private readonly ValidadorContrasena _validadorContrasena;

    public ComandoConfirmarCrearCuenta(AppSettings settings, ComprobadorDominio comprobador, CrearCuentaViewModel vm)
    {
        _settings = settings;
        _comprobador = comprobador;
        _vm = vm;
        _validadorUsuario = new ValidadorNombreUsuario(_settings);
        _validadorCorreo = new ValidadorCorreoElectronico(EnumOrigen.CrearCuenta, _settings, _comprobador);
        _validadorContrasena = new ValidadorContrasena();
    }

    public override async void Execute(object? parameter)
    {
        var (usuarioOk, mensajeUsu) = await _validadorUsuario.ValidarAsync(_vm.NombreUsuario!);
        _vm.EstadoImgNombreUsuario = usuarioOk;

        var (correoOk, mensajeCorr) = await _validadorCorreo.ValidarAsync(_vm.CorreoElectronico!);
        _vm.EstadoImgCorreoElectronico = correoOk;

        var (pwOk, mensajePw) = _validadorContrasena.Validar(_vm.Contrasena, _vm.ConfirmarContrasena);
        _vm.EstadoImgContrasena = pwOk;
        _vm.EstadoImgConfirmarContrasena = pwOk;

        if (parameter is CrearCuenta crearCuenta)
        {
            // Mostrar popup con mensajes: mensajeUsu, mensajeCorr, mensajePw
            var popup = new PopupErrores(new PopupErroresViewModel(
                    usuarioOk && correoOk && pwOk ? "FELICIDADES!\nSE CREO LA CUENTA DE FORMA EXITOSA"  : "ERROR AL CREAR CUENTA",
                    usuarioOk ? null : mensajeUsu,
                    correoOk ? null : mensajeCorr,
                    pwOk ? null : mensajePw
                )
            );

            await crearCuenta.ShowPopupAsync(popup);
        }
    }
}

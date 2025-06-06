using CommunityToolkit.Maui.Views;
using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.View;
using MensajesServidor;

namespace JuegoMarvel.ModuloLogin.ViewModel.Comandos;

/// <summary>
/// Comando para confirmar la creación de una nueva cuenta de usuario.
/// Realiza la validación del nombre de usuario, correo electrónico y contraseña utilizando los validadores correspondientes.
/// Muestra un popup con los mensajes de validación y el resultado del proceso.
/// </summary>
/// <remarks>
/// Este comando utiliza <see cref="ValidadorNombreUsuario"/>, <see cref="ValidadorCorreoElectronico"/> y <see cref="ValidadorContrasena"/>
/// para validar los datos introducidos en el <see cref="CrearCuentaViewModel"/>. El resultado se muestra mediante un <see cref="PopupErrores"/>.
/// </remarks>
public class ComandoConfirmarCrearCuenta : BaseCommand
{
    /// <summary>
    /// Propiedad Privada de la configuración de la app
    /// </summary>
    private readonly AppSettings _settings;

    /// <summary>
    /// Propiedad privada del Comprobador de dominios
    /// </summary>
    private readonly ComprobadorDominio _comprobador;

    /// <summary>
    /// Propiedad privada del View Model
    /// </summary>
    private readonly CrearCuentaViewModel _vm;

    /// <summary>
    /// Propiedad privada del Validador del nombre del Usuario
    /// </summary>
    private readonly ValidadorNombreUsuario _validadorUsuario;

    /// <summary>
    /// Propiedad privada del validador del correo electornico.
    /// </summary>
    private readonly ValidadorCorreoElectronico _validadorCorreo;

    /// <summary>
    /// Propiedad privada del Validador de la contraseña
    /// </summary>
    private readonly ValidadorContrasena _validadorContrasena;

    /// <summary>
    /// Inicializa una nueva instancia de <see cref="ComandoConfirmarCrearCuenta"/> con las dependencias necesarias.
    /// Inicializa los campos y los validadores con sus respectivos parametros.
    /// </summary>
    /// <param name="settings">Configuración de la aplicación.</param>
    /// <param name="comprobador">Comprobador de dominio para validaciones de correo electrónico.</param>
    /// <param name="vm">ViewModel de la pantalla de creación de cuenta.</param>
    public ComandoConfirmarCrearCuenta(AppSettings settings, ComprobadorDominio comprobador, CrearCuentaViewModel vm)
    {
        _settings = settings;
        _comprobador = comprobador;
        _vm = vm;
        _validadorUsuario = new ValidadorNombreUsuario(_settings);
        _validadorCorreo = new ValidadorCorreoElectronico(EnumOrigen.CrearCuenta, _settings, _comprobador);
        _validadorContrasena = new ValidadorContrasena();
    }

    /// <summary>
    /// Ejecuta la validación de los datos y muestra un popup con el resultado.
    /// </summary>
    /// <param name="parameter">Debe ser una instancia de <see cref="CrearCuenta"/> (la vista actual).</param>
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
                    usuarioOk && correoOk && pwOk ? "FELICIDADES!\nSE CREO LA CUENTA DE FORMA EXITOSA" : "ERROR AL CREAR CUENTA",
                    usuarioOk ? null : mensajeUsu,
                    correoOk ? null : mensajeCorr,
                    pwOk ? null : mensajePw
                )
            );

            await crearCuenta.ShowPopupAsync(popup);
        }
    }
}
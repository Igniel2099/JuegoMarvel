using CommunityToolkit.Maui.Views;
using JuegoMarvel.ClasesBase;
using JuegoMarvel.ModuloAuxiliares.ModuloCarga;
using JuegoMarvel.ModuloAuxiliares.ModuloCarga.ViewModels;
using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.View;
using JuegoMarvel.Views;
using JuegoMarvelData.Data;
using MensajesServidor;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace JuegoMarvel.ModuloLogin.ViewModel.Comandos;

/// <summary>
/// Comando para gestionar el proceso de inicio de sesión del usuario.
/// Valida las credenciales, consulta al servidor y gestiona la navegación y los mensajes de error o éxito.
/// </summary>
/// <remarks>
/// Utiliza <see cref="AppSettings"/> para la configuración de la conexión al servidor y <see cref="BbddjuegoMarvelContext"/>
/// para el acceso a la base de datos local. Muestra popups de error o éxito y navega a la pantalla de carga si el login es correcto.
/// </remarks>
public class ComandoLogearse(AppSettings configuracion, BbddjuegoMarvelContext context) : BaseCommand
{
    /// <summary>
    /// Propiedad privada del configurador de la aplicación
    /// </summary>
    private readonly AppSettings _configuracion = configuracion;

    /// <summary>
    /// Propiedad privada del DbContext
    /// </summary>
    private readonly BbddjuegoMarvelContext _context = context;

    /// <summary>
    /// Nombre de usuario a autenticar.
    /// </summary>
    public string? Nombre { get; set; }

    /// <summary>
    /// Contraseña a autenticar.
    /// </summary>
    public string? Contrasena { get; set; }

    /// <summary>
    /// Ejecuta el proceso de inicio de sesión.
    /// Valida los campos, consulta al servidor y navega a la pantalla de carga si las credenciales son correctas.
    /// Muestra popups de error en caso contrario.
    /// </summary>
    /// <param name="parameter">Debe ser una instancia de <see cref="Login"/> (la vista actual).</param>
    public override async void Execute(object? parameter)
    {
        if (parameter is Login login)
        {
            if (string.IsNullOrEmpty(Nombre) || string.IsNullOrEmpty(Contrasena))
            {
                var popup = new PopupErrores(
                    new PopupErroresViewModel(
                        "ERROR AL INICIAR SESIÓN",
                        "El usuario o la contraseña son incorrectos"
                    )
                );
                await login.ShowPopupAsync(popup);
                return;
            }

            try
            {
                using var cliente = new TcpClient();

                await cliente.ConnectAsync(
                    _configuracion.IpServidor,
                    _configuracion.PuertoServidor
                );

                using var stream = cliente.GetStream();

                var mensaje = new MensajesModuloLogin(
                    EnumOrigen.Login,
                    EnumTipoRespuesta.Comprobar,
                    [new Propiedad(EnumTipoValor.NombreUsuario, Nombre.Trim()),
                    new Propiedad(EnumTipoValor.Contraseña, Contrasena.Trim())],
                    null);

                var ajustesJson = new JsonSerializerSettings();
                ajustesJson.Converters.Add(new StringEnumConverter());

                string contenido = JsonConvert.SerializeObject(mensaje, Formatting.Indented, ajustesJson);
                byte[] datosEnvio = Encoding.UTF8.GetBytes(contenido);
                await stream.WriteAsync(datosEnvio, 0, datosEnvio.Length);

                byte[] buffer = new byte[1024];
                int leidos = await stream.ReadAsync(buffer);
                string respuestaJson = Encoding.UTF8.GetString(buffer, 0, leidos);

                var respuestaServidor = JsonConvert.DeserializeObject<MensajesModuloLogin?>(respuestaJson, ajustesJson);
                if (respuestaServidor?.Respuesta is not null)
                {
                    bool existe = respuestaServidor.Respuesta == EnumRespuesta.Existente;
                    if (existe)
                    {
                        var popup = new PopupErrores(
                            new PopupErroresViewModel(
                                "HAS INICIADO SESION"
                            )

                        );
                        await login.ShowPopupAsync(popup);

                        // Push sin animación nativa
                        var window = Application.Current.Windows[0];
                        // para apps de una sola ventana
                        var nav = window.Page.Navigation;

                        // Para hacer el PushModal sin animación nativa:
                        await nav.PushModalAsync(new PantallaDeCarga(new PantallaCargaViewModel(
                            _configuracion,
                            _context,
                            Nombre.Trim()
                            )));
                    }
                    else
                    {
                        var popup = new PopupErrores(
                            new PopupErroresViewModel(
                                "ERROR AL INICAR SESIÓN",
                                "El usuario o la contraseña son incorrectos"
                            )
                        );
                        await login.ShowPopupAsync(popup);
                    }
                }
            }
            catch (SocketException ex)
            {
                Debug.WriteLine("==================================" + $"Error: {ex.Message}" + "==================================");
                var popup = new PopupErrores(
                    new PopupErroresViewModel(
                        "ERROR DE CONEXIÓN",
                        "No se pudo conectar al servidor. Verifica tu conexión a Internet."
                    )
                );
                await login.ShowPopupAsync(popup);
            }
            catch (Exception ex)
            {
                Debug.WriteLine("==================================" + $"Error: {ex.Message}" + "==================================");
            }
        }
    }
}
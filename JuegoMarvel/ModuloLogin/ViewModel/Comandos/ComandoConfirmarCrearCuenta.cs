using CommunityToolkit.Maui.Views;
using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.View;
using MensajesServidor;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;

namespace JuegoMarvel.ModuloLogin.ViewModel.Comandos;

public class ComandoConfirmarCrearCuenta(AppSettings settings, CrearCuentaViewModel crearCuentaViewModel) : BaseCommand
{
    private readonly AppSettings _settings = settings;
    private readonly CrearCuentaViewModel _crearCuentaViewModel = crearCuentaViewModel;

    public override async void Execute(object? parameter)
    {
        _crearCuentaViewModel.EstadoImgNombreUsuario = null;
        _crearCuentaViewModel.EstadoImgCorreoElectronico = null;
        _crearCuentaViewModel.EstadoImgContrasena = null;
        _crearCuentaViewModel.EstadoImgConfirmarContrasena = null;

        if (parameter is CrearCuenta crearCuenta)
        {

            var (existeNu, mensajeNu) = await ComprobarNombreUsuario(_crearCuentaViewModel.NombreUsuario!);

            _crearCuentaViewModel.EstadoImgNombreUsuario = existeNu;
            string? txtErrorNomUs = mensajeNu;

            var (existeCe, mensajeCe) = await ComprobarCorreoElectronico(_crearCuentaViewModel.CorreoElectronico!);
            _crearCuentaViewModel.EstadoImgCorreoElectronico = existeCe;
            string? txtErrorCorreoElec = mensajeCe;

            var (comprobacionContrasena, mensajeContrasena) = ComprobarContrasena(_crearCuentaViewModel.Contrasena, _crearCuentaViewModel.ConfirmarContrasena);
            _crearCuentaViewModel.EstadoImgContrasena = comprobacionContrasena;
            _crearCuentaViewModel.EstadoImgConfirmarContrasena = comprobacionContrasena;
            string? txtErrorContrasena = mensajeContrasena;

            var popupECC = new PopupErroresCrearCuenta
            {
                BindingContext = new PopupErroresCrearCuentaViewModel(txtErrorNomUs,txtErrorCorreoElec,txtErrorContrasena)
            };
            await crearCuenta.ShowPopupAsync(popupECC);
        }
        else
            throw new Exception("El parametro de confirmar de Crear Cuenta no es el que se esperaba");

    }

    public async Task<(bool comprobacion, string? mensaje)> ComprobarNombreUsuario(string nombreUsuario)
    {
        // Comprobar el formato del nombre de usuario
        if (string.IsNullOrWhiteSpace(nombreUsuario))
            return (false, "No puedes dejar vacio el Campo de Nombre Usuario. ");
       return await ExistenciaNombreUsuario(nombreUsuario);
    }


    public async Task<(bool existe, string? mensaje)> ExistenciaNombreUsuario(string nombreUsuario)
    {
        try
        {
            using TcpClient cliente = new();

            await cliente.ConnectAsync(
                _settings.IpServidor,
                _settings.PuertoServidor
            ); // IP y puerto del servidor

            using NetworkStream stream = cliente.GetStream();

            MensajesModuloLogin mensaje = new(
                EnumOrigen.CrearCuenta,
                EnumTipoRespuesta.Comprobar,
                [new Propiedad(EnumTipoValor.NombreUsuario, nombreUsuario)],
                null
            );

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumConverter());

            string mensajeJson = JsonConvert.SerializeObject(mensaje, Formatting.Indented, settings);

            byte[] datos = Encoding.UTF8.GetBytes(mensajeJson);
            await stream.WriteAsync(datos, 0, datos.Length);

            // Leer respuesta
            byte[] buffer = new byte[1024];
            int bytesLeidos = await stream.ReadAsync(buffer);
            string respuesta = Encoding.UTF8.GetString(buffer, 0, bytesLeidos);

            MensajesModuloLogin? mensajeServidor = JsonConvert.DeserializeObject<MensajesModuloLogin?>(respuesta, settings);
            if ( mensajeServidor != null)
            {
                if (mensajeServidor.Respuesta == null)
                    throw new Exception("Error con el json Recibido algo paso en el proceso");

                bool existe = mensajeServidor.Respuesta == EnumRespuesta.Existente;
                return (!existe, existe ? "El nombre de usuario ya existe." : null );
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("==================================" + $"Error: {ex.Message}" + "==================================");
        }
        return (false, "Error con el Campo de Nombre Usuario.");
    }

    public async Task<(bool existe, string? mensaje)> ComprobarCorreoElectronico(string correoElectronico)
    {
        // Comprobar el formato del correo electrónico
        if (string.IsNullOrWhiteSpace(correoElectronico))
            return (false, "No puedes dejar vacio el Campo de Correo Electronico. ");
        else if (!correoElectronico.Contains('@') || !correoElectronico.Contains('.')) // Hacer una comprobacion mas fuerte de la validacion del correo Electronico 
            return (false, "El correo no es válido. ");

        return await ExisteCorreoElectronico(correoElectronico);
    }

    public async Task<(bool existe, string? mensaje)> ExisteCorreoElectronico(string correoElectronico)
    {
        try
        {
            using TcpClient cliente = new();
            await cliente.ConnectAsync(
                _settings.IpServidor,
                _settings.PuertoServidor
            ); // IP y puerto del servidor // IP y puerto del servidor

            using NetworkStream stream = cliente.GetStream();

            MensajesModuloLogin mensaje = new(
                EnumOrigen.CrearCuenta,
                EnumTipoRespuesta.Comprobar,
                [new Propiedad(EnumTipoValor.CorreoElectronico, correoElectronico)],
                null
            );

            var settings = new JsonSerializerSettings();
            settings.Converters.Add(new StringEnumConverter());

            string mensajeJson = JsonConvert.SerializeObject(mensaje, Formatting.Indented, settings);

            byte[] datos = Encoding.UTF8.GetBytes(mensajeJson);
            await stream.WriteAsync(datos, 0, datos.Length);

            // Leer respuesta
            byte[] buffer = new byte[1024];
            int bytesLeidos = await stream.ReadAsync(buffer);
            string respuesta = Encoding.UTF8.GetString(buffer, 0, bytesLeidos);

            MensajesModuloLogin? mensajeServidor = JsonConvert.DeserializeObject<MensajesModuloLogin?>(respuesta);
            if (mensajeServidor != null)
            {
                if (mensajeServidor.Respuesta == null)
                    throw new Exception("Error con el json Recibido algo paso en el proceso");

                bool existe = mensajeServidor.Respuesta == EnumRespuesta.Existente;
                return (!existe, existe ? "El correo electronico ya existe." : null);
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("==================================" + $"Error: {ex.Message}" + "==================================");
        }
        return (false, "Error con el Campo de Correo Electronico.");
    }


    public (bool valida, string? error) ComprobarContrasena(string? contra, string? confirma)
    {
        var (nulosOk, errorNulos) = ComprobarNulosContrasenas(contra, confirma);
        if (!nulosOk)
            return (false, errorNulos);

        var (formatoOk, errorForm) = ComprobarFormatoContrasena(contra!);
        if (!formatoOk)
            return (false, errorForm);

        return contra == confirma
            ? (true, null) 
            : (false, "Las contraseñas no coinciden.");
    }

    public (bool esValido, string? error) ComprobarNulosContrasenas(string? contrasena, string? confirmarContrasena)
    {
        if (string.IsNullOrWhiteSpace(contrasena) || string.IsNullOrWhiteSpace(confirmarContrasena))
            return (false, "Las contraseñas no pueden tener campos vacíos.");
        return (true, null);
    }


    public (bool comprobacion, string? mensaje) ComprobarIgualdadContrasenas(string? contrasena, string? confirmarContrasena)
    {
        if (contrasena == confirmarContrasena)
            return (true, null);
        return (false, "Las contraseñas no coinciden.");
    }

    public (bool comprobacion, string? mensaje) ComprobarFormatoContrasena(string contraseña)
    {
        if (contraseña.Length > 12) // Tambien si es alfanumerico, si contiene un caracter especial
            return (true, null);
        return (false, "La contraseña no Tiene un Formato Valido.");
    }



}

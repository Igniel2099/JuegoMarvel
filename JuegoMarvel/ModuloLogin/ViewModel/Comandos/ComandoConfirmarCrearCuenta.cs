using CommunityToolkit.Maui.Views;
using JuegoMarvel.ModuloLogin.Model;
using JuegoMarvel.ModuloLogin.View;
using MensajesServidor;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System.Diagnostics;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;

namespace JuegoMarvel.ModuloLogin.ViewModel.Comandos;

public class ComandoConfirmarCrearCuenta(AppSettings settings, ComprobadorDominio comprobador ,CrearCuentaViewModel crearCuentaViewModel) : BaseCommand
{
    private readonly AppSettings _settings = settings;
    private readonly ComprobadorDominio _comprobador = comprobador; 
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
        catch (SocketException ex)
        {
            Debug.WriteLine("==================================" + $"Error: {ex.Message}" + "==================================");
        }
        return (false, "Error con el Campo de Nombre Usuario.");
    }

    public async Task<(bool existe, string? mensaje)> ComprobarCorreoElectronico(string correoElectronico)
    {
        if (string.IsNullOrWhiteSpace(correoElectronico))
            return (false, "El campo de correo no puede estar vacío.");

        // 1. Sintaxis
        if (!FormatoCorreoElectronico(correoElectronico))
            return (false, "Formato de correo inválido.");

        // 2. ¿Ya existe en el servidor?
        var (noExiste, msgExiste) = await ExisteCorreoElectronico(correoElectronico);
        if (!noExiste)  // ExisteCorreoElectronico devuelve (true, null) si NO existe
            return (false, msgExiste);

        // 3. Dominio DNS/MX
        bool dominioValido = await _comprobador
            .ComprobarDominioCorreoElectronicoAsync(correoElectronico);
        if (!dominioValido)
            return (false, "El dominio del correo electrónico no es válido.");

        // 4. Todo OK
        return (true, null);
    }

    public bool FormatoCorreoElectronico(string correoElectronico)
    {
        const string patron = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
        if (!Regex.IsMatch(correoElectronico.Trim(), patron, RegexOptions.IgnoreCase))
            return false;
        return true; 
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
        if (contraseña.Length < 12)
            return (false, "La contraseña debe tener al menos 12 caracteres.");

        // 2. Al menos una letra
        if (!Regex.IsMatch(contraseña, @"[A-Za-z]"))
            return (false, "La contraseña debe contener al menos una letra.");

        // 3. Al menos un dígito
        if (!Regex.IsMatch(contraseña, @"\d"))
            return (false, "La contraseña debe contener al menos un número.");

        // 4. (Opcional) Al menos un carácter especial
        if (!Regex.IsMatch(contraseña, @"[!@#$%^&*(),.?""':{}|<>_\-\[\]\\\/]"))
            return (false, "La contraseña debe contener al menos un carácter especial.");

        return (true, null);
    }



}

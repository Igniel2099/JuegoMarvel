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

public class ComandoConfirmarCrearCuenta : BaseCommand
{
    private readonly Dictionary<string, string> _propiedadesCrearCuenta;
    private readonly AppSettings _settings;
    private readonly CrearCuentaViewModel _crearCuentaViewModel;

    public ComandoConfirmarCrearCuenta(AppSettings settings, CrearCuentaViewModel crearCuentaViewModel )
    {
        _settings = settings;

        _propiedadesCrearCuenta = new()
        {
            { nameof(crearCuentaViewModel.NombreUsuario), crearCuentaViewModel.NombreUsuario! },
            { nameof(crearCuentaViewModel.CorreoElectronico), crearCuentaViewModel.CorreoElectronico! },
            { nameof(crearCuentaViewModel.Contrasena), crearCuentaViewModel.Contrasena! },
            { nameof(crearCuentaViewModel.ConfirmarContrasena), crearCuentaViewModel.ConfirmarContrasena! }
        };

        crearCuentaViewModel.PropertyChanged += OnVmPropertyChanged;
        _crearCuentaViewModel = crearCuentaViewModel;
    }

    private void OnVmPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (sender is not CrearCuentaViewModel vm) return;

        // e.PropertyName te dice qué propiedad cambió:
        switch (e.PropertyName)
        {
            case nameof(vm.NombreUsuario):
                HandleCambio("NombreUsuario", vm.NombreUsuario);
                break;

            case nameof(vm.CorreoElectronico):
                HandleCambio("CorreoElectronico", vm.CorreoElectronico);
                break;

            case nameof(vm.Contrasena):
                HandleCambio("Contrasena", vm.Contrasena);
                break;

            case nameof(vm.ConfirmarContrasena):
                HandleCambio("ConfirmarContrasena", vm.ConfirmarContrasena);
                break;
        }
    }

    private void HandleCambio(string propiedad, string? valor)
    {
        if (valor is null) return;
        _propiedadesCrearCuenta[propiedad] = valor;
    }

    public override async void Execute(object? parameter)
    {
        _crearCuentaViewModel.EstadoImgNombreUsuario = null;
        _crearCuentaViewModel.EstadoImgCorreoElectronico = null;
        _crearCuentaViewModel.EstadoImgContrasena = null;
        _crearCuentaViewModel.EstadoImgConfirmarContrasena = null;

        string? txtErrorNomUs = null;
        string? txtErrorCorreoElec = null;
        string? txtErrorContrasena = null;

        bool existeUsuario = await ComprobarNombreUsuario(_crearCuentaViewModel.NombreUsuario!);
        _crearCuentaViewModel.EstadoImgNombreUsuario = existeUsuario;

        bool correoValidoFormato = _crearCuentaViewModel.CorreoElectronico!.Contains('@');
        bool existeCorreo = await ComprobarCorreoElectronico(_crearCuentaViewModel.CorreoElectronico);
        _crearCuentaViewModel.EstadoImgCorreoElectronico = correoValidoFormato && !existeCorreo;

        bool contrasenaValida = ComprobarContrasena(_crearCuentaViewModel.Contrasena!);
        _crearCuentaViewModel.EstadoImgContrasena = contrasenaValida;

        bool coincide = _crearCuentaViewModel.Contrasena == _crearCuentaViewModel.ConfirmarContrasena;
        _crearCuentaViewModel.EstadoImgConfirmarContrasena = coincide;

        if (parameter is CrearCuenta crearCuenta)
        {
            if (_crearCuentaViewModel.EstadoImgNombreUsuario == false)
                txtErrorNomUs = "El nombre de usuario ya existe.\n";
            if (_crearCuentaViewModel.EstadoImgCorreoElectronico == false)
                txtErrorCorreoElec = "El correo no es válido o ya existe.\n";
            if (_crearCuentaViewModel.EstadoImgContrasena == false)
                txtErrorContrasena = "La contraseña no cumple los requisitos.\n";
            if (_crearCuentaViewModel.EstadoImgConfirmarContrasena == false)
                txtErrorContrasena = "Las contraseñas no coinciden.\n";


            var popupECC = new PopupErroresCrearCuenta
            {
                BindingContext = new PopupErroresCrearCuentaViewModel(txtErrorNomUs,txtErrorCorreoElec,txtErrorContrasena)
            };
            await crearCuenta.ShowPopupAsync(popupECC);
        }
        else
            throw new Exception("El parametro de confirmar de Crear Cuenta no es el que se esperaba");

    }

    public async Task<bool> ComprobarNombreUsuario(string nombreUsuario)
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

                return mensajeServidor.Respuesta == EnumRespuesta.NoExistente;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("==================================" + $"Error: {ex.Message}" + "==================================");
        }
        return false;
    }

    public async Task<bool> ComprobarCorreoElectronico(string correoElectronico)
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

                return mensaje.Respuesta == EnumRespuesta.Existente;
            }
        }
        catch (Exception ex)
        {
            Debug.WriteLine("==================================" + $"Error: {ex.Message}" + "==================================");
        }
        return false;
    }

    public bool ComprobarContrasena(string contraseña)
    {
        if (contraseña.Length > 12) // Tambien si es alfanumerico, si contiene un caracter especial
            return true;
        return false;

    }



}

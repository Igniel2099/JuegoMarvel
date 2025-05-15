using CommunityToolkit.Maui.Views;
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

    public ComandoConfirmarCrearCuenta(CrearCuentaViewModel crearCuentaViewModel)
    {
        _propiedadesCrearCuenta = new()
        {
            { nameof(crearCuentaViewModel.NombreUsuario), crearCuentaViewModel.NombreUsuario! },
            { nameof(crearCuentaViewModel.CorreoElectronico), crearCuentaViewModel.CorreoElectronico! },
            { nameof(crearCuentaViewModel.Contrasena), crearCuentaViewModel.Contrasena! },
            { nameof(crearCuentaViewModel.ConfirmarContrasena), crearCuentaViewModel.ConfirmarContrasena! }
        };

        crearCuentaViewModel.PropertyChanged += OnVmPropertyChanged;
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

        StringBuilder textoFinal = new();


        if ( await ComprobarNombreUsuario(_propiedadesCrearCuenta["NombreUsuario"]))
        {
            textoFinal.AppendLine("El Nombre de Usuario ya existe");
        }

        if ( await ComprobarCorreoElectronico(_propiedadesCrearCuenta["CorreoElectronico"]))
        {
            textoFinal.AppendLine("\n El Correo Electronico ya existe");
        }

        if (!_propiedadesCrearCuenta["CorreoElectronico"].Contains('@'))
        {
            textoFinal.AppendLine("\n El correo no es Valido");
        }
        
        if (!ComprobarContrasena(_propiedadesCrearCuenta["Contrasena"]))
        {
            textoFinal.AppendLine("\n La contraseña no es Valida, tiene menos de 12 caracteres");
        }

        if (_propiedadesCrearCuenta["Contrasena"] != _propiedadesCrearCuenta["ConfirmarContrasena"])
        {
            textoFinal.AppendLine("\n Las contraseñas no coinciden.");
        }

        if (textoFinal.Length == 0 )
            textoFinal.AppendLine("Todo correcto se ha creado la cuenta.");


        if (parameter is CrearCuenta crearCuenta)
        {
            var popupECC = new PopupErroresCrearCuenta
            {
                BindingContext = new PopupErroresCrearCuentaViewModel(textoFinal.ToString())
            };
            await crearCuenta.ShowPopupAsync(popupECC);
            popupECC.BindingContext = this;
        }
        else
            throw new Exception("El parametro de confirmar de Crear Cuenta no es el que se esperaba");

    }

    public async Task<bool> ComprobarNombreUsuario(string nombreUsuario)
    {
        try
        {
            using TcpClient cliente = new();
            await cliente.ConnectAsync("192.168.19.150", 5000); // IP y puerto del servidor

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

                return mensaje.Respuesta == EnumRespuesta.Existente;
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
            await cliente.ConnectAsync("192.168.19.150", 5000); // IP y puerto del servidor

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
            int bytesLeidos = await stream.ReadAsync(buffer, 0, buffer.Length);
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

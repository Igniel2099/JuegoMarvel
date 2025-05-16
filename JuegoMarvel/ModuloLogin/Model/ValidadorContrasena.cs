using System.Text.RegularExpressions;

namespace JuegoMarvel.ModuloLogin.Model;

/// <summary>
/// Valida la política de contraseñas (longitud, dígitos, letras y caracteres especiales).
/// </summary>
public class ValidadorContrasena
{
    /// <summary>
    /// Comprueba nulos, coincidencia y formato.
    /// </summary>
    public (bool EsValido, string? Mensaje) Validar(string? contrasena, string? confirmar)
    {
        if (string.IsNullOrWhiteSpace(contrasena) || string.IsNullOrWhiteSpace(confirmar))
            return (false, "Las contraseñas no pueden quedar vacías.");

        if (contrasena != confirmar)
            return (false, "Las contraseñas no coinciden.");

        if (contrasena.Length < 12)
            return (false, "La contraseña debe tener al menos 12 caracteres.");

        if (!Regex.IsMatch(contrasena, "[A-Za-z]"))
            return (false, "La contraseña debe contener al menos una letra.");

        if (!Regex.IsMatch(contrasena, "\\d"))
            return (false, "La contraseña debe contener al menos un número.");

        if (!Regex.IsMatch(contrasena, @"[!@#$%^&*(),.?""':{}|<>_\-\[\]\\/]"))
            return (false, "La contraseña debe contener al menos un carácter especial.");

        return (true, null);
    }
}

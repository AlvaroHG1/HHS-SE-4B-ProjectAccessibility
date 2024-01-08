namespace ProjectAccessibility.Controllers;

using BCrypt.Net;

public static class Utils
{
    public static string HashPassword(string password)
    {
        var passwordHash = BCrypt.HashPassword(password);
        return passwordHash;
    }

    public static bool PasswordMatch(string enteredPassword, string hashedPassword)
    {
        return BCrypt.Verify(enteredPassword, hashedPassword);
    }
}
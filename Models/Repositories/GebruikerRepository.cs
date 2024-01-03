using MySqlConnector;
using ProjectAccessibility.Models;

public class GebruikerRepository
{
    private readonly string _connectionString;

    public GebruikerRepository(string connectionString)
    {
        _connectionString = connectionString;
    }

    public void CreateGebruiker(string email, string wachtwoord)
    {
        using MySqlConnection connection = new MySqlConnection(_connectionString);
        connection.Open();

        using MySqlCommand command = new MySqlCommand("INSERT INTO gebruiker (email, wachtwoord) VALUES (@email, @wachtwoord)", connection);
    
        // Add parameters to the command
        command.Parameters.AddWithValue("@email", email);
        command.Parameters.AddWithValue("@wachtwoord", wachtwoord);

        // Execute the command
        command.ExecuteNonQuery();
    }

    public Gebruiker GetGebruikerById(int userId)
    {
        using MySqlConnection connection = new MySqlConnection(_connectionString);
        connection.Open();

        using MySqlCommand command = new MySqlCommand("SELECT * FROM Gebruiker WHERE Gcode = @UserId", connection);
        command.Parameters.AddWithValue("@UserId", userId);

        using MySqlDataReader reader = command.ExecuteReader();

        if (reader.Read())
        {
            return MapToGebruiker(reader);
        }

        return null;
    }

    private Gebruiker MapToGebruiker(MySqlDataReader reader)
    {
        Gebruiker gebruiker = new Gebruiker
        {
            Gcode = reader.GetInt32("Gcode"),
            Email = reader.GetString("Email"),
            Wachtwoord = reader.GetString("Wachtwoord")
        };
        
        return gebruiker;
    }
}
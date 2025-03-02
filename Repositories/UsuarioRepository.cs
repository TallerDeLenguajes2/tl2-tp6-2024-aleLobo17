using usuarios;
using Microsoft.Data.Sqlite;
using iUsuariosRepository;

namespace usuarioReposiroty
{
    public class UsuariosRespository: IUsuariosRepository
    {
        private string CadenaDeConexion = "Data Source=db/Tienda.db;Cache=Shared";

    public void CrearUsuario(Usuarios user)
    {
        string query = @"INSERT INTO Usuarios (Nombre, Usuario, Contraseña, Rol) VALUES (@nombre, @user, @password, @rol)";

        using (SqliteConnection connection = new SqliteConnection(CadenaDeConexion))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@nombre", user.Nombre);
            command.Parameters.AddWithValue("@user", user.Username);
            command.Parameters.AddWithValue("@password", user.Password);
            command.Parameters.AddWithValue("@rol", user.Rol);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }

     public Usuarios ObtenerUsuario(string usuario, string password)
    {

        var userEncontrado = new Usuarios();
        string query = @"SELECT * FROM Usuarios WHERE Usuario = @user AND Contraseña = @password ";

        using (SqliteConnection connection = new SqliteConnection(CadenaDeConexion))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@user", usuario);
            command.Parameters.AddWithValue("@password", password);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    userEncontrado.Id = Convert.ToInt32(reader["idUser"]);
                    userEncontrado.Nombre = reader["Nombre"].ToString();
                    userEncontrado.Username = reader["Usuario"].ToString();
                    userEncontrado.Password = reader["Contraseña"].ToString();
                    int valorRol = Convert.ToInt32(reader["idUser"]);
                    userEncontrado.Rol = (Rol)valorRol;
                }
            }
            connection.Close();
        }
        return userEncontrado;
    }

    public void EliminarUsuario(int id)
    {
        string query = @"DELETE FROM Usuario WHERE idUser = @Id;";

        using (SqliteConnection connection = new SqliteConnection(CadenaDeConexion))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@Id", id);
            command.ExecuteNonQuery();
            connection.Close();
        }
    }
    }
}
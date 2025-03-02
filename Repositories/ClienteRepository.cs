using Microsoft.Data.Sqlite;
using presupuestos;
using presupuestosDetalle;
using productos;
using productoReposotory;
using clientes;
using SQLitePCL;

namespace clienteRepository
{

    public class ClientesRepository
    {
        private string CadenaDeConexion = "Data Source=db/Tienda.db;Cache=Shared";

        public void CrearCliente(Clientes cliente)
        {
            string query = @"INSERT INTO Clientes (Nombre, Email, Telefono) VALUES (@nombre, @email, @telefono)";

            using (SqliteConnection connection = new SqliteConnection(CadenaDeConexion))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);

                command.Parameters.AddWithValue("@nombre", cliente.NombreCliente);
                command.Parameters.AddWithValue("@email", cliente.EmailCliente);
                command.Parameters.AddWithValue("@telefono", cliente.TelefonoCliente);
                command.ExecuteNonQuery();
            }
        }

        public List<Clientes> ObtenerClientes()
        {
            List<Clientes> clientes = new List<Clientes>();

            string query = "SELECT * FROM Clientes";

            using (SqliteConnection connection = new SqliteConnection(CadenaDeConexion))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);

                using (SqliteDataReader reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Clientes nuevoCliente = new Clientes();
                        nuevoCliente.ClienteId = Convert.ToInt32(reader["ClienteId"]);
                        nuevoCliente.NombreCliente = reader["Nombre"].ToString();
                        nuevoCliente.EmailCliente = reader["Email"].ToString();
                        nuevoCliente.TelefonoCliente = reader["Telefono"].ToString();
                        clientes.Add(nuevoCliente);
                    }
                }
                connection.Close();
            }
            return clientes;
        }

        public void ModificarCliente(int id, Clientes cliente)
        {
            string query = @"UPDATE Clientes SET Nombre = @nombre, Email = @email, Telefono = @telefono WHERE ClienteId = @Id";

            using (SqliteConnection connection = new SqliteConnection(CadenaDeConexion))
            {
                connection.Open();
                SqliteCommand command = new SqliteCommand(query, connection);
                command.Parameters.AddWithValue("@nombre", cliente.NombreCliente);
                command.Parameters.AddWithValue("@email", cliente.EmailCliente);
                command.Parameters.AddWithValue("@telefono", cliente.TelefonoCliente);
                command.Parameters.AddWithValue("@Id", id);
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        public Clientes ObtenerCliente(int id)
    {
        Clientes cliente = null; //Uso el null para devolver en caso de no encontrar nada

        string query = @"SELECT * FROM Clientes WHERE ClienteId = @id ";

        using (SqliteConnection connection = new SqliteConnection(CadenaDeConexion))
        {
            connection.Open();
            SqliteCommand command = new SqliteCommand(query, connection);
            command.Parameters.AddWithValue("@id", id);
            using (SqliteDataReader reader = command.ExecuteReader())
            {
                if (reader.Read())
                {
                    cliente = new Clientes();
                    cliente.ClienteId = Convert.ToInt32(reader["ClienteId"]);
                    cliente.NombreCliente = reader["Nombre"].ToString();
                    cliente.EmailCliente = reader["Email"].ToString();
                    cliente.TelefonoCliente = reader["Telefono"].ToString();
                }
            }
            connection.Close();
        }
        return cliente;
    }

        public void EliminarCliente(int id)
    {
        string query = @"DELETE FROM Clientes WHERE ClienteId = @Id";

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
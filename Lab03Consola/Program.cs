//Librerias del ADO .NET
using System.Data.SqlClient;
using System.Data;

class Program
{
    // Cadena de conexión a la base de datos
    public static string connectionString = "Data Source=LAB1504-24\\SQLEXPRESS;Initial Catalog=Tecsup2023DB;User ID=userTecsup;Password=123456";


    static void Main()
    {
        var list = ListarEmpleadosListaObjetos();
        foreach (var item in list)
        {
            Console.WriteLine(item.Id + "\t" + item.Nombre + "\t" + item.Apellidos + "\t" + item.Sueldo + "\t" + item.FechaVencimiento);
        };

    }

    //De forma desconectada
    private static DataTable ListarEmpleadosDataTable()
    {
        // Crear un DataTable para almacenar los resultados
        DataTable dataTable = new DataTable();
        // Crear una conexión a la base de datos
        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT * FROM Trabajadores";

            // Crear un adaptador de datos
            SqlDataAdapter adapter = new SqlDataAdapter(query, connection);


            // Llenar el DataTable con los datos de la consulta
            adapter.Fill(dataTable);

            // Cerrar la conexión
            connection.Close();

        }
        return dataTable;
    }

    //De forma conectada
    private static List<Trabajador> ListarEmpleadosListaObjetos()
    {
        List<Trabajador> trabajador = new List<Trabajador>();

        using (SqlConnection connection = new SqlConnection(connectionString))
        {
            // Abrir la conexión
            connection.Open();

            // Consulta SQL para seleccionar datos
            string query = "SELECT IdTrabajador,Nombre,Apellidos,Sueldo,FechaVencimiento FROM Trabajadores";

            using (SqlCommand command = new SqlCommand(query, connection))
            {
                using (SqlDataReader reader = command.ExecuteReader())
                {
                    // Verificar si hay filas
                    if (reader.HasRows)
                    {
                        while (reader.Read())
                        {
                            // Leer los datos de cada fila

                            trabajador.Add(new Trabajador
                            {
                                Id = (int)reader["IdTrabajador"],
                                Nombre = reader["Nombre"].ToString(),
                                Apellidos = reader["Apellidos"].ToString(),
                                Sueldo = (decimal)reader["Sueldo"],
                                FechaVencimiento = (DateTime)reader["FechaVencimiento"]
                            });

                        }
                    }
                }
            }

            // Cerrar la conexión
            connection.Close();


        }
        return trabajador;

    }


}
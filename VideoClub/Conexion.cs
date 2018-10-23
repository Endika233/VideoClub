using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace VideoClub
{
    class Conexion
    {
        static String connectionString = ConfigurationManager.ConnectionStrings["conexionVideoClub"].ConnectionString;//Si hubiera que cambiar la direccion solo habria que cambiarlo aqui una vez
       static SqlConnection conexion = new SqlConnection(connectionString);

        public static SqlConnection GetConnection()
        {
            return conexion;
        }

    }
}

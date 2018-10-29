using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace VideoClub
{
    class Peliculas
    {
        //No creo la lista porque si no tendria que andar moviendola entre clases para no perderla como el cliente
        SqlConnection conexion = Conexion.GetConnection();
        static string cadena;
        static SqlCommand comando;
        private Clientes c1;
        private int edad;

        public Peliculas()
        {

        }
        public Peliculas(Clientes c1)
        {
            this.c1 = c1;
        }
        public void MostrarPeliculas(Clientes c1)
        {
            edad = c1.GetEdad(c1);
            conexion.Open();
            cadena = "SELECT * FROM Peliculas WHERE EdadRecomendada <= '" + edad + "'";
            comando = new SqlCommand(cadena, conexion);
            SqlDataReader peliculas = comando.ExecuteReader();
            while (peliculas.Read())
            {
                Console.WriteLine(Int32.Parse(peliculas["IdPelicula"].ToString()) + "\n" + peliculas["Titulo"].ToString() + "\n" + peliculas["Sinopsis"].ToString() + "\n" + peliculas["Director"].ToString() + "\n" + Int32.Parse(peliculas["Año"].ToString()) + "\n----------------------------\n");
            }
            conexion.Close();
        }
        public void MostrarPeliculasLogout()
        {
            conexion.Open();
            cadena = "SELECT * FROM Peliculas WHERE EdadRecomendada <= '" + edad + "'";
            comando = new SqlCommand(cadena, conexion);
            SqlDataReader peliculas = comando.ExecuteReader();
            while (peliculas.Read())
            {
                Console.WriteLine(peliculas["Titulo"].ToString() + "\n" + peliculas["Sinopsis"].ToString() + "\n" + peliculas["Director"].ToString() + "\n" + Int32.Parse(peliculas["Año"].ToString()) + "\n----------------------------\n");
            }
            conexion.Close();
        }
        public void MostrarPeliculasDisponibles(Clientes c1)
        {
            Console.WriteLine("\n\tA continuación se mostrarán las películas disponibles");
            edad = c1.GetEdad(c1);
            conexion.Open();
            cadena = "SELECT * FROM Peliculas WHERE EdadRecomendada <= '" + edad + "'";
            comando = new SqlCommand(cadena, conexion);
            SqlDataReader peliculas = comando.ExecuteReader();
            while (peliculas.Read())
            {
                if (peliculas["Estado"].ToString() == "l")
                {
                    Console.WriteLine(Int32.Parse(peliculas["IdPelicula"].ToString()) + "\n" + peliculas["Titulo"].ToString() + "\n" + peliculas["Sinopsis"].ToString() + "\n" + peliculas["Director"].ToString() + "\n" + Int32.Parse(peliculas["Año"].ToString()) + "\n----------------------------\n");
                }
            }
            conexion.Close();
        }
    }

}

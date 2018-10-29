using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace VideoClub
{
    class Alquileres
    {
        SqlConnection conexion = Conexion.GetConnection();
        static string cadena,respuesta;
        static SqlCommand comando;
        private int elecPelicula,newCod,peliculaId,cont=0;
        public Alquileres()
        {

        }
        public void AlquilarPelicula(Clientes c1)
        {
            Console.WriteLine("\n\tElija la película que desee alquilar");
            try
            {
                elecPelicula = Int32.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("\n\tIntroduzca un valor válido");
                AlquilarPelicula(c1);
            }
            try
            {
                conexion.Open();
                cadena = "SELECT ESTADO FROM PELICULAS WHERE IDPELICULA=" + elecPelicula;
                comando = new SqlCommand(cadena, conexion);
                SqlDataReader estadoPelicula = comando.ExecuteReader();
                estadoPelicula.Read();
                if (estadoPelicula[0].ToString() == "l")
                {
                    estadoPelicula.Close();
                    cadena = "UPDATE PELICULAS SET ESTADO='o' WHERE IDPELICULA=" + elecPelicula;
                    comando = new SqlCommand(cadena, conexion);
                    comando.ExecuteNonQuery();
                    cadena = "SELECT MAX(IDALQUILER) AS 'CodReserva' FROM ALQUILERES";
                    comando = new SqlCommand(cadena, conexion);
                    SqlDataReader maxCod;
                    maxCod= comando.ExecuteReader();
                    if (maxCod.Read())//Una hora perdida por este puto if
                    {
                         newCod = Int32.Parse(maxCod["CodReserva"].ToString()) + 1;
                    }
                    maxCod.Close();
                    cadena = "INSERT INTO ALQUILERES VALUES (" + newCod + ",'" + DateTime.Today + "','" + DateTime.Today.AddDays(3) + "'," + elecPelicula + ",'" + c1.GetNickUser() + "','no')";
                    comando = new SqlCommand(cadena, conexion);
                    comando.ExecuteNonQuery();
                    conexion.Close();
                    System.Console.ForegroundColor = ConsoleColor.Blue;
                    Console.WriteLine("\n\tPelícula alquilada con éxito\n");
                    System.Console.ForegroundColor = ConsoleColor.White;
                }
                else if(estadoPelicula[0].ToString() == "o")
                {
                    Console.WriteLine("\n\tEsa película esta actualmente alquilada, se procede a volver al MenuLogin\n----------------------");
                    conexion.Close();
                    return;
                }
            }catch/*(buscar la excepcion en concreto y ponerla aqui)*/
            {
                Console.WriteLine("\n\tNo se encuentra ninguna película que corresponda a su elección, se procede a volver al MenuLogin\n----------------");
            }
            conexion.Close();
        }
        public void AlquileresUsuario(Clientes c1)
        {
            conexion.Open();
            cadena = "SELECT * FROM ALQUILERES WHERE NICKUSER='" + c1.GetNickUser() + "' AND DEVUELTA LIKE 'no'";
            comando = new SqlCommand(cadena, conexion);
            SqlDataReader alquiladas;
            alquiladas = comando.ExecuteReader();
            if (!alquiladas.Read())
            {
                Console.WriteLine("\n\tEn estos momentos no tiene ninguna película alquilada, se procede a volver al menú LogIn\n----------------");
                conexion.Close();
                return;
            }
            alquiladas.Close();//Hay que cerrar y volver a abrir el lector porque si no se saltaría una lectura al comprobar si no hay ninguna pelicula alquilada
            alquiladas = comando.ExecuteReader();
            while (alquiladas.Read())
            {
            Console.WriteLine("\n\tA continuacion se mostraran las id de sus películas alquiladas, si la fecha ha sido excedida apareceran en rojo");
                peliculaId = Int32.Parse(alquiladas["IdPelicula"].ToString());
                int dayAlq = (DateTime.Now-DateTime.Parse(alquiladas["FinAlquiler"].ToString())).Days;
                if (dayAlq <=0)
                {
                    System.Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\n\tId Película:"+alquiladas["IDPelicula"].ToString() + "\n\tInicio Alquiler:" + alquiladas["InicioAlquiler"].ToString() + "\n\tFin Alquiler:" + alquiladas["FinAlquiler"].ToString());
                    System.Console.ForegroundColor = ConsoleColor.White;
                }
                else if (dayAlq > 0)
                {
                    System.Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("\n\tId Película:" + alquiladas["IDPelicula"].ToString() + "\n\tInicio Alquiler:"  + alquiladas["InicioAlquiler"].ToString() + "\n\tFin Alquiler:" + alquiladas["FinAlquiler"].ToString());
                    System.Console.ForegroundColor = ConsoleColor.White;
                }
            }
            alquiladas.Close();
            conexion.Close();
            Console.WriteLine("------------------------\n\t ¿Desea devolver alguna película? S/N (Introduzca Menu para volver al menú LogIn)");
            respuesta = Console.ReadLine().ToUpper();
            do
            {
                if (respuesta == "S")
                {
                    DevolverPelicula(c1);
                    return;
                }
                else if (respuesta == "N")
                {
                    return;
                }
            } while (respuesta != "MENU");
            return;
        }
        public void DevolverPelicula(Clientes c1)
        {
            Console.WriteLine("\n\tIntroduzca la Id de la película a devolver");
            try
            {
                elecPelicula = Int32.Parse(Console.ReadLine());
            }
            catch
            {
                Console.WriteLine("\n\tIntroduzca un valor válido");
                AlquilarPelicula(c1);
            }
            try
            {
                conexion.Open();
                cadena = "SELECT * FROM ALQUILERES WHERE NICKUSER='" + c1.GetNickUser() + "' AND DEVUELTA LIKE 'no'";
                comando = new SqlCommand(cadena, conexion);
                SqlDataReader estadoPelicula = comando.ExecuteReader();
                if (estadoPelicula.Read())
                {
                    estadoPelicula.Close();
                    cadena = "UPDATE PELICULAS SET ESTADO='l' WHERE IDPELICULA=" + elecPelicula;
                    comando = new SqlCommand(cadena, conexion);
                    comando.ExecuteNonQuery();
                    cadena = "UPDATE ALQUILERES SET DEVUELTA='si' WHERE IDPELICULA=" + elecPelicula + " AND NICKUSER='" + c1.GetNickUser() + "'";
                    comando = new SqlCommand(cadena, conexion);
                    comando.ExecuteNonQuery();
                    Console.WriteLine("\n\tPelicula devuelta con éxito\n--------------------");
                }
            }
            catch
            {
                conexion.Close();
                Console.WriteLine("\n\tNo se encuentra ninguna película que corresponda a su elección, se procede a volver al MenuLogin\n----------------");
            }
            conexion.Close();
        }
    }
}

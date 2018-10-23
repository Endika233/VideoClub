using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace VideoClub
{
    class Menu
    {
        SqlConnection conexion = Conexion.GetConnection();
        static string cadena;
        static SqlCommand comando;
        public Menu()
        {
            
        }
        public void Bienvenida()
        {
            Console.WriteLine("\n\n\t\t\t\t\tBienvenido al VideoClub \n");//TODO:Buscar un nombre para poner
        }
        public void MenuOption()
        {
            int eleccionPrimera;
            do
            {
                Console.WriteLine("\tElija una de las siguientes acciones:\n\t1.Registrar Usuario\n\t2.LogIn\n\t3.Consultar Películas\n\t0.Salir");
                try
                {
                    eleccionPrimera = Int32.Parse(Console.ReadLine());
                }
                catch//TODO:Si aqui no se pone que excepcion es funciona? Creo que si
                {
                    eleccionPrimera = -1;//Hay que darle un valor por si el try no le asignara
                    Console.WriteLine("   Por favor, introduzca un valor válido\n-----------------------");
                }
            } while (eleccionPrimera < 0 && eleccionPrimera > 4);
            switch (eleccionPrimera)
            {
                case 0:
                    Salir();
                    break;
                case 1:
                    RegistrarUsuario();
                    break;
                case 2:
                    break;
                case 3:
                    break;
                case 4:
                    break;
            }
        }
        public void RegistrarUsuario()
        {
            String nombre = null,nickUser=null,email=null;//Al usar nombre como una clase me deja asignarle valor nulo
            int añoNacimiento, mesNacimiento, diaNacimiento;//TODO:Con int usar Integer o Int para poder asignarle valor nuloo
            Console.WriteLine("\n\tHa elegido la opción Registrar usuario\n----------------------------\n\tIntroduzca su nombre");
            do
            {
                try
                {
                    nombre = Console.ReadLine();
                }
                catch
                {
                    Console.WriteLine("\n\tPor favor, introduzca un nombre válido");//TODO:Los mensajes de error de formato porque el usuario meta algo erroneo tal vez ponerle otro color
                }
            } while (nombre == null);
            Console.WriteLine("\n\tIntroduzca el apodo que quiera utilizar");//TODO:Revisar si apodo existe

        }
        public void LogIn()
        {

        }
        public void LogOut()
        {

        }
        public void VerPeliculas()//Enseñar peliculas dependiendo de la edad incluso si estan alquiladas
        {

        }
        public void AlquilarPelicula()
        {

        }
        public void Salir()
        {
            Console.WriteLine("\t*************************************\n\t\tMuchas gracias por elegir nuestros servicios, ¡Que tenga un buen día!");
        }
    }
}

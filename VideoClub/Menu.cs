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
        SqlDataReader match;
        static string cadena;
        static SqlCommand comando;
        private string nombre, nickUser, email;
        private DateTime fechaNacimiento;
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
                    MenuOption();
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
            Clientes c1 = new Clientes();
            Console.WriteLine("\n\tHa elegido la opción Registrar usuario\n----------------------------\n\tIntroduzca el apodo que desee utilizar");
            nickUser=c1.ComprobarNickExiste();
            Console.WriteLine("\n\tIntroduzca su nombre");
            nombre = c1.RegistroNombre();
            Console.WriteLine("\n\tIntroduzca el email que desee utilizar (puede usar el mismo para varios usuarios)");
            email = c1.RegistroEmail();
            fechaNacimiento = c1.GetFechaNacimiento();
            c1 = new Clientes(nickUser, email, nombre, fechaNacimiento);
            c1.AñadirBBDD();
            MenuOption();
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

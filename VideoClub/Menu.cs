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
        private string nombre, nickUser, email,password;
        private DateTime fechaNacimiento;
        private bool loged;
        public Menu()
        {
            
        }
        public void Bienvenida()
        {
            Console.WriteLine("\n\n\t\t\t\t\tBienvenido al VideoClub \n");//TODO:Buscar un nombre para poner
        }
        public void MenuOption()
        {

            int eleccion;
            do
            {
                Console.WriteLine("\tElija una de las siguientes acciones:\n\t1.Registrar Usuario\n\t2.LogIn\n\t3.Consultar Películas(solo apareceran las recomendadas para menores de 18 años)\n\t0.Salir");
                try
                {
                    eleccion = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                    eleccion = -1;
                    Console.WriteLine("   Por favor, introduzca un valor válido\n-----------------------");
                }
            } while (eleccion < 0 || eleccion > 3);
            switch (eleccion)
            {
                case 0:
                    Salir();
                    break;
                case 1:
                    RegistrarUsuario();
                    break;
                case 2:
                    LogIn();
                    break;
                case 3:
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
            Console.WriteLine("\n\tIntroduzca la contraseña que quiera utilizar");
            password = c1.RegistroPassword();
            c1 = new Clientes(nickUser, email, nombre, fechaNacimiento,password);
            c1.AñadirBBDD();
            MenuOption();
        }
        public void LogIn()
        {
            Clientes c1 = new Clientes();//Para resetear el cliente c1 si entraran despues de registrar cliente
            Console.WriteLine("\n\tHa elegido la opción LogIn\n----------------------------\n\tIntroduzca el nombre de usuario");//TODO:poner que tambien puedan loguearse con el email
            loged=c1.Loguearse();
            if (loged == true)
            {
                MenuLogin();
            }
            else
            {
                MenuOption();
            }
        }
        public void MenuLogin()
        {
            int eleccion;
            do
            {
                Console.WriteLine("\tElija una de las siguientes acciones:\n\t1.Ver Películas(solo aparecerán aquellas adecuadas para su edad)\n\t2.Alquilar Películas\n\t3.Mis Alquileres\n\t4.LogOut y salir del programa");
                try
                {
                    eleccion = Int32.Parse(Console.ReadLine());
                }
                catch
                {
                    eleccion = -1;
                    Console.WriteLine("   Por favor, introduzca un valor válido\n-----------------------");
                }
            } while (eleccion < 1 || eleccion > 4);
            switch (eleccion)
            {
                case 1:
                    VerPeliculas();
                    break;
                case 2:
                    break;
                case 3:
                    LogOut();
                    break;
                case 4:
                    loged = false;//No es necesario porque se cierra el programa y resetean los valores pero si el día de mañana este programa se acoplara a otro y en realidad nunca dejara de ejecutarse habria que hacerlo
                    Salir();
                    break;
            }
        }
        public void LogOut()
        {
            Clientes c1 = new Clientes();//Así reseteo la edad de la clase clientes a 18
            loged = false;
            MenuOption();
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

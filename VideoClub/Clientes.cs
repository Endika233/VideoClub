using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace VideoClub
{
    class Clientes
    {
        SqlConnection conexion = Conexion.GetConnection();
        static string cadena;
        static SqlCommand comando;
        static SqlDataReader match;
        private String nickUser = null,nombre=null,email=null,password=null;
        private int añoNacimiento, mesNacimiento, diaNacimiento;
        private DateTime fechaNacimiento;

        public Clientes()
        {

        }
        public Clientes(string nickUser,string email,string nombre,DateTime fechaNacimiento,string password)
        {
            this.nickUser = nickUser;
            this.email = email;
            this.nombre = nombre;
            this.fechaNacimiento = fechaNacimiento;
            this.password = password;
        }
        public void AñadirBBDD()
        {
            conexion.Open();
            cadena = " INSERT INTO CLIENTES VALUES ('" + nickUser + "','" + email + "','" + nombre + "','" + fechaNacimiento.ToString() + "','"+password+"')";
            comando = new SqlCommand(cadena, conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
            Console.WriteLine("\n\tUsuario registrado con éxito\n******************************************\n");
        }
        public string ComprobarNickExiste()
        {
            do
            {
                try
                {
                    nickUser = Console.ReadLine();
                }
                catch
                {
                    Console.WriteLine("\n\tIntroduzca un apodo válido");
                }
                conexion.Open();
                cadena = "SELECT * FROM CLIENTES WHERE NICKUSER LIKE '" + nickUser + "'";
                comando = new SqlCommand(cadena, conexion);
                match = comando.ExecuteReader();
                if (match.Read())
                {
                    Console.WriteLine("\n\tYa existe un usuario registrado con ese nombre, escoja otro por favor");
                    nickUser = null;
                }
                if (nickUser == "")
                {
                    Console.WriteLine("Debe introducir un nick para poder continuar");
                }
                match.Close();
                conexion.Close();
            } while (nickUser == null||nickUser=="");//TODO: en nick email y demas si no meten ningun dado en la respuesta guarda"" no null
            return nickUser;
        }
        public bool Loguearse()
        {
            do
            {
                nickUser = Console.ReadLine();
                conexion.Open();
                cadena = "SELECT * FROM CLIENTES WHERE NICKUSER LIKE '" + nickUser + "'";
                comando = new SqlCommand(cadena, conexion);
                match = comando.ExecuteReader();
                if (match.Read())
                {
                    match.Close();
                    do
                    {
                        Console.WriteLine("\n\tUsuario válido, introduzca su contraseña");
                        password = Console.ReadLine();
                        cadena = "SELECT * FROM CLIENTES WHERE NICKUSER LIKE '" + nickUser + "' AND PASS='" + password + "'";
                        comando = new SqlCommand(cadena, conexion);
                        match = comando.ExecuteReader();
                        if (match.Read())
                        {
                            Console.WriteLine("\n\tLa contraseña introducida es correcta");
                            match.Close();
                            return true;
                        }
                        match.Close();
                    } while (password.ToUpper() != "MENU");

                }
                else if (!match.Read()&&nickUser.ToUpper()!="MENU")
                {
                    Console.WriteLine("\n\tDebe introducir un usuario registrado anteriormente, si desea volver al menú principal escriba menu");
                    nickUser = null;
                }
                match.Close();
                conexion.Close();
            } while (nickUser == null || nickUser == ""||nickUser.ToUpper()!="MENU");
            return false;
        }
        public string RegistroNombre()
        {
            do
            {
                try
                {
                    nombre = Console.ReadLine();//TODO:Poner una salida a todos los bucles
                }
                catch
                {
                    Console.WriteLine("\n\tPor favor, introduzca un nombre válido");//TODO:Los mensajes de error de formato porque el usuario meta algo erroneo tal vez ponerle otro color
                }
                if (nombre == "")
                {
                    Console.WriteLine("Debe introducir un nombre para poder continuar");
                }
            } while (nombre == null||nombre=="");
            return nombre;
        }
        public string RegistroEmail()
        {
            do
            {
                try
                {
                    email = Console.ReadLine();//TODO:Poner una salida a todos los bucles
                    if (!email.Contains("@")||!email.Contains("."))//TODO: bucar el metodo en with para que solo coja los que acaben en .com... mirar algun caso mas para frenar el registro de email
                    {
                        email = null;
                        Console.WriteLine("\n\tPor favor, introduzca un email válido");
                    }
                }
                catch
                {
                    Console.WriteLine("\n\tPor favor, introduzca un email válido");//TODO:Los mensajes de error de formato porque el usuario meta algo erroneo tal vez ponerle otro color
                }
                if (email == "")
                {
                    Console.WriteLine("Debe introducir un email para poder continuar");
                }
            } while (email == null);
            return email;
        }
        public void RegistroAñoNacimiento()
        {
            Console.WriteLine("\n\tIntroduzca su año de nacimiento");
            try
            {
                añoNacimiento = Int32.Parse(Console.ReadLine());
                if (añoNacimiento < 1900|| añoNacimiento>DateTime.Today.Year)
                {
                    Console.WriteLine("\n\tIntroduce tu año de nacimiento real\n*************************");
                    RegistroAñoNacimiento(); // TODO: hacer con bucle
                    //TODO:poner break al final de los bucles para que se los salte
                }
            }
            catch
            {
                Console.WriteLine("\n\tIntroduzca un valor válido\n***********************");
                RegistroAñoNacimiento();
            }
        }
        public void RegistroMesNacimiento()
        {
            Console.WriteLine("\n\tIntroduzca su mes de nacimiento en número");
            try
            {
                mesNacimiento = Int32.Parse(Console.ReadLine());
                if (mesNacimiento < 1 || mesNacimiento>12)
                {
                    Console.WriteLine("\n\tIntroduce un mes de nacimiento válido\n*************************");
                    RegistroMesNacimiento();
                }
            }
            catch
            {
                Console.WriteLine("\n\tIntroduzca un valor válido\n***********************");
                RegistroMesNacimiento();
            }
        }
        public void RegistroDiaNacimiento()
        {
            Console.WriteLine("\n\tIntroduzca su día de nacimiento");
            try
            {
                diaNacimiento = Int32.Parse(Console.ReadLine());
                if (diaNacimiento < 1 || diaNacimiento > 31)
                {
                    Console.WriteLine("\n\tIntroduce un día válido\n*************************");
                    RegistroDiaNacimiento();
                }
            }
            catch
            {
                Console.WriteLine("\n\tIntroduzca un valor válido\n***********************");
                RegistroDiaNacimiento();
            }
        }
        public DateTime GetFechaNacimiento()
        {
            RegistroAñoNacimiento();
            RegistroMesNacimiento();
            RegistroDiaNacimiento();
            try
            {
            fechaNacimiento =new DateTime(añoNacimiento,mesNacimiento,diaNacimiento);
            }
            catch//Esto esta por si fuera un año bisiesto o el dia ese no existiria
            {
                Console.WriteLine("\n\tLa fecha de nacimiento introducida no es válida");
                GetFechaNacimiento();
            }
            return fechaNacimiento;
        }
        public string RegistroPassword()
        {
            do
            {
                try
                {
                    password = Console.ReadLine();//TODO:Poner una salida a todos los bucles
                }
                catch
                {
                    Console.WriteLine("\n\tPor favor, introduzca una contraseña válida");//TODO:Los mensajes de error de formato porque el usuario meta algo erroneo tal vez ponerle otro color
                }
                if (password == "")
                {
                    Console.WriteLine("Debe introducir una contraseña para poder continuar");
                }             
                else if (password.Length < 6)
                {
                    Console.WriteLine("La contraseña debe tener al menos 6 carácteres");
                    password = null;
                }
            } while (password == null || password == "");
            return password;
        }
    }
}

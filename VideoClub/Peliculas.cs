﻿using System;
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
        SqlConnection conexion = Conexion.GetConnection();
        static string cadena;
        static SqlCommand comando;
        private int edad;

        public Peliculas()
        {

        }
        public Peliculas(int edad)
        {
            this.edad = edad;
        }
        public void MostrarPeliculas(int edad)
        {
            
        }
    }

}

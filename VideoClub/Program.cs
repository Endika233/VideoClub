﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;

namespace VideoClub
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Console.ForegroundColor = ConsoleColor.White;
            Menu menu = new Menu();
            menu.Bienvenida();
            menu.MenuOption();
            Console.ReadLine();
        }
    }
}

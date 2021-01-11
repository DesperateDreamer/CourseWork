using System;
using BLL.Services.EntityServices;
using BLL.Models;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Menu
{
    public class MainMenu
    {
        public static void Welcome()
        {
            Console.Clear();
            Console.WriteLine("Welcome! Select an option\n1. Login to account\n2. Login to library(work with books)" +
                "\n3. Exit");
            switch(Console.ReadLine())
            {
                case "1":
                    StudentSubmenu submenu = new StudentSubmenu();
                    submenu.WelcomePage();
                    break;
                case "2":
                    BooksSubmenu booksSubmenu = new BooksSubmenu();
                    booksSubmenu.MainPage();
                    break;
                case "3":
                    Console.Clear();
                    Environment.Exit(0);
                    break;
                default:
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Enter command numb [1-2]");
                    Console.ResetColor();
                    Welcome();
                    break;
            }
        }
    }
}

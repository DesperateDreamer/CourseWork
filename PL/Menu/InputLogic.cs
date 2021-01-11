using System;
using System.Text.RegularExpressions;
using BLL.Services.EntityServices;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL.Menu
{
    public class InputLogic
    {
        private readonly static string NamePattern = @"^[A-Z][a-z]{1,15}$";
        private readonly static string TitlePattern = @"^[A-Z]";
        private readonly static string IDPattern = @"[A-ZA-Z]\d{8}$";
        private readonly static string FileNamePattern = @"(.bin)|(.json)|(.xml)$";
        private readonly static string GroupPattern = @"^[1-9]{3}$";

        public static string InputName(string message)      //correct first/last name of person
        {
            string Input;
            Regex regex = new Regex(NamePattern);
            do
            {
                Console.Write("Enter " + message + ":  ");
                Input = Console.ReadLine();
                if (!regex.IsMatch(Input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error! Check the entered " + message);
                    Console.ResetColor();
                }
            } while (!regex.IsMatch(Input));
       
            return Input;
        }

        public static string InputID(string message)        //correct ID of person
        {
            string Input;
            Regex regex = new Regex(IDPattern);
            do
            {
                Console.Write("Enter " + message + ":  ");
                Input = Console.ReadLine();
                if (!regex.IsMatch(Input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error! Check the entered " + message);
                    Console.ResetColor();
                }
            } while (!regex.IsMatch(Input));

            return Input;
        }

        public static int InputGroup()             //correct Group of student
        {
            string Input;
            int Group;
            Regex regex = new Regex(GroupPattern);
            do
            {
                Console.Write("Enter group:  ");
                Input = Console.ReadLine();
                if(!regex.IsMatch(Input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error! Check the entered group (group [100-1000])");
                    Console.ResetColor();
                }
            } while (!regex.IsMatch(Input));

            Group = int.Parse(Input);

            return Group;
        }

        public static string FileCheckIn(string Input)         //correct filename by serialization
        {
            string fileName = "CheckIn";
            return fileName + Input;
        }

        public static string BooksLibrary(string Input)
        {
            string fileName = "Books";
            return fileName + Input;
        }

        public static string ReaderFile(string Input)
        {
            string fileName = "ReaderForms";
            return fileName + Input;
        }

        public static string InputTitle()
        {
            string Input;
            Regex regex = new Regex(TitlePattern);
            do
            {
                Console.Write("Enter title: ");
                Input = Console.ReadLine();
                if (!regex.IsMatch(Input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error! Check the entered title");
                    Console.ResetColor();
                }
            } while (!regex.IsMatch(Input));

            return Input;
        }

        public static string InputAuthor()
        {
            string Author = InputName("author's first name") + " " + InputName("author's last name");
            return Author;
        }

        public static string SelectProvider()
        {
            string Input;
            Regex regex = new Regex(FileNamePattern);
            Console.WriteLine("Select serialization: ");
            Console.WriteLine("\n1. Binary (.bin)\n2. JSON (.json)\n3. XML (.xml)");
            do
            {
                Console.Write("Enter the desired extension: ");
                Input = Console.ReadLine();
                if (!regex.IsMatch(Input))
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Error! Check the entered file extension");
                    Console.ResetColor();
                }
            } while (!regex.IsMatch(Input));
            return Input;
        }
    }
}

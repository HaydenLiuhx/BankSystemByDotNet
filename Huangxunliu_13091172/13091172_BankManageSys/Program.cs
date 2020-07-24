using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace _13091172_BankManageSys
{
    class Program
    {
        #region Cursor and WriteAt
        protected static int origRow;
        protected static int origCol;

        protected static void WriteAt(string s, int x, int y)
        {
            try
            {
                Console.SetCursorPosition(origCol + x, origRow + y);
                Console.Write(s);
            }
            catch (ArgumentOutOfRangeException e)
            {
                Console.Clear();
                Console.WriteLine(e.Message);
            }
        }
        #endregion

        static void Main(string[] args)
        {
            #region readline in login.txt then spilt into arraylist then add into list then print all
            List<User> usersGroup = new List<User>();
            string path = @"login.txt";
            using (StreamReader sr = new StreamReader(path))
            {
                while (sr.Peek() >= 0)
                {
                    string str;
                    string[] strArray;
                    str = sr.ReadLine();

                    strArray = str.Split('|');
                    User user = new User
                    {
                        UserName = strArray[0],
                        Password = strArray[1]
                    };
                    usersGroup.Add(user);
                }
            }
            foreach (User user in usersGroup)
            {
                Console.WriteLine(user);
                Thread.Sleep(150);
                
            }
            Thread.Sleep(150);
            Console.Clear();
            #endregion

            while (true)
            {
                #region Interface design
                origRow = Console.CursorTop;
                origCol = Console.CursorLeft;

                // Draw the left side of a 5x5 rectangle, from top to bottom.
                WriteAt("+", 0, 0);
                for(int i = 1; i < 6; i++)
                {
                    WriteAt("|", 0, i);
                }
                WriteAt("+", 0, 6);
                // Draw the top side, from right to left.
                for (int i = 1; i < 40; i++)
                {
                    WriteAt("=", i, 0);
                }
                // Draw the right side, from bottom to top.
                for(int i = 1; i < 6; i++)
                {
                    WriteAt("|", 40, i);
                }
                WriteAt("+", 40, 0);
                Console.WriteLine("\n\tWELCOME TO ACCOUNT SYSTEM");
                Console.WriteLine("\t   LOGIN TO START");
                for(int i =1; i < 40; i++)
                {
                    WriteAt("-", i, 3);
                }
                Console.Write("\n\tUser Name : ");
                Console.Write("\n\tPass Word : ");
                // Draw the bottom side, from left to right.
                WriteAt("+", 40, 6);
                for (int i =1; i < 40; i++)
                {
                    WriteAt("=", i, 6);
                }
                Console.SetCursorPosition(20, 4);
                string username = Console.ReadLine();
                Console.SetCursorPosition(20, 5);
                string password = ReadPassword();
                Console.SetCursorPosition(0, 8);
                #endregion

                #region retrieve all users in list and check password,username then show menu
                for (int i = 0; i < usersGroup.Count; i++)
                {
                    if ((usersGroup[i].UserName == username) && (usersGroup[i].Password == password))
                    {
                        Console.WriteLine("Valid credential!...");
                        Bank bank = new Bank();
                        bank.Continue();
                        Console.WriteLine("WELCOME TO SIMPLE BANK ACCOUNT SYSTEM");
                        bank.P();


                        //bank.ShowAllUsers();
                        bank.InitialAccount();

                        bank.ShowCustomMenu();
                        bank.P();
                    }
                }

                Console.Write("\nThe password entered is : " + password + "       not right, sry\n Press any key to continue...");
                Console.ReadKey(false);
                Thread.Sleep(1000);
                Console.Clear();
                #endregion
            }
        }
        /// <summary>
        /// show password in asterisk(*)
        /// </summary>
        /// <returns></returns>
        #region ReadPassword
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring(0, password.Length - 1);
                        int pos = Console.CursorLeft;
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                        Console.Write(" ");
                        Console.SetCursorPosition(pos - 1, Console.CursorTop);
                    }
                }
                info = Console.ReadKey(true);
            }

            // add a new line because user pressed enter at the end of their password
            Console.WriteLine();
            return password;
        }
        #endregion
    }
}

﻿using System;
using System.Collections.Generic;
using BL;
using Persistance;

namespace ConsoleApp
{
    class Program
    {   
        static void Main(string[] args)
        {   
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            int drinkID;
            StaffBl bl = new StaffBl();
            Console.Write("User Name: ");
            string userName = Console.ReadLine();
            Console.Write("Password: ");
            string password = GetPassword();
            Console.WriteLine();
            Staff staff = bl.Login(userName, password);
            if(staff!=null)
            {   
                Console.WriteLine("Login successfully!");
                Console.WriteLine("HIGHLAND XIN CHÀO");
                Console.WriteLine("1.Order");
                Console.WriteLine("2.Search Order");
                int choice = InputChoice();
                switch (choice)
                {
                    case 1: 
                        Console.WriteLine("Danh sách đồ uống");
                        DrinkBL drinkBL = new DrinkBL();
                        List<Drink> listdrinks = drinkBL.GetDrinks();
                        foreach (Drink drink in listdrinks)
                        {
                            Console.WriteLine(drink.DrinkId +" " +drink.DrinkName );
                        }
                        Console.Write("Mời bạn nhập mã đồ uống: ");
                        drinkID = Convert.ToInt32(Console.ReadLine());
                        // SizeBL sizeBL = new SizeBL();
                        // Size size = sizeBL.GetSizeById(drinkID);
                        List<Drink> listdrink1s = drinkBL.GetDrinkById(drinkID);
                        foreach (Drink drink1s in listdrink1s)
                        {
                            Console.WriteLine(drink1s.DrinkId + " " + drink1s.DrinkName);
                        }                        
                        Pause();
                        break;

                    case 2:
                        Console.Write("Mời bạn nhập mã id Order: "); 
                        break;
                    
                    case 0: 
                        break;                    
                    
                    default:
                        break;
                }
            }
            else 
            {
                Console.WriteLine("Login fail!");
            }
        }

        
        static string GetPassword()
        {
            var pass = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && pass.Length > 0)
                {
                    Console.Write("\b \b");
                    pass = pass[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    pass += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);
            return pass;
        }

        static int InputChoice()
        {   
            Console.Write("Chon: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            return choice;
        }

        static void Pause()
        {
            Console.Write("Ấn một phím bất kì để tiếp tục:");
            Console.ReadLine();
        }
    }
}

using System;
using System.Collections.Generic;
using BL;
using Persistance;



namespace ConsoleApp
{
    public class Program
    {   
        static void Main(string[] args)
        {   
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
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
                            Console.Write(drink.DrinkId +" " +drink.DrinkName + " ");
                            foreach (Size size in drink.SizeList)
                            {
                                Console.Write(size.SizeName + ":" + size.Price + " ");
                            }
                            Console.WriteLine("");
                        }
                        CardBL cardBL = new CardBL();
                        List<Card> listcards = cardBL.GetAllCard();
                        Console.Write("Nhập mã đồ uống: ");
                        int drinkId = Convert.ToInt32(Console.ReadLine());
                        if (CheckDrinkForId(listdrinks,drinkId) == true)
                        {   
                            drinkId = drinkId - 1; 
                            Console.Write(listdrinks[drinkId].DrinkId + " " + listdrinks[drinkId].DrinkName + "    ");
                            foreach (Size size in listdrinks[drinkId].SizeList)
                            {
                                Console.Write(size.SizeName + ":" + size.Price + "    ");
                            }
                            Console.Write( "\nMời bạn chọn size: ");
                            string sizechoose = Console.ReadLine();
                            if ((CheckSize(listdrinks,sizechoose,drinkId) == 0) || (CheckSize(listdrinks,sizechoose,drinkId) == 1) || (CheckSize(listdrinks,sizechoose,drinkId) == 2))
                            {   
                                int i = CheckSize(listdrinks,sizechoose,drinkId);
                                // Console.WriteLine("Bạn đã nhập đúng");
                                // OrderBL orderBL = new OrderBL();
                                // Order order = new Order();
                                // Size size1 = new Size();
                                // size1 = listdrinks[drinkId].SizeList[i];
                                // Drink drink1 = new Drink();
                                // drink1 = listdrinks[drinkId];
                                // drink1.SizeList.Add(size1);
                                // order.OrderDrinks.Add(drink1);
                                // order.OrderDate = DateTime.Now.ToString();
                                // order.Status = false;
                                // order.OrderCard = listcards[0];
                                // order.OrderStaff = staff;
                            }
                            else
                            {
                                Console.WriteLine("Bạn đã nhập sai");
                            }
                        }
                        else
                        {
                            Console.WriteLine("Không có đồ uống tương ứng với mã Id đã nhập");
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

        static bool CheckDrinkForId(List<Drink> listdrinks, int drinkId)
        {   
            foreach (Drink drink in listdrinks)
            {
                if (drinkId == drink.DrinkId)
                {   
                    return true;
                }
            }
            return false;
        }

        static int CheckSize(List<Drink> listdrinks, string sizechoose, int drinkId)
        {
            foreach (Size size in listdrinks[drinkId].SizeList)
            {
                if (sizechoose == size.SizeName)
                {
                    if (size.SizeName == "S")
                    {
                        return 0;
                    }
                    if (size.SizeName == "M")
                    {
                        return 1;
                    }
                    if (size.SizeName == "L")
                    {
                        return 2;
                    }
                }
            }
            return 3;
        }

        static bool CheckCardById(List<Card> listcards, int cardId)
        {
            foreach (Card card in listcards)
            {
                if (cardId == card.CardId)
                {
                    if (listcards[cardId].Stat == 0)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }
    }
}

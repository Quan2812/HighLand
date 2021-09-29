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
            OrderBL orderBL = new OrderBL();
            Order order = new Order();
            StaffBl bl = new StaffBl();
            Drink drink1 = null;
            Size size1 = null;
            Staff staff = new Staff();
            int d = -1;
            int s = -1;
            do
            {
                Console.Write("User Name: ");
                string userName = Console.ReadLine();
                Console.Write("Password: ");
                string password = GetPassword();
                Console.WriteLine();
                staff = bl.Login(userName, password);
                if (staff != null)
                {
                    Console.WriteLine("Login successfully!");
                    Console.WriteLine("HIGHLAND XIN CHÀO");
                    Console.WriteLine("1.Order");
                    Console.WriteLine("2.Search Order");
                    int choice = InputChoice();
                    string choose = null;
                    switch (choice)
                    {
                        case 1:
                            do
                            {
                                Console.WriteLine("Danh sách đồ uống");
                                DrinkBL drinkBL = new DrinkBL();
                                List<Drink> listdrinks = drinkBL.GetDrinks();
                                foreach (Drink drink in listdrinks)
                                {
                                    Console.Write(drink.DrinkId + " " + drink.DrinkName + " ");
                                    foreach (Size size in drink.SizeList)
                                    {
                                        Console.Write(size.SizeName + ":" + size.Price + " ");
                                    }
                                    Console.WriteLine("");
                                }
                                do
                                {
                                Console.Write("Nhập mã đồ uống: ");
                                int drinkId = Convert.ToInt32(Console.ReadLine());
                                if (CheckDrinkForId(listdrinks, drinkId) == true)
                                {
                                    d = drinkId - 1;
                                    Console.Write(listdrinks[d].DrinkId + " " + listdrinks[d].DrinkName + "    ");
                                    foreach (Size size in listdrinks[d].SizeList)
                                    {
                                        Console.Write(size.SizeName + ":" + size.Price + "    ");
                                    }
                                    do
                                    {
                                        Console.Write("\nMời bạn chọn size: ");
                                        string sizechoose = Console.ReadLine();
                                        s = CheckSize(listdrinks, sizechoose, d);
                                        if ((s == 0) || (s == 1) || (s == 2))
                                        {
                                            bool check = CheckDrinkById(order, drinkId);
                                            Console.WriteLine(check);
                                            if (check == false)
                                            {
                                                Console.WriteLine("Bạn đã nhập đúng");
                                                drink1 = new Drink();
                                                size1 = new Size();
                                                size1 = listdrinks[d].SizeList[s];
                                                drink1 = drinkBL.GetDrinkById(drinkId);
                                                drink1.SizeList.Add(size1);
                                                order.OrderDrinks.Add(drink1);
                                                Console.WriteLine("Số phần tử trong Size: " + drink1.SizeList.Count);
                                                Console.WriteLine("Số đồ uống trong Order:" + order.OrderDrinks.Count);
                                                Console.Write("Bạn có muốn chọn thêm đồ uống nữa không ?(Y/N):  ");
                                                choose = Console.ReadLine();
                                            }
                                            if (check == true)
                                            {
                                                size1 = listdrinks[d].SizeList[s];
                                                bool c = CheckDrinkBySize(order, size1, drinkId);
                                                if (c == true)
                                                {
                                                    foreach (Drink drink in order.OrderDrinks)
                                                    {
                                                        if (drinkId == drink.DrinkId)
                                                        {
                                                            foreach (Size size in drink.SizeList)
                                                            {
                                                                if (size1.SizeName == size.SizeName)
                                                                {
                                                                    size.Quantity++;
                                                                    Console.WriteLine(size.Quantity);
                                                                    Console.WriteLine("Số phần tử trong Size: " + drink.SizeList.Count);
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    foreach (Drink drink in order.OrderDrinks)
                                                    {
                                                        if (drinkId == drink.DrinkId)
                                                        {
                                                            foreach (Size size in drink.SizeList)
                                                            {
                                                                drink.SizeList.Add(size);
                                                                Console.WriteLine("Số phần tử trong Size: " + drink.SizeList.Count);
                                                                break;
                                                            }
                                                        }
                                                    }
                                                }
                                                Console.WriteLine("Số đồ uống trong Order:" + order.OrderDrinks.Count);
                                                Console.Write("Bạn có muốn chọn thêm đồ uống nữa không ?(Y/N):  ");
                                                choose = Console.ReadLine();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("Bạn đã nhập sai");
                                        }
                                    } while (s == 3);
                                }
                                else
                                {
                                    Console.WriteLine("Không có đồ uống tương ứng với mã Id đã nhập");
                                }
                                } while (d == -1);

                            } while (choose == "Y");
                            while (true)
                            {
                                CardBL cardBL = new CardBL();
                                List<Card> listcards = cardBL.GetAllCard();
                                foreach (Card card in listcards)
                                {
                                    Console.Write(card.CardId + "  " + card.Stat);
                                }
                                Console.Write("Mời bạn nhập mã thẻ: ");
                                int cardId = Convert.ToInt32(Console.ReadLine());
                                if (CheckCardById(listcards, cardId) == true)
                                {
                                    Console.WriteLine("Bạn đã nhập đúng");
                                    cardId = cardId - 1;
                                    order.OrderCard = listcards[cardId];
                                    order.OrderStaff = staff;
                                    order.Status = false;
                                    break;
                                }
                                if (CheckCardById(listcards, cardId) == false)
                                {
                                    Console.WriteLine("Thẻ đang được sử dụng");
                                }
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
                    Console.WriteLine("Mời bạn nhập lại UserName, Password");
                }
            } while (staff == null);
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
                    if (size.SizeName == "M" && listdrinks[drinkId].SizeList.Count == 2)
                    {
                        return 0;
                    }
                    if (size.SizeName == "L" && listdrinks[drinkId].SizeList.Count == 2)
                    {
                        return 1;
                    }
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

        static bool CheckDrinkById(Order order, int drinkId)
        {
            foreach (Drink drink in order.OrderDrinks)
            {
                if (drinkId == drink.DrinkId)
                {
                    return true;
                }
            }
            return false;
        }

        static bool CheckDrinkBySize(Order order, Size size1, int drinkId)
        {
            foreach (Drink drink in order.OrderDrinks)
            {
                if (drinkId == drink.DrinkId)
                {
                    foreach (Size size in drink.SizeList)
                    {
                        if (size1.SizeName == size.SizeName)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }
}

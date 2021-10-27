using System;
using System.Collections.Generic;
using BL;
using Persistance;
using ConsoleTables;


namespace ConsoleApp
{
    public class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            Console.InputEncoding = System.Text.Encoding.Unicode;
            StaffBl bl = new StaffBl();
            Staff staff = new Staff();
            do
            {
                Console.Clear();
                Header();
                Console.WriteLine("----------LOGIN----------");
                Console.WriteLine("=========================");
                Console.Write("User Name: ");
                string userName = Console.ReadLine();
                Console.Write("Password: ");
                string password = GetPassword();
                Console.WriteLine();
                staff = bl.Login(userName, password);
                if (staff != null)
                {
                    Console.WriteLine("Login successfully!");
                    Pause();
                }
                else
                {
                    Console.WriteLine("Login fail!");
                    Pause();
                }
            } while (staff == null);
            DisplayMainMenu(staff);
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
            Console.Write("Your choice: ");
            int choice = Convert.ToInt32(Console.ReadLine());
            return choice;
        }
        static void Pause()
        {
            Console.WriteLine("Press any button to forward");
            Console.ReadKey();
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
                if (sizechoose.ToUpper() == size.SizeName)
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
                    if (listcards[cardId - 1].Stat == 0)
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
        static void DisplayMainMenu(Staff staff)
        {
            int choice;
            do
            {
                Console.Clear();
                Header();
                Console.WriteLine("+===============================+");
                Console.WriteLine("|----------HIGHLAND COFFE-------|");
                Console.WriteLine("+-------------------------------+");
                Console.WriteLine("|1. Create New Order            |");
                Console.WriteLine("+-------------------------------+");
                Console.WriteLine("|2. View Order Details          |");
                Console.WriteLine("+-------------------------------+");
                Console.WriteLine("|0. Exit                        |");
                Console.WriteLine("+===============================+");
                choice = InputChoice();
                switch (choice)
                {
                    case 1:
                        DisplaySubMenu1(staff);
                        break;
                    case 2:
                        DisplaySubMenu2();
                        break;
                    case 0:
                        break;
                    default:
                        break;
                }
            } while (choice != 0);
        }
        static void DisplaySubMenu1(Staff staff)
        {
            int choice;
            do
            {
                Console.Clear();
                Header();
                Console.WriteLine("+===============================+");
                Console.WriteLine("|       Create New Order        |");
                Console.WriteLine("+-------------------------------+");
                Console.WriteLine("|1. Create New Order            |");
                Console.WriteLine("+-------------------------------+");
                Console.WriteLine("|0. Back To Main Menu           |");
                Console.WriteLine("+===============================+");
                choice = InputChoice();
                OrderBL orderBL = new OrderBL();
                Order order = new Order();
                Drink drink1 = null;
                Size size1 = null;
                List<Order> listorders = orderBL.GetOrderToday();
                int count = listorders.Count;
                int d = -1;
                int s = -1;
                string choose = null;
                switch (choice)
                {
                    case 1:
                        do
                        {
                            Console.Clear();
                            // Console.WriteLine("Danh sách đồ uống");
                            DrinkBL drinkBL = new DrinkBL();
                            List<Drink> listdrinks = drinkBL.GetDrinks();
                            var table = new ConsoleTable("ID", "DRINK");
                            ShowAllDrink(listdrinks);
                            // foreach (Drink drink in listdrinks)
                            // {
                            //     // Console.Write(drink.DrinkId + " " + drink.DrinkName + " ");
                            //     table.AddRow(drink.DrinkId, drink.DrinkName);
                            // }
                            // table.Write(Format.Alternative);
                            do
                            {
                                Console.Write("Enter ID Drink: ");
                                int drinkId = Convert.ToInt32(Console.ReadLine());
                                if (CheckDrinkForId(listdrinks, drinkId) == true)
                                {
                                    Console.Clear();
                                    d = drinkId - 1;
                                    Console.Write(listdrinks[d].DrinkId + " " + listdrinks[d].DrinkName + "    ");
                                    foreach (Size size in listdrinks[d].SizeList)
                                    {
                                        Console.Write(size.SizeName + ":" + size.Price + "    ");
                                    }
                                    do
                                    {
                                        Console.Write("\nPlease choose size: ");
                                        string sizechoose = Console.ReadLine();
                                        s = CheckSize(listdrinks, sizechoose, d);
                                        if ((s == 0) || (s == 1) || (s == 2))
                                        {
                                            bool check = CheckDrinkById(order, drinkId);
                                            // Console.WriteLine(check);
                                            if (check == false)
                                            {
                                                // Console.WriteLine("Bạn đã nhập đúng");
                                                drink1 = new Drink();
                                                size1 = new Size();
                                                size1 = listdrinks[d].SizeList[s];
                                                drink1 = drinkBL.GetDrinkById(drinkId);
                                                drink1.SizeList.Add(size1);
                                                order.OrderDrinks.Add(drink1);
                                                // Console.WriteLine("Số phần tử trong Size: " + drink1.SizeList.Count);
                                                // Console.WriteLine("Số đồ uống trong Order:" + order.OrderDrinks.Count);
                                                Console.Write("Would you like to choose more drinks? ?(Y/N):  ");
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
                                                                    // Console.WriteLine(size.Quantity);
                                                                    // Console.WriteLine("Số phần tử trong Size: " + drink.SizeList.Count);
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                if (c == false)
                                                {
                                                    foreach (Drink drink in order.OrderDrinks)
                                                    {
                                                        if (drinkId == drink.DrinkId)
                                                        {
                                                            drink.SizeList.Add(size1);
                                                        }
                                                    }
                                                }
                                                // Console.WriteLine("Số đồ uống trong Order:" + order.OrderDrinks.Count);
                                                Console.Write("Would you like to choose more drinks? ?(Y/N):  ");
                                                choose = Console.ReadLine();
                                            }
                                        }
                                        else
                                        {
                                            Console.WriteLine("You entered it wrong");
                                        }
                                    } while (s == 3);
                                }
                                else
                                {
                                    Console.WriteLine("There is no drink corresponding to the entered Id code");
                                }
                            } while (d == -1);

                        } while (choose.ToUpper() == "Y");
                        while (true)
                        {
                            Console.Clear();
                            CardBL cardBL = new CardBL();
                            if (listorders.Count == 0)
                            {
                                cardBL.UpdateAllCards();
                            }
                            List<Card> listcards = cardBL.GetAllCard();
                            ShowAllCard(listcards);
                            Console.Write("Please enter your card number: ");
                            int cardId = Convert.ToInt32(Console.ReadLine());
                            if (CheckCardById(listcards, cardId) == true)
                            {
                                // Console.WriteLine("Bạn đã nhập đúng");
                                int ca = cardId - 1;
                                order.OrderCard = listcards[ca];
                                order.OrderStaff = staff;
                                order.OrderDate = DateTime.Now;
                                Console.WriteLine("Create Order: " + (orderBL.CreateOrder(order) ? "completed!" : "not complete!"));
                                listorders = orderBL.GetOrderToday();
                                if (listorders.Count > count)
                                {
                                    cardBL.UpdateCard(cardId, order.Status);
                                    Pause();
                                    SubPrintInvoice(order);
                                }
                                break;
                            }
                            if (CheckCardById(listcards, cardId) == false)
                            {
                                Console.WriteLine("Card is in use");
                                Pause();
                            }
                            if ((cardId >= 23) || (cardId < 1))
                            {
                                Console.WriteLine("Card does not exist");
                                Pause();
                            }
                        }
                        Pause();

                        break;
                    case 0:
                        break;
                    default:
                        break;

                }
            } while (choice != 0);
        }
        static void DisplaySubMenu2()
        {
            int choice;
            do
            {
                Console.Clear();
                Header();
                Console.WriteLine("+===============================+");
                Console.WriteLine("|          Order List           |");
                Console.WriteLine("+-------------------------------+");
                Console.WriteLine("|1. View Order Details          |");
                Console.WriteLine("+-------------------------------+");
                Console.WriteLine("|0. Back To Main Menu           |");
                Console.WriteLine("+===============================+");
                choice = InputChoice();
                OrderBL orderBL = new OrderBL();
                CardBL cardBL = new CardBL();
                List<Order> listorders = new List<Order>();
                var table = new ConsoleTable("order_id", "order_date", "status", "staff", "card_number");
                int check = -1;
                string choose = null;
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        listorders = orderBL.GetOrderToday();
                        if (listorders.Count == 0)
                        {
                            Console.WriteLine("No orders have been placed today!(" + DateTime.Now.ToShortDateString() + ")");
                            Pause();
                        }
                        else
                        {
                            foreach (Order order in listorders)
                            {
                                if (order.Status == true)
                                {
                                    table.AddRow(order.OrderId, order.OrderDate, "Not Completed", order.OrderStaff.StaffName, order.OrderCard.CardId);
                                }
                                else
                                {
                                    table.AddRow(order.OrderId, order.OrderDate, "Completed", order.OrderStaff.StaffName, order.OrderCard.CardId);
                                }
                            }

                            Console.WriteLine("Today's Order List(" + DateTime.Now.ToShortDateString() + ")");
                            table.Write(Format.Alternative);
                            do
                            {
                                Console.WriteLine("Please enter 0 to exit");
                                Console.Write("Please enter your Order's Id code if you want Update: ");
                                int orderId = Convert.ToInt32(Console.ReadLine());
                                if (orderId == 0)
                                {
                                    check = 0;
                                }
                                foreach (Order order in listorders)
                                {
                                    if (orderId == order.OrderId && order.Status == true)
                                    {
                                        SubPrintInvoice(order);
                                        check = orderId;
                                        Console.Write("Do you want to update this Order?(Y/N): ");
                                        choose = Console.ReadLine();
                                        if (choose.ToUpper() == "Y")
                                        {
                                            Console.WriteLine("Update Order: " + (orderBL.UpdateOrderStatus(orderId) ? "completed!" : "not complete!"));
                                            cardBL.UpdateCard(order.OrderCard.CardId, order.Status);
                                            break;
                                        }
                                    }
                                }
                            } while (check == -1);
                            Pause();
                        }
                        break;
                    case 0:
                        break;
                    default:
                        break;
                }
            } while (choice != 0);

        }

        static void SubPrintInvoice(Order order)
        {
            double total = 0;
            string left = null;
            Console.Clear();
            Console.WriteLine("+================================================+");
            Console.WriteLine("|{0,16} HIGHLAND COFFEE                |", left);
            Console.WriteLine("+------------------------------------------------+");
            Console.WriteLine("| Add: 179 Nui Thanh, Hai Chau District, Da Nang |");
            Console.WriteLine("+------------------------------------------------+");
            Console.WriteLine("| Phone: 001009008007                            |");
            Console.WriteLine("+------------------------------------------------+");
            Console.WriteLine("|                * * RECEIPT * *                 |");
            Console.WriteLine("+------------------------------------------------+");
            Console.WriteLine("|{0,-18}{1,30}|", order.OrderStaff.StaffName, order.OrderDate);
            Console.WriteLine("+------------------------------------------------+");

            foreach (Drink drink in order.OrderDrinks)
            {
                foreach (Size size in drink.SizeList)
                {
                    Console.WriteLine("|{0,-20}{1,-10}{2,-10}{3,8}|", drink.DrinkName, size.SizeName, size.Quantity, size.Price.ToString("0,000"));
                    Console.WriteLine("+------------------------------------------------+");
                    total = total + Convert.ToDouble(size.Quantity) * size.Price;
                }
            }
            Console.WriteLine("|                         Totals:{0,16}|", total.ToString("0,000"));
            Console.WriteLine("+------------------------------------------------+");
            Console.WriteLine("|        ***Share your opinion with us***        |");
            Console.WriteLine("|https://www.facebook.com/highlandscoffeevietnam/|");
            Console.WriteLine("+------------------------------------------------+");
            Console.WriteLine("|                    THANK YOU                   |");
            Console.WriteLine("|                  SEE YOU AGAIN                 |");
            Console.WriteLine("+================================================+");
            // do
            // {
            //     Console.Write("Cash: ");
            //     cash = Convert.ToDouble(Console.ReadLine());
            //     if (cash < total)
            //     {
            //         Console.WriteLine("Cash must be greater than or equal to total.");
            //         Console.WriteLine("Please re-enter");
            //     }
            //     if (cash > total)
            //     {
            //         table.AddRow("Change: " + (cash - total));
            //         table.Write();
            //     }
            // } while (cash < total);
        }

        static void Header()
        {
            Console.WriteLine("+------------------------------------------------+");
            Console.WriteLine("|                 HIGHLAND COFFEE                |");
            Console.WriteLine("|     Version 4.0.9 Design by Bui Hong Quan      |");
            Console.WriteLine("+================================================+");
        }

        static void ShowAllDrink(List<Drink> listdrinks)
        {
            // foreach (Drink drink in listdrinks)
            // {
            //     Console.Write("|ID:{0,4}|", drink.DrinkId);
            //     Console.WriteLine("Drink:{0,20}|", drink.DrinkName);
            // }
            int choice = -1;
            Console.WriteLine("+=========================+");
            Console.WriteLine("| ID |      DrinkName     |");
            Console.WriteLine("+=========================+");
            for (int i = 0; i <= 9; i++)
            {
                Console.Write("|{0,4}|", listdrinks[i].DrinkId);
                Console.WriteLine("{0,20}|", listdrinks[i].DrinkName);
                Console.WriteLine("|-------------------------|");
            }
            Console.WriteLine("Page 1/3");
            do
            {
                Console.WriteLine("Press 0 to choose a drink");
                Console.Write("Please enter the number of Pages you want to see: ");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("+=========================+");
                        Console.WriteLine("| ID |      DrinkName     |");
                        Console.WriteLine("+=========================+");
                        for (int i = 0; i <= 9; i++)
                        {
                            Console.Write("|{0,4}|", listdrinks[i].DrinkId);
                            Console.WriteLine("{0,20}|", listdrinks[i].DrinkName);
                            Console.WriteLine("|-------------------------|");
                        }
                        Console.WriteLine("Page 1/3");
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("+=========================+");
                        Console.WriteLine("| ID |      DrinkName     |");
                        Console.WriteLine("+=========================+");
                        for (int i = 10; i <= 20; i++)
                        {
                            Console.Write("|{0,4}|", listdrinks[i].DrinkId);
                            Console.WriteLine("{0,20}|", listdrinks[i].DrinkName);
                            Console.WriteLine("|-------------------------|");
                        }
                        Console.WriteLine("Page 2/3");
                        break;
                    case 3:
                        Console.Clear();
                        Console.WriteLine("+=========================+");
                        Console.WriteLine("| ID |      DrinkName     |");
                        Console.WriteLine("+=========================+");
                        for (int i = 20; i < listdrinks.Count; i++)
                        {
                            Console.Write("|{0,4}|", listdrinks[i].DrinkId);
                            Console.WriteLine("{0,20}|", listdrinks[i].DrinkName);
                            Console.WriteLine("|-------------------------|");
                        }
                        Console.WriteLine("Page 3/3");
                        break;
                    default:
                        break;
                }
            } while (choice != 0);
        }

        static void ShowAllCard(List<Card> listcards)
        {
            int choice = -1;
            Console.Clear();
            Console.WriteLine("+=========================+");
            Console.WriteLine("| ID |        Status      |");
            Console.WriteLine("+=========================+");
            for (int i = 0; i <= 9; i++)
            {
                if (listcards[i].Stat == 0)
                {
                    Console.Write("|{0,4}|", listcards[i].CardId);
                    Console.WriteLine("Non Active          |");
                    Console.WriteLine("|-------------------------|");
                }
                else
                {
                    Console.Write("|{0,4}|", listcards[i].CardId);
                    Console.WriteLine("Is Active           |");
                    Console.WriteLine("|-------------------------|");
                }
            }
            Console.WriteLine("Page 1/2");
            do
            {
                Console.WriteLine("Press 0 to choose a card");
                Console.Write("Please enter the number of Pages you want to see: ");
                choice = Convert.ToInt32(Console.ReadLine());
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        Console.WriteLine("+=========================+");
                        Console.WriteLine("| ID |        Status      |");
                        Console.WriteLine("+=========================+");
                        for (int i = 0; i <= 9; i++)
                        {
                            // Console.Write(card.CardId + "  ");
                            if (listcards[i].Stat == 0)
                            {
                                Console.Write("|{0,4}|", listcards[i].CardId);
                                Console.WriteLine("Non Active          |");
                                Console.WriteLine("|-------------------------|");
                            }
                            else
                            {
                                Console.Write("|{0,4}|", listcards[i].CardId);
                                Console.WriteLine("Is Active           |");
                                Console.WriteLine("|-------------------------|");
                            }
                        }
                        Console.WriteLine("Page 1/2");
                        break;
                    case 2:
                        Console.Clear();
                        Console.WriteLine("+=========================+");
                        Console.WriteLine("| ID |        Status      |");
                        Console.WriteLine("+=========================+");
                        for (int i = 10; i < 20; i++)
                        {
                            // Console.Write(card.CardId + "  ");
                            if (listcards[i].Stat == 0)
                            {
                                Console.Write("|{0,4}|", listcards[i].CardId);
                                Console.WriteLine("Non Active          |");
                                Console.WriteLine("|-------------------------|");
                            }
                            else
                            {
                                Console.Write("|{0,4}|", listcards[i].CardId);
                                Console.WriteLine("Is Active            |");
                                Console.WriteLine("|-------------------------|");
                            }
                        }
                        Console.WriteLine("Page 2/2");
                        break;
                    default:
                        break;
                }
            } while (choice != 0);
        }
    }
}

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
                Console.WriteLine("LOGIN");
                Console.Write("User Name: ");
                string userName = Console.ReadLine();
                Console.Write("Password: ");
                string password = GetPassword();
                Console.WriteLine();
                staff = bl.Login(userName, password);
                if (staff != null)
                {
                    Console.WriteLine("Login successfully!");
                }
                else
                {
                    Console.WriteLine("Login fail!");
                    Console.WriteLine("Mời bạn nhập lại UserName, Password");
                    Pause();
                }
            } while (staff == null);
            Pause();
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
            Console.Write("Chọn: ");
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

        static string[] mainMenu = { "1. Order", "2. Xem danh sách Order", "0. Thoát" };
        static string[] subMenu1 = { "1. Tạo mới Order", "0. Quay lại menu chính" };
        static string[] subMenu2 = { "1.Danh sách Order", "2.Cập nhật trạng thái Order", "0.Quay lại menu chính" };

        static void DisplayMenu(string[] menu)
        {
            for (int i = 0; i < menu.Length; i++)
            {
                Console.WriteLine(menu[i]);
            }
        }
        static void DisplayMainMenu(Staff staff)
        {
            int choice;
            do
            {
                Console.Clear();
                Console.WriteLine("----------HIGHLAND COFFE XIN CHÀO---------");
                Console.WriteLine("==========================================");
                DisplayMenu(mainMenu);
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
                Console.WriteLine("===========================================");
                Console.WriteLine("Order\n");
                DisplayMenu(subMenu1);
                choice = InputChoice();
                OrderBL orderBL = new OrderBL();
                Order order = new Order();
                Drink drink1 = null;
                Size size1 = null;
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
                            var table = new ConsoleTable("ID", "Tên đồ uống");
                            foreach (Drink drink in listdrinks)
                            {
                                // Console.Write(drink.DrinkId + " " + drink.DrinkName + " ");
                                table.AddRow(drink.DrinkId, drink.DrinkName);
                            }
                            table.Write();
                            do
                            {
                                Console.Write("Nhập mã đồ uống: ");
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
                                        Console.Write("\nMời bạn chọn size: ");
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
                                                                    // Console.WriteLine(size.Quantity);
                                                                    // Console.WriteLine("Số phần tử trong Size: " + drink.SizeList.Count);
                                                                    break;
                                                                }
                                                            }
                                                        }
                                                    }
                                                }
                                                if(c == false)
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
                            Console.Clear();
                            CardBL cardBL = new CardBL();
                            List<Card> listcards = cardBL.GetAllCard();
                            var table1 = new ConsoleTable("ID thẻ", "Trạng thái");
                            foreach (Card card in listcards)
                            {
                                // Console.Write(card.CardId + "  ");
                                table1.AddRow(card.CardId, card.Stat);
                            }
                            table1.Write();
                            Console.Write("Mời bạn nhập mã thẻ: ");
                            int cardId = Convert.ToInt32(Console.ReadLine());
                            if (CheckCardById(listcards, cardId) == true)
                            {
                                // Console.WriteLine("Bạn đã nhập đúng");
                                int ca = cardId - 1;
                                order.OrderCard = listcards[ca];
                                order.OrderStaff = staff;
                                Console.WriteLine("Create Order: " + (orderBL.CreateOrder(order) ? "completed!" : "not complete!"));
                                if (order.OrderId != 0)
                                {
                                    cardBL.UpdateCard(cardId, order.Status);
                                }
                                break;
                            }
                            if (CheckCardById(listcards, cardId) == false)
                            {
                                Console.WriteLine("Thẻ đang được sử dụng");
                            }
                            if ((cardId >= 23) || (cardId < 1))
                            {
                                Console.WriteLine("Thẻ không tồn tại");
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
                Console.WriteLine("===========================================");
                Console.WriteLine("Danh sách Order\n");
                DisplayMenu(subMenu2);
                choice = InputChoice();
                OrderBL orderBL = new OrderBL();
                CardBL cardBL = new CardBL();
                List<Order> listorders = new List<Order>();
                var table = new ConsoleTable("order_id", "order_date", "stat", "staff_id", "card_id");
                int check = -1;
                switch (choice)
                {
                    case 1:
                        Console.Clear();
                        listorders = orderBL.GetOrderToday();
                        if (listorders.Count == 0)
                        {
                            Console.WriteLine("Chưa có order nào được tạo trong ngày hôm nay" + DateTime.Now.ToShortDateString() + ")");
                            Pause();
                        }
                        else
                        {
                            foreach (Order order in listorders)
                            {
                                table.AddRow(order.OrderId, order.OrderDate, order.Status, order.OrderStaff.StaffId, order.OrderCard.CardId);
                            }
                            Console.WriteLine("Danh sách Order ngày hôm nay(" + DateTime.Now.ToShortDateString() + ")");
                            table.Write();
                            Pause();
                        }
                        break;
                    case 2:
                        listorders = orderBL.GetOrderToday();
                        if (listorders.Count == 0)
                        {
                            Console.Clear();
                            Console.WriteLine("Chưa có order nào được tạo trong ngày hôm nay" + DateTime.Now.ToShortDateString() + ")");
                            Pause();
                        }
                        else
                        {
                            do
                            {
                                Console.Clear();
                                foreach (Order order in listorders)
                                {
                                    table.AddRow(order.OrderId, order.OrderDate, order.Status, order.OrderStaff.StaffId, order.OrderCard.CardId);
                                }
                                Console.WriteLine("Danh sách Order ngày hôm nay(" + DateTime.Now.ToShortDateString() + ")");
                                table.Write();
                                Console.WriteLine("Mời bạn nhập mã Id của Order");
                                int orderId = Convert.ToInt32(Console.ReadLine());
                                foreach (Order order in listorders)
                                {
                                    if (orderId == order.OrderId)
                                    {   
                                        check = orderId;
                                        Console.WriteLine("Update Order: " + (orderBL.UpdateOrderStatus(orderId, listorders) ? "completed!" : "not complete!"));
                                        Pause();
                                    }
                                    else
                                    {
                                        Console.WriteLine("Order với mã id: " + orderId + "chưa được tạo");
                                        Pause();
                                    }
                                }
                            } while (check == -1);

                        }

                        break;
                    case 0:
                        break;
                    default:
                        break;
                }
            } while (choice != 0);

        }
    }
}


using System;

using System.Collections.Generic;

using System.Linq;


namespace ConsoleApp1
{
    class Program
    {

        //создаем сам продукт
        public class Product
        {
            public string Name;
            public decimal Price;

            public Product(string name, decimal price)
            {
                Name = name;
                Price = price;
            }
        }
        public class Order
        {
            public decimal FullPrice;
            public List<Product> Products;
            public Order(List<Product> products)
            {
                Products = products;
                for (int i = 0; i < Products.Count; i++)
                {
                    FullPrice += Products[i].Price;
                }
            }

            public void ShowFullPrice()
            {
                Console.WriteLine(FullPrice);
            }
        }

        //хранилище для продуктов - магазин
        public class Store
        {
            //инициализаия списков
            public List<Product> Products;

            public List<Product> Basket = new List<Product>();

            public List<Order> Orders;

            public Store()
            {

                //инициализация списка продуктов (пример инициализации списка + вложение конкретных экземпляров)
                Products = new List<Product>
                {
                    new Product("Milk", 13),
                    new Product("Bread", 6),
                    new Product("Eggs", 7),
                    new Product("Cheese", 26),
                    new Product("Wine", 122)
                };
                //инициализация пустых списков для корзины и для фин.заказа
                Basket = new List<Product>();
                Orders = new List<Order>();
            }

            public void ShowCatalog()
            {
                Console.WriteLine("\tКаталог продуктов:");
                int number = 1;//для нумерации продуктов
                for (int i = 0; i < Products.Count; i++)
                {
                    Console.WriteLine($"\t{number}. {Products[i].Name} {Products[i].Price}");
                    number++;
                }
            }
            public void AddToBacket(int choise)
            {
                Basket.Add(Products[choise - 1]);

                Console.WriteLine("");
                Console.WriteLine($"Продукт {Products[choise - 1].Name} добавлен в корзину!");
                Console.WriteLine($"Количество позиций в корзине: {Basket.Count}");
            }
            public void AddProduct(string name, int price)
            {
                Product newProduct = new Product(name, price);
                Products.Add(newProduct);

                Console.WriteLine("");
                Console.WriteLine($"=== Продукт {newProduct.Name} добавлен в каталог ===");
            }
            public void DeleteProduct(int choise)
            {
                Products.Remove(Products[choise - 1]);

                Console.WriteLine("");
                Console.WriteLine($"=== Продукт под номером {choise} был удален из каталога ===");
            }

            public void ShowBacket()
            {
                Console.WriteLine("\tВаша корзина:");
                int number2 = 1;//для нумерации продуктов
                for (int i = 0; i < Basket.Count; i++)
                {
                    Console.WriteLine($"\t{number2}. {Basket[i].Name} {Basket[i].Price}");
                    number2++;
                }
            }
            public void CreateOrder()
            {
                //передать в отдел доставки затем опустошить корзину
                Order order = new Order(Basket);
                Orders.Add(order);
                //перед очисткой пересчитываем и выводим сумму цены всего в корзине
                decimal orderCost = 0;
                for (int i = 0; i < Basket.Count; i++)
                {
                    orderCost += Basket[i].Price;
                }
                Console.WriteLine("");
                Console.WriteLine($"=== Заказ на сумму {orderCost} обработан ===");
                Basket.Clear();
            }
            public void ClearBasket()
            {            
                Basket.Clear();
            }
        }

        static void Main(string[] args)
        {
           
            Store onlineStore = new Store();

            //исполняется тело главной программы
            MainMenu(onlineStore);

            //функция срабатывает при выборе входа для администратора

            static void LoginPasswordCheck(Store onlineStore)
            {
                Console.WriteLine("");
                Console.WriteLine("Введите логин:");
                string login = Console.ReadLine();
                Console.WriteLine("Введите пароль:");
                string password = Console.ReadLine();

                if (login != "135" && password != "135")
                {
                    Console.WriteLine("");
                    Console.WriteLine("===== В доступе отказано: неверный логин или пароль =====");
                    MainMenu(onlineStore);
                }
                else
                {
                    Admin(onlineStore);
                }
            }

            static void Admin(Store onlineStore)
            {
                Store x = onlineStore;
                Console.WriteLine("");
                Console.WriteLine("====== Доступ разрешен ======");

                while (true)
                {
                    Console.WriteLine("");
                    onlineStore.ShowCatalog();
                    Console.WriteLine("");
                    Console.WriteLine("Выберите действие");
                    Console.WriteLine("1. Добавить товар в каталог");
                    Console.WriteLine("2. Удалить товар из каталога");
                    Console.WriteLine("3. Выйти из режима администратора");
                    Console.WriteLine("");
                    Console.WriteLine("Выберите номер действия, которое хотите совершить");

                    int numAction = Convert.ToInt32(Console.ReadLine());
                    switch (numAction)
                    {
                        case 1:
                            bool isNotNull = true;
                            Console.WriteLine("");
                            do
                            {
                                try
                                {
                                    Console.WriteLine("Введите название товара");
                                    string name = Convert.ToString(Console.ReadLine());

                                    Console.WriteLine("Введите цену товара");
                                    int price = Convert.ToInt32(Console.ReadLine());
                                    onlineStore.AddProduct(name, price);
                                    if (name != "" & price != 0)
                                    {
                                        isNotNull = true;
                                    }
                                    else isNotNull = false;
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("");
                                    Console.WriteLine("======================================================");
                                    Console.WriteLine("ОШИБКА: Вы ничего не ввели, либо ввели неверный формат");
                                    Console.WriteLine("======================================================");
                                }

                            } while (isNotNull != true);
                            break;
                        case 2:
                            bool isInRange = true;
                            
                            do
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Введите номер товара, который нужно удалить");
                                //на случай, если человек введет не число, выбрасываем сообщения вместо сбоя программы
                                try
                                {
                                    int num = Convert.ToInt32(Console.ReadLine());

                                    if (num == 0 || num > onlineStore.Products.Count())
                                    {
                                        isInRange = false;
                                        Console.WriteLine("");
                                        Console.WriteLine("===================================");
                                        Console.WriteLine("ОШИБКА: Неверно указан номер товара");
                                        Console.WriteLine("===================================");
                                    }
                                    else isInRange = true;
                                    if (isInRange == true)
                                    {
                                        onlineStore.DeleteProduct(num);
                                    }
                                }
                                catch (FormatException)
                                {
                                    Console.WriteLine("");
                                    Console.WriteLine("===================================");
                                    Console.WriteLine("ОШИБКА: Неверно указан номер товара");
                                    Console.WriteLine("===================================");
                                }
                            } while (isInRange != true);

                            break;
                        case 3:
                            Console.WriteLine("");
                            Console.WriteLine("===== Вы вышли из режима администратора =====");
                            MainMenu(onlineStore);
                            break;
                        default:
                            Console.WriteLine("");
                            Console.WriteLine("=======================================================");
                            Console.WriteLine("Вы ничего не выбрали. Выберите номер действия из списка");
                            Console.WriteLine("=======================================================");
                            break;
                    }
                }
            }

            static void RequestAddToBacket(Store onlineStore)
            {
                while (true)
                {
                    bool isYes = true;

                    do
                    {
                        try
                        {
                            Console.WriteLine("");
                            Console.WriteLine("Хотите добавить продукт в корзину? Введите \"да\" или \"нет\"");

                            string agreement = Convert.ToString(Console.ReadLine());
                            if (agreement.ToLower() == "да")
                            {
                                isYes = true;
                            }
                            else if (agreement.ToLower() == "нет")
                            {
                                ShowBucketAndOrder(onlineStore);
                            }
                            else isYes = false;

                            if (isYes == true)
                            {
                                Console.WriteLine("");
                                Console.WriteLine("Введите номер продукта, который желаете добавить в корзину");
                                int choise = Convert.ToInt32(Console.ReadLine());
                                onlineStore.AddToBacket(choise);
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("=====================");
                            Console.WriteLine("Ошибка: неверный ввод");
                            Console.WriteLine("=====================");
                        }
                        catch (ArgumentException)
                        {
                            Console.WriteLine("");
                            Console.WriteLine("=====================");
                            Console.WriteLine("Ошибка: неверный ввод");
                            Console.WriteLine("=====================");
                        }

                    } while (isYes != true);                    
                }
            }
            static void ShowBucketAndOrder(Store onlineStore)
            {
                Console.WriteLine("");
                Console.WriteLine("Хотите посмотреть корзину?");
                string agreement2 = Convert.ToString(Console.ReadLine());
                if (agreement2.ToLower() == "да")
                {
                    if (!onlineStore.Basket.Any())
                    {
                        Console.WriteLine("");
                        Console.WriteLine("=== Ваша корзина и так пуста ===");
                        MainMenu(onlineStore);
                    }
                    else
                    {
                        onlineStore.ShowBacket();
                    }
                }
                
                Console.WriteLine("");
                Console.WriteLine("Хотите оформить заказ?");
                string agreement3 = Convert.ToString(Console.ReadLine());
                if (agreement3.ToLower() == "да")
                {
                    onlineStore.CreateOrder();
                    MainMenu(onlineStore);
                }
                else
                {
                    MainMenu(onlineStore);
                }
                if (agreement2.ToLower() == "нет" & agreement3.ToLower() == "нет")
                {
                    MainMenu(onlineStore);
                }
            }

            //тело программы вынесено в функцию специально, чтобы вставить исполнение главной программы
            //в случае неудачного входа для администратора
            static void MainMenu(Store onlineStore)
            {
                while (true)
                {
                    //создаем конкретные продукты
                    Console.WriteLine("");
                    Console.WriteLine("Выберите действие");
                    Console.WriteLine("1. Показать каталог продуктов");
                    Console.WriteLine("2. Вход для администратора");
                    Console.WriteLine("3. Очистить корзину");
                    Console.WriteLine("4. Выйти");
                    Console.WriteLine("Выберите номер действия, которое хотите совершить");
                    string numAction = Console.ReadLine();
                    bool isRight;

                    do
                    {
                        if (numAction != "1" || numAction != "2" || numAction != "3")
                        {
                            isRight = false;
                        }
                        else isRight = true;
                        switch (numAction)
                        {
                            case "1":
                                onlineStore.ShowCatalog();
                                RequestAddToBacket(onlineStore);
                                break;
                            case "2":
                                LoginPasswordCheck(onlineStore);
                                break;
                            case "3":
                                if (!onlineStore.Basket.Any())
                                {
                                    Console.WriteLine("");
                                    Console.WriteLine("=== Ваша корзина и так пуста ===");
                                    MainMenu(onlineStore);
                                }
                                else
                                {
                                    onlineStore.ClearBasket();
                                    Console.WriteLine("");
                                    Console.WriteLine("=== Корзина очищена ===");
                                    MainMenu(onlineStore);
                                }
                                break;
                            case "4":
                                Environment.Exit(0);
                                break;
                            default:
                                Console.WriteLine("");
                                Console.WriteLine("=======================================================");
                                Console.WriteLine("Вы ничего не выбрали. Выберите номер действия из списка");
                                Console.WriteLine("=======================================================");
                                break;
                        }
                        if (isRight == false)
                        {
                            string numAction2 = Console.ReadLine();
                            numAction = numAction2;
                        }
                    } while (isRight == false);
                }
            }
        }
    }
}






















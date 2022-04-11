using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Vending2022
{
    internal class VendingMachine : IVending
    {
        public VendingMachine()
        {
            //Initialize the vending machine with coins and wares
            CreateListOfCoins();
            CreateListOfProducts();
            SelectedItems = new List<Article>();
        }
        private List<int> _coinValues = new List<int>();

        public List<Article> SelectedItems { get; set; }

        private List<Article> _articles = new List<Article>();
        public int InsertedAmount { get; set; }

        public void ShowAll()
        {
            string newName;
            Console.WriteLine("Item no.\t  Name  \t\tPrice");
            foreach (var item in _articles)
            {
                //Make all strings 16 characters long, by adding spaces to the end or cutting them in case they are too long
                newName = MakeToSameSize(item.Name);
                Console.WriteLine("   {0}   \t\t  {1}  \t  {2}", _articles.IndexOf(item) + 1, newName, item.Price);
            }
        }
        public void EndTransaction()
        {
            //If we are to return the change in correct monetary values, we could use modulo operations to detect
            //what kind of change we should return, starting with the biggest values first
            // _coinValues is a list so we better make it into an array first
            int[] coinValuesArr = _coinValues.ToArray();

            //We also need to store away how many of each value. We use an array for this.
            int[] howManyOfEach = new int[coinValuesArr.Length];

            //We also need an amount to process.
            //The starting value will be the remaining funds to be returned to the user
            //But as we proceed, we will have less and less to convert into valid currencies
            int currentAmountToDistribute = InsertedAmount;

            Console.WriteLine();
            Console.WriteLine("Enjoy your purchase(s), said the vending machine returning {0} kr to you like this:", InsertedAmount);
            //The most significant monetary value is at the end of the array. That's why we pick the array elements in reverse order
            for (int i = coinValuesArr.Length-1; i >= 0; i--)
            {
                if ( currentAmountToDistribute >= coinValuesArr[i])
                {
                    howManyOfEach[i] = currentAmountToDistribute / coinValuesArr[i];
                    Console.WriteLine($"{howManyOfEach[i]} st x {coinValuesArr[i]} kr");
                    currentAmountToDistribute = currentAmountToDistribute % coinValuesArr[i];
                    //Console.WriteLine($"Remaining value: {currentAmountToDistribute}");
                }
            }
        }

        void CreateListOfCoins()
        {
            _coinValues.Add(1);
            _coinValues.Add(5);
            _coinValues.Add(10);
            _coinValues.Add(20);
            _coinValues.Add(50);
            _coinValues.Add(100);
            _coinValues.Add(500);
            _coinValues.Add(1000);
        }

        void CreateListOfProducts()
        {
            Snack snack = new Snack("Tuna sandwich", "Snack", 50);
            _articles.Add(snack);
            snack = new Snack("Ham and cheese", "Snack", 45);
            _articles.Add(snack);
            snack = new Snack("Vegie burger", "Snack", 55);
            _articles.Add(snack);
            //--------------------------------------------------------
            Beverage bev = new Beverage("Coca-cola", "Beverage", 20);
            _articles.Add(bev);
            bev = new Beverage("Fanta", "Beverage", 20);
            _articles.Add(bev);
            bev = new Beverage("Red bull", "Beverage", 20);
            _articles.Add(bev);
            //--------------------------------------------------------
            Sweets candy = new Sweets("Kitcat", "Sweets", 15);
            _articles.Add(candy);
            candy = new Sweets("Snickers", "Sweets", 15);
            _articles.Add(candy);
            candy = new Sweets("Marabou", "Sweets", 15);
            _articles.Add(candy);
        }

        public void InsertMoney()
        {
            int insertedCoin;
            Console.Write("\nEnter coin (End with 0): ");
            string coinString = Console.ReadLine();

            while (coinString != "0")
            {
                try
                {
                    insertedCoin = int.Parse(coinString);
                    if (_coinValues.Contains(insertedCoin))
                    {
                        UpdateInsertedAmount(insertedCoin);
                        DisplayCurrentBalance();
                    }
                    else
                    {
                        Console.WriteLine("Unrecognized coin type! Try again!");
                    }
                }
                catch (FormatException e)
                {
                    Console.WriteLine(e.Message);
                    Console.Write("Write a VALID integer number: ");
                }
                Console.Write("Enter coin (End with 0): ");
                coinString = Console.ReadLine();
            }

        }
        public void UpdateInsertedAmount(int amount)
        {
            InsertedAmount += amount;
        }

        public void DisplayCurrentBalance()
        {
            Console.WriteLine("You have inserted {0} kr", InsertedAmount);
        }
        public void DisplayCurrentCredit()
        {
            Console.WriteLine("You have {0} kr left", InsertedAmount);
        }
        public string GetAllCoinTypes()
        {
            string str = "";
            foreach (var item in _coinValues)
            {
                str += item.ToString() + ", ";
            }
            //Remove the last comma and space
            str = str.Substring(0, str.Length - 2);

            return str;
        }
        internal string MakeToSameSize(string str)
        {
            if (str.Length > 16)
            {
                //Chop it off 
                str = str.Substring(0, 16);
            }
            if (str.Length < 16)
            {
                //Add spaces
                str = str.PadRight(16, ' ');
            }
            return str;
        }

        public void Purchase()
        {
            int selectedArticleNo;

            Console.Write("\nSelect an article (End with 0): ");
            string articleString = Console.ReadLine();
            if (!string.IsNullOrEmpty(articleString)) {

                while (articleString != "0")
                {
                    try
                    {
                        selectedArticleNo = int.Parse(articleString);
                        //We are only goig to allow VALID integer numbers in a range defined by
                        //the total number of existing items inside the vending machine
                        //We are using the method "Count" on the list of items to get the maximum limit
                        //So we are basically saying that we only allow numbers from 1-6 (if the total of items in the list is 6)
                        if (selectedArticleNo > _articles.Count || selectedArticleNo <= 0)
                        {
                            Console.Write("\nWrite a VALID article number: ");
                        }
                        else
                        {
                            //Show the user how the item is used:
                            _articles[selectedArticleNo - 1].Use();

                            //We need to make a final check to see if the current credit is sufficient to
                            //allow the next purchase
                            UpdateSelectedArticles(selectedArticleNo);

                            DisplaySelectedArticles();
                            DisplayCurrentCredit();
                            ShowAll();
                        }
                    }
                    catch (FormatException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.Write("\nWrite a VALID article number: ");
                    }
                    catch (OverflowException e)
                    {
                        Console.WriteLine(e.Message);
                        Console.Write("\nArticle number too large: ");
                    }
                    Console.Write("Select an article (End with 0): ");
                    articleString = Console.ReadLine();
                }
            }
        }

        public void UpdateSelectedArticles(int articleNo)
        {
            //Add selected item to the list
            Article article = _articles[articleNo - 1];
            if (InsertedAmount >= article.Price)
            {
                SelectedItems.Add(article);
                InsertedAmount -= article.Price;
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nThe item you are trying to buy is too expensive!");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

        public void DisplaySelectedArticles()
        {
            Console.Write("\nYour items so far: ");
            foreach (var item in SelectedItems)
            {
                Console.Write("{0}, ", item.Name);
            }
        }

    }
}

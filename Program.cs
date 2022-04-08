using Vending2022;

//Create an instance of the Vending machine
VendingMachine vm = new VendingMachine();
Console.Write("\nAvailable coin types are: {0}", vm.GetAllCoinTypes());
vm.InsertMoney();

Console.WriteLine("\nYour balance: {0}", vm.InsertedAmount);
vm.ShowAll();

vm.Purchase();

vm.EndTransaction();


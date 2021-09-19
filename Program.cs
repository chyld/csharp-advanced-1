using System;
using System.IO;
using System.Text.Json;
using System.Collections.Generic;

namespace Adv1
{
  public class Program
  {
    static void Main(string[] args)
    {
      var cjson = File.ReadAllText("./data/customers.json");
      var tjson = File.ReadAllText("./data/transactions.json");
      var customers = JsonSerializer.Deserialize<List<Customer>>(cjson);
      var transactions = JsonSerializer.Deserialize<List<Transaction>>(tjson);
      var analytis = new Analytics(customers, transactions);

      var names = analytis.GetCustomerNames();
      Console.WriteLine($"Customers: {string.Join(", ", names)}");

      var avgAge = analytis.GetAverageAgeOfCustomers();
      Console.WriteLine($"Average customer age: {avgAge}");

      try
      {
        Console.Write("Category: ");
        var category = Console.ReadLine();
        var filtered = analytis.GetTransactionsByCategory(category);
        foreach (var f in filtered)
          Console.WriteLine($"Fruit Transactions: {f}");
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Exception caught: {ex.Message}");
      }

      var groups = analytis.GetTotalByCustomer();
      foreach (var g in groups)
        Console.WriteLine($"{g.Customer} spent a total of ${g.Total}");
    }
  }
}

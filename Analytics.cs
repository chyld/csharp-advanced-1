using System;
using System.Linq;
using System.Collections.Generic;

namespace Adv1
{
  public class Analytics
  {
    private List<Customer> _customers;
    private List<Transaction> _transactions;

    public Analytics(List<Customer> customers, List<Transaction> transactions)
    {
      _customers = customers;
      _transactions = transactions;
    }

    public IEnumerable<string> GetCustomerNames()
    {
      return from c in _customers select c.Name;
    }

    public double GetAverageAgeOfCustomers()
    {
      return (from c in _customers select c.Age).Average();
    }

    public IEnumerable<Transaction> GetTransactionsByCategory(string category = "phone")
    {
      var match = (new[] { "phone", "power", "fruit", "vegetable" }).Any(cat => cat == category);

      if (!match) throw new Exception("Incorrect Category");

      return from t in _transactions where t.Category == category select t;
    }

    public IEnumerable<(int Key, decimal Total, string Customer)> GetTotalByCustomer()
    {
      return from t in _transactions
             group t by t.CustomerId into tg
             select
             (
               Key: tg.Key,
               Total: tg.Sum(t => t.Cost),
               Customer: (from t in tg
                          join c in _customers on t.CustomerId equals c.Id
                          select c.Name).First()
             );
    }
  }
}

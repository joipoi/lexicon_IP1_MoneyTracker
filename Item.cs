using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace individualProject1_MoneyTracker
{
    class Item
    {
        public string Title { get; set; }
        public decimal Amount { get; set; }
        public string Month { get; set; }
        public bool IsExpense { get; set; }

        public Item(string title, decimal amount, string month, bool isExpense)
        {
            Title = title;
            Amount = amount;
            Month = month;
            IsExpense = isExpense;
        }

        public override string? ToString()
        {
            char sign = IsExpense ? '-' : '+';
            //CultureInfo.CurrentCulture gets what currency is used currently
            return $"{Title,-18} {sign}{Amount.ToString("C", CultureInfo.CurrentCulture),-19} {Month}";
        }
    }
}

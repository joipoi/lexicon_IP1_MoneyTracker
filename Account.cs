using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace individualProject1_MoneyTracker
{
    class Account
    {
        public List<Item> ItemList { get; set; }
        public decimal Balance { get; set; }

        public Account(List<Item> itemList)
        {
            ItemList = itemList;
            UpdateBalance();
        }

       
        public void UpdateBalance()
        {
            decimal total = 0;
            foreach(Item item in ItemList)
            {
                if (item.IsExpense)
                {
                    total -= item.Amount;
                }
                else
                {
                    total += item.Amount;
                }
            }
            Balance = total;
        }
    }
}

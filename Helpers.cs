using System;
using System.Collections.Generic;
namespace NgApi
{
    public class Helpers
    {
        private static Random rand = new Random();
        private static string GetRandom(IList<string> items)
        {
            return items[rand.Next(items.Count)];
        }
        internal static string MakeUniqueCustomerName(List<string> names)
        {
            var maxNames = bizPrefix.Count * bizSuffix.Count;
            if (names.Count >= maxNames)
            {
                throw new System.InvalidOperationException("Maximum number of unique names exceeded.");
            }
            var prefix = GetRandom(bizPrefix);
            var suffix = GetRandom(bizSuffix);
            var bizName = prefix + suffix;
            if (names.Contains(bizName))
            {
                MakeUniqueCustomerName(names);
            }
            return bizName;
        }

        internal static string MakeCustomerEmail(string customerName)
        {
            return $"contact@{customerName.ToLower()}.com";
        }

        internal static string GetRandomState()
        {
            return GetRandom(usStates);
        }

        private static readonly List<string> usStates = new List<string>()
        {
           "AK","AL","AR","AZ","CA","CO","CT","DE","FL","GA","HI","IA",
           "ID","IL","IN","KS","KY","LA","MA","MD","ME","MI","MN","MO",
           "MS","MT","NC","ND","NE","NH","NJ","NM","NV","NY","OH","OK",
           "OR","PA","RI","SC","SD","TN","TX","UT","VA","VT","WA","WI",
           "WV","WY","DC"
        };
        private static readonly List<string> bizPrefix = new List<string>()
        {
            "ABC",
            "XYZ",
            "MainSt",
            "Sales",
            "Enterprise",
            "Ready",
            "Quick",
            "Budget",
            "Peak",
            "Magic",
            "Family",
            "Comfort"
        };
        private static readonly List<string> bizSuffix = new List<string>()
        {
            "Corporation",
            "CO",
            "Logistics",
            "Transit",
            "Bakery",
            "Goods",
            "Foods",
            "Cleaners",
            "Hotels",
            "Planers",
            "Automotive",
            "Books"
        };

        internal static decimal GetRandomOrderTotal()
        {
            return rand.Next(100, 5000);
        }

        internal static DateTime GetRandomOrderPlaced()
        {
            var end = DateTime.Now;
            var start = end.AddDays(-90);

            TimeSpan possibleSpan = end - start;
            TimeSpan newSpan = new TimeSpan(0, rand.Next(0, (int)possibleSpan.TotalMinutes), 0);

            return start + newSpan;
        }
        internal static DateTime? GetRandomOrderComplited(DateTime orderPlaced)
        {
             var now = DateTime.Now;
             var minLeadTime = TimeSpan.FromDays(7);
             var timePassed = now - orderPlaced;
             if (timePassed < minLeadTime)
             {
                 return null;
             }
             return orderPlaced.AddDays(rand.Next(7, 14)); 
        }
    }
}
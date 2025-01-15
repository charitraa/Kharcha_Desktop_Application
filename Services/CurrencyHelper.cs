using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expenses_tracker.Services
{
    public static class CurrencyHelper
    {
        public static void SetCurrencySymbol(string currencyType)
        {
            StaticValue.CurrencyType = currencyType;

            switch (currencyType)
            {
                case "USD":
                    StaticValue.CurrencySymbol = "$";
                    break;
                case "NRP":
                    StaticValue.CurrencySymbol = "Rs";
                    break;
                case "EUR":
                    StaticValue.CurrencySymbol = "€";
                    break;
                case "GBP":
                    StaticValue.CurrencySymbol = "£";
                    break;
                case "JPY":
                    StaticValue.CurrencySymbol = "¥";
                    break;
                case "INR":
                    StaticValue.CurrencySymbol = "₹";
                    break;
                default:
                    StaticValue.CurrencySymbol = string.Empty;
                    break;
            }
        }
    }
}

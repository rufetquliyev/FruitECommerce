using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Exceptions.Fruit
{
    public class FruitNotFoundException : Exception
    {
        public string ParamName { get; set; }
        public FruitNotFoundException(string? message, string paramName) : base(message)
        {
            ParamName = paramName ?? string.Empty;
        }
    }
}

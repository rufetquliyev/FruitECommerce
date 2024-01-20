using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Exceptions.Fruit
{
    public class FruitImageException : Exception
    {
        public string ParamName { get; set; }
        public FruitImageException(string? message, string paramName) : base(message)
        {
            ParamName = paramName ?? string.Empty;
        }
    }
}

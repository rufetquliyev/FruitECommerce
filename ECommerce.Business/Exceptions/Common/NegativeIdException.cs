using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Exceptions.Common
{
    public class NegativeIdException : Exception
    {
        public string ParamName { get; set; }
        public NegativeIdException(string? message, string paramName) : base(message)
        {
            ParamName = paramName ?? string.Empty;
        }
    }
}

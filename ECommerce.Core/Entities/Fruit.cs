using ECommerce.Core.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Core.Entities
{
    public class Fruit : BaseAuditableEntity
    {
        public string FruitName { get; set; }
        public string CategoryName { get; set; }
        public string ImgUrl { get; set; }
    }
}

using ECommerce.Core.Entities;
using ECommerce.DAL.Context;
using ECommerce.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.DAL.Repository.Implementations
{
    public class FruitRepository : Repository<Fruit>, IFruitRepository
    {
        public FruitRepository(AppDbContext context) : base(context)
        {
        }
    }
}

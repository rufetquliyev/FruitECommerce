using ECommerce.Business.ViewModels.FruitVMs;
using ECommerce.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Services.Interfaces
{
    public interface IFruitService
    {
        public Task<IQueryable<Fruit>> GetAllAsync();
        public Task<Fruit> GetByIdAsync(int id);
        public Task CreateAsync(CreateFruitVm vm, string env);
        public Task UpdateAsync(UpdateFruitVm vm, string env);
        public Task Delete(int id, string env);
    }
}

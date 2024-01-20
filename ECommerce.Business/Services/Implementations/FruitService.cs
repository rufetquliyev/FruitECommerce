using AutoMapper;
using ECommerce.Business.Exceptions.Common;
using ECommerce.Business.Exceptions.Fruit;
using ECommerce.Business.Helpers;
using ECommerce.Business.Services.Interfaces;
using ECommerce.Business.ViewModels.FruitVMs;
using ECommerce.Core.Entities;
using ECommerce.DAL.Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Business.Services.Implementations
{
    public class FruitService : IFruitService
    {
        private readonly IFruitRepository _fruitRepository;
        private readonly IMapper _mapper;

        public FruitService(IFruitRepository fruitRepository, IMapper mapper)
        {
            _fruitRepository = fruitRepository;
            _mapper = mapper;
        }
        public async Task<IQueryable<Fruit>> GetAllAsync()
        {
            return await _fruitRepository.GetAllAsync();
        }
        public async Task<Fruit> GetByIdAsync(int id)
        {
            if (id <= 0) throw new NegativeIdException("Invalid ID received", nameof(id));
            return await _fruitRepository.GetByIdAsync(id);
        }
        public async Task CreateAsync(CreateFruitVm vm, string env)
        {
            if (!vm.Image.CheckImg()) throw new FruitImageException("The file must be image.", nameof(vm.Image));
            Fruit fruit = _mapper.Map<Fruit>(vm);
            fruit.ImgUrl = vm.Image.UploadImg(env, @"/Upload/FruitImages/");
            await _fruitRepository.CreateAsync(fruit);
            await _fruitRepository.SaveChangesAsync();
        }
        public async Task Delete(int id, string env)
        {
            var fruit = await CheckId(id);
            //fruit.ImgUrl.DeleteImg(env, @"/Upload/FruitImages");
            await _fruitRepository.Delete(fruit);
            await _fruitRepository.SaveChangesAsync();
        }
        public async Task UpdateAsync(UpdateFruitVm vm, string env)
        {
            var fruit = await CheckId(vm.Id);
            _mapper.Map(vm, fruit);
            if (vm.Image != null)
            {
                if (!vm.Image.CheckImg()) throw new FruitImageException("The file must be image.", nameof(vm.Image));
                fruit.ImgUrl.DeleteImg(env, @"/Upload/FruitImages");
                fruit.ImgUrl = vm.Image.UploadImg(env, @"/Upload/FruitImages/");
            }
            await _fruitRepository.UpdateAsync(fruit);
            await _fruitRepository.SaveChangesAsync();
        }
        public async Task<Fruit> CheckId(int id)
        {
            if (id <= 0) throw new NegativeIdException("Invalid ID received.", nameof(id));
            Fruit fruit = await _fruitRepository.GetByIdAsync(id);
            if (fruit == null) throw new FruitNotFoundException("The fruit not found.", nameof(fruit));
            return fruit;
        }
    }
}

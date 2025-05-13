using EFEjercicio1.Service.DTOs.Confectionery;
using EFEjercicio1.Service.DTOs.Drink;
using EFEjercicio1.Service.Interfaces;
using EFEjercicio1.Service.Mappers;
using EFEjercicio1.Service.Validators;
using EFEjercicio1Data.Interfaces;
using EFEjercicio1Entities;

namespace EFEjercicio1.Service.Services
{
    public class ConfectioneryService : IConfectioneryService
    {
        private readonly IConfectioneriesRepository _confectioneryRepository = null!;

        public ConfectioneryService(IConfectioneriesRepository repository)
        {
            _confectioneryRepository = repository;
        }

        public List<ConfectioneryDrinksCountDto> ConfectioneriesWithDrinksCount()
        {
            var confectioneryWithDrinks = _confectioneryRepository.GetAllWithDrinks();
            return confectioneryWithDrinks.Select(c => new ConfectioneryDrinksCountDto
            {
                Id = c.Id,
                Name = $"{c.Name}",
                DrinksCount = c.Drinks is null ? 0 : c.Drinks.Count,
                Drinks = c.Drinks != null
                ? c.Drinks.Select(DrinkMapper.ToDto).ToList()
                : new List<DrinkDto>()
            }).ToList();
        }

        public bool Create(ConfectioneryCreateDto confectioneryDto, out List<string> errors)
        {
            errors = new List<string>();
            Confectionery confectionery = ConfectioneryMapper.ToEntity(confectioneryDto);
            if (_confectioneryRepository.Exist(confectioneryDto.Name))
            {
                errors.Add("Confectionery already exist");
                return false;
            }
            var confectioneryValidator = new ConfectioneryValidator();
            if (!UniversalValidator.IsValid(confectionery, confectioneryValidator, out errors))
            {
                return false;
            }
            _confectioneryRepository.Add(confectionery);
            _confectioneryRepository.SaveChanges();
            return true;
        }

        public bool Delete(int confectioneryId, out List<string> errors)
        {
            errors = new List<string>();
            if (_confectioneryRepository.GetById(confectioneryId) is null)
            {
                errors.Add("Confectionery does not exist!!");
                return false;
            }
            if (_confectioneryRepository.HasDependencies(confectioneryId))
            {
                errors.Add("Confectionery with dependencies!!!");
                return false;
            }
            _confectioneryRepository.Delete(confectioneryId);
            _confectioneryRepository.SaveChanges();
            return true;
        }

        public bool Exist(string name, int? excludeId = null)
        {
            return _confectioneryRepository.Exist(name, excludeId);
        }

        public List<ConfectioneryDto> GetAll(string sortedBy = "Name")
        {
            var confectioneries = _confectioneryRepository.GetAll(sortedBy);
            return confectioneries.Select(ConfectioneryMapper.ToDto).ToList();
        }

        public List<ConfectioneryWithDrinksDto> GetAllWithDrinks()
        {
            var confectioneryWithDrinks = _confectioneryRepository.GetAllWithDrinks();
            return confectioneryWithDrinks.Select(c => new ConfectioneryWithDrinksDto
            {
                Id = c.Id,
                Name = $"{c.Name}",
                Drinks = c.Drinks != null
                    ? c.Drinks.Select(DrinkMapper.ToDto).ToList()
                    : new List<DrinkDto>()
            }).ToList();
        }

        public ConfectioneryDto? GetById(int confectioneryId)
        {
            var confectionery = _confectioneryRepository.GetById(confectioneryId);
            return confectionery is null ? null : ConfectioneryMapper.ToDto(confectionery);
        }

        public ConfectioneryDto? GetByName(string name)
        {
            var confectionery = _confectioneryRepository.GetByName(name);
            return confectionery is null ? null : ConfectioneryMapper.ToDto(confectionery);
        }

        public bool Update(ConfectioneryUpdateDto confectioneryDto, out List<string> errors)
        {
            errors = new List<string>();
            Confectionery confectionery = ConfectioneryMapper.ToEntity(confectioneryDto);
            if (_confectioneryRepository.Exist(confectioneryDto.Name,
                confectioneryDto.Id))
            {
                errors.Add("Confectionery already exist!!!");
                return false;
            }
            var confectioneryValidator = new ConfectioneryValidator();
            if (!UniversalValidator.IsValid(confectionery, confectioneryValidator, out errors))
            {
                return false;
            }
            _confectioneryRepository.Update(confectionery);
            _confectioneryRepository.SaveChanges();
            return true;
        }
    }
}

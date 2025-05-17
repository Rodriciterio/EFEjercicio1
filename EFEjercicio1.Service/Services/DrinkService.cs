using EFEjercicio1.Service.DTOs.Confectionery;
using EFEjercicio1.Service.DTOs.Drink;
using EFEjercicio1.Service.Interfaces;
using EFEjercicio1.Service.Mappers;
using EFEjercicio1.Service.Validators;
using EFEjercicio1Data;
using EFEjercicio1Entities;

namespace EFEjercicio1.Service.Services
{
    public class DrinkService : IDrinkService
    {
        private readonly IUnitOfWork _unitOfWork = null!;

        public DrinkService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public bool Delete(int drinkId, out List<string> errors)
        {
            errors = new List<string>();
            if (_unitOfWork.Drinks.GetById(drinkId) is null)
            {
                errors.Add("Drink ID not found");
                return false;
            }
            _unitOfWork.Drinks.Delete(drinkId);
            _unitOfWork.Complete();
            return true;
        }

        public bool Exist(string name, int confectioneryId, int? excludeId = null)
        {
            return _unitOfWork.Drinks.Exist(name, confectioneryId, excludeId);
        }

        public List<DrinkListDto> GetAll(string sortedBy = "name")
        {
            var drinks = _unitOfWork.Drinks.GetAll(sortedBy);
            return drinks.Select(DrinkMapper.ToDrinkListDto).ToList();
        }

        public DrinkDto? GetById(int drinkId)
        {
            var drink = _unitOfWork.Drinks.GetById(drinkId);
            return drink is null ? null : DrinkMapper.ToDto(drink);
        }

        //public Drink? GetByNameAndConfectioneryId(string name, int confectioneryId)
        //{
        //    return _unitOfWork.GetByNameAndConfectioneryId(name, confectioneryId);
        //}

        public List<DrinksWithConfectioneryDto> DrinksGroupByConfectionery()
        {
            var drinksWithConfectioneries = _unitOfWork.Drinks.GetAll();
            var grouped = drinksWithConfectioneries
                .GroupBy(d => new { d.Confectionery!.Id, d.Confectionery.Name })
                .Select(g => new DrinksWithConfectioneryDto
                {
                    Confectionery = new ConfectioneryDto
                    {
                        Id = g.Key.Id,
                        Name = g.Key.Name
                    },
                    Drinks = g.Select(DrinkMapper.ToDto).ToList()
                }).ToList();
            return grouped;
        }

        public bool Create(DrinkCreateDto drinkDto, out List<string> errors)
        {
            errors = new List<string>();
            Drink drink = DrinkMapper.ToEntity(drinkDto);
            if (_unitOfWork.Drinks.Exist(drink.Name, drink.ConfectioneryId))
            {
                errors.Add("Drink already exist");
                return false;
            }
            DrinkValidator drinkValidator = new DrinkValidator();
            if (!UniversalValidator.IsValid(drink, drinkValidator, out errors))
            {
                return false;
            }
            _unitOfWork.Drinks.Add(drink);
            _unitOfWork.Complete();
            return true;
        }

        public bool Update(DrinkUpdateDto drinkDto, out List<string> errors)
        {
            errors = new List<string>();
            Drink drink = DrinkMapper.ToEntity(drinkDto);
            if (_unitOfWork.Drinks.Exist(drink.Name, drink.ConfectioneryId, drink.Id))
            {
                errors.Add("Drink already exist");
                return false;
            }
            DrinkValidator drinkValidator = new DrinkValidator();
            if (!UniversalValidator.IsValid(drink, drinkValidator, out errors))
            {
                return false;
            }
            _unitOfWork.Drinks.Update(drink);
            _unitOfWork.Complete();
            return true;
        }
    }
}

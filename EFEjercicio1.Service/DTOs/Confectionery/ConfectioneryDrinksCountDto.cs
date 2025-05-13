using EFEjercicio1.Service.DTOs.Drink;

namespace EFEjercicio1.Service.DTOs.Confectionery
{
    public class ConfectioneryDrinksCountDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int DrinksCount { get; set; }
        public List<DrinkDto> Drinks { get; set; } = null!;
    }
}

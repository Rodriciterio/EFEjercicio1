using EFEjercicio1.Service.DTOs.Drink;

namespace EFEjercicio1.Service.DTOs.Confectionery
{
    public class ConfectioneryWithDrinksDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public List<DrinkDto> Drinks { get; set; } = null!;
    }
}

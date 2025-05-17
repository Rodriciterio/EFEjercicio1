using EFEjercicio1.Service.DTOs.Confectionery;

namespace EFEjercicio1.Service.DTOs.Drink
{
    public class DrinksWithConfectioneryDto
    {
        public ConfectioneryDto Confectionery { get; set; } = null!;
        public List<DrinkDto>? Drinks { get; set; }
    }
}

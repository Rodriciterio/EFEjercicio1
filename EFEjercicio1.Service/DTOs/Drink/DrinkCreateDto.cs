namespace EFEjercicio1.Service.DTOs.Drink
{
    public class DrinkCreateDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Size { get; set; } = null!;

        public int ConfectioneryId { get; set; }
    }
}

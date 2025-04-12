namespace EFEjercicio1Entities
{
    public class Drink
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Size { get; set; } = null!;

        public int ConfectioneryId { get; set; }
        public Confectionery? Confectionery { get; set; }

        public override string ToString()
        {
            return $"Drink: {Name} - Size: {Size}";
        }
    }
}

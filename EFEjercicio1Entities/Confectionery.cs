using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EFEjercicio1Entities
{
    [Table("Confectioneries")]
    [Index(nameof(Confectionery.Name), Name ="IX_Confectionaries_Name", IsUnique =true)]
    public class Confectionery
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="The field {0} is required")]
        [StringLength(100,ErrorMessage ="The field {0} must be between {2} and {1} characteres", MinimumLength =10)]

        public string Name { get; set; } = null!;

        public ICollection<Drink>? Drinks { get; set; }

        public override string ToString()
        {
            return $"{Name.ToUpper()}";
        }
    }
}

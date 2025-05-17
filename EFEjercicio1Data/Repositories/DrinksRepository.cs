using EFEjercicio1Data.Interfaces;
using EFEjercicio1Entities;
using Microsoft.EntityFrameworkCore;

namespace EFEjercicio1Data.Repositories
{
    public class DrinksRepository : IDrinksRepository
    {
        private readonly ConfectioneryContext _context = null!;

        public DrinksRepository(ConfectioneryContext context)
        {
            _context = context;
        }

        public List<Drink> GetAll(string sortedBy = "Name")
        {
            IQueryable<Drink> query = _context.Drinks
                .Include(d => d.Confectionery)
                .AsNoTracking();

            return sortedBy switch
            {
                "Name" => query.OrderBy(d => d.Name)
                                        .ThenBy(d => d.Name)
                                        .ToList(),
                "Size" => query.OrderBy(d => d.Size)
                                    .ThenBy(d => d.Size)
                                    .ToList(),
                _ => query.OrderBy(d => d.Id)
                .ThenBy(d => d.ConfectioneryId)
                .ToList(),
            };
        }

        public Drink? GetById(int drinkId, bool tracked = false)
        {
            var query = _context.Drinks
                .Include(d => d.Confectionery)
                .Where(d => d.Id == drinkId);

            return tracked ? query.FirstOrDefault() : query.AsNoTracking().FirstOrDefault();
        }

        public bool Exist(string drinkName, int drinkConfectioneryId, int? excludeId = null)
        {
            return excludeId.HasValue ? _context.Drinks.Any(d => d.Name == drinkName
                   && d.ConfectioneryId == drinkConfectioneryId && d.Id != excludeId)
               : _context.Drinks.Any(d => d.Name == drinkName
                   && d.ConfectioneryId == drinkConfectioneryId);
        }


        public void Add(Drink drink)
        {
            _context.Drinks.Add(drink);
        }

        public void Delete(int drinkId)
        {
            var drinkInDb = GetById(drinkId, true);
            if (drinkInDb != null)
            {
                _context.Drinks.Remove(drinkInDb);
            }
        }

        public void Update(Drink drink)
        {
            var drinkInDb = GetById(drink.Id);
            if (drinkInDb != null)
            {
                drinkInDb.Id = drink.Id;
                drinkInDb.Name = drink.Name;
                drinkInDb.ConfectioneryId = drink.ConfectioneryId;
                drinkInDb.Size = drink.Size;
                _context.Entry(drinkInDb).State = EntityState.Modified;
            }
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

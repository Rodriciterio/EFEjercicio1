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
            return tracked
                ? _context.Drinks
                    .FirstOrDefault(d => d.Id == drinkId)
                : _context.Drinks
                    .AsNoTracking()
                    .FirstOrDefault(d => d.Id == drinkId);
        }

        public bool Exist(string name, string size, int confectioneryId, int? excludeId = null)
        {
            return excludeId.HasValue
                ? _context.Drinks.Any(d => d.Name.ToLower() == name.ToLower()
                                        && d.Size.ToLower() == size.ToLower()
                                        && d.ConfectioneryId == confectioneryId
                                        && d.Id != excludeId)
                : _context.Drinks.Any(d => d.Name.ToLower() == name.ToLower()
                                        && d.Size.ToLower() == size.ToLower()
                                        && d.ConfectioneryId == confectioneryId);
        }


        public void Add(Drink drink)
        {
            _context.Drinks.Add(drink);
            _context.SaveChanges();

        }

        public void Delete(int drinkId)
        {
            var drinkInDb = GetById(drinkId, true);
            if (drinkInDb != null)
            {
                _context.Drinks.Remove(drinkInDb);
                _context.SaveChanges();

            }
        }

        public void Edit(Drink drink)
        {
            var drinkInDb = GetById(drink.Id, true);
            if (drinkInDb != null)
            {
                drinkInDb.Name = drink.Name;
                drinkInDb.Size = drink.Size;

                _context.SaveChanges();
            }
        }

        public Drink? GetByNameAndConfectioneryId(string name, int confectioneryId)
        {
            return _context.Drinks
                .AsNoTracking()
                .FirstOrDefault(d => d.Name.ToLower() == name.ToLower()
                                  && d.ConfectioneryId == confectioneryId);
        }

    }
}

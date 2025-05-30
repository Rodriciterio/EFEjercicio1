﻿using EFEjercicio1Data.Interfaces;
using EFEjercicio1Entities;
using Microsoft.EntityFrameworkCore;

namespace EFEjercicio1Data.Repositories
{
    public class ConfectioneriesRepository : IConfectioneriesRepository
    {
        private readonly ConfectioneryContext _context = null!;

        public ConfectioneriesRepository(ConfectioneryContext context)
        {
            _context = context;
        }

        public List<Confectionery> GetAll(string sortedBy = "Name")
        {
            IQueryable<Confectionery> query = _context.Confectioneries.AsNoTracking();

            return sortedBy switch
            {
                "Name" => query.OrderBy(c => c.Name).ToList(),
                "Id" => query.OrderBy(c => c.Id).ToList(),
                _ => query.OrderBy(c => c.Id).ToList(),
            };
        }   

        public List<Confectionery> GetAll()
        {
            return _context.Confectioneries
                   .OrderBy(c => c.Name)
                   .AsNoTracking()
                   .ToList();
        }

        public Confectionery? GetById(int confectioneryId, bool tracked = false)
        {
            return tracked
                ? _context.Confectioneries
                    .FirstOrDefault(c => c.Id == confectioneryId)
                    : _context.Confectioneries
                        .AsNoTracking()
                        .FirstOrDefault(c => c.Id == confectioneryId);

        }

        public bool Exist(string name, int? excludeId = null)
        {
            return excludeId.HasValue
                    ? _context.Confectioneries.Any(c => c.Name.ToLower() == name.ToLower() &&
                    c.Id == excludeId)
                    : _context.Confectioneries.Any(c => c.Name.ToLower() == name.ToLower());
        }

        public void Add(Confectionery confectionery)
        {
            _context.Confectioneries.Add(confectionery);
        }

        public void Delete(int confectioneryId)
        {
            var confectioneryInDb = GetById(confectioneryId);
            if (confectioneryInDb != null)
            {
                _context.Entry(confectioneryInDb).State = EntityState.Deleted;
            }

        }

        public void Update(Confectionery confectionery)
        {
            var confectioneryInDb = GetById(confectionery.Id);
            if (confectioneryInDb != null)
            {
                confectioneryInDb.Name = confectionery.Name;
                _context.Entry(confectioneryInDb).State = EntityState.Modified;
            }
        }

        public bool HasDependencies(int confectioneryId)
        {
            return _context.Drinks.Any(d => d.ConfectioneryId == confectioneryId);
        }

        public void LoadDrinks(Confectionery confectionery)
        {
            _context.Entry(confectionery).Collection(c => c.Drinks!).Load();
        }

        public List<Confectionery> GetAllWithDrinks()
        {
            return _context.Confectioneries.Include(c => c.Drinks).ToList();
        }

        public Confectionery? GetByName(string name)
        {
            return _context.Confectioneries
                .FirstOrDefault(c => c.Name.ToLower() == name.ToLower());
        }


        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

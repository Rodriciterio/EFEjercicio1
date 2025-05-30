﻿using EFEjercicio1Entities;

namespace EFEjercicio1Data.Interfaces
{
    public interface IConfectioneriesRepository
    {
        void Add(Confectionery confectionery);
        void Delete(int confectioneryId);
        void Update(Confectionery confectionery);
        bool Exist(string name, int? excludeId = null);
        List<Confectionery> GetAll();
        List<Confectionery> GetAll(string sortedBy = "Name");
        Confectionery? GetById(int confectioneryId, bool tracked = false);
        bool HasDependencies(int confectioneryId);
        void LoadDrinks(Confectionery confectionery);
        List<Confectionery> GetAllWithDrinks();
        Confectionery? GetByName(string name);
        void SaveChanges();
    }
}
using EFEjercicio1.Service.Interfaces;
using EFEjercicio1Data.Interfaces;
using EFEjercicio1Entities;

namespace EFEjercicio1.Service.Services
{
    public class ConfectioneryService : IConfectioneryService
    {
        private readonly IConfectioneriesRepository _repository = null!;

        public ConfectioneryService(IConfectioneriesRepository repository)
        {
            _repository = repository;
        }

        public List<IGrouping<int, Drink>> ConfectioneriesGroupIdDrinks()
        {
            return _repository.ConfectioneriesGroupIdDrinks();
        }

        public void Delete(int confectioneryId)
        {
            _repository?.Delete(confectioneryId);
        }

        public bool Exist(string name, int? excludeId = null)
        {
            return _repository.Exist(name, excludeId);
        }

        public List<Confectionery> GetAll(string sortedBy = "Name")
        {
            return _repository.GetAll(sortedBy);
        }

        public List<Confectionery> GetAllWithDrinks()
        {
            return _repository.GetAllWithDrinks();
        }

        public Confectionery? GetById(int confectioneryId, bool tracked = false)
        {
            return _repository.GetById(confectioneryId, tracked);
        }

        public bool HasDrinks(int confectioneryId)
        {
            return _repository.HasDrinks(confectioneryId);
        }

        public void LoadDrinks(Confectionery confectionery)
        {
            _repository.LoadDrinks(confectionery);
        }

        public void Save(Confectionery confectionery)
        {
            if (confectionery.Id==0)
            {
                _repository.Add(confectionery);
            }
            else
            {
                _repository.Edit(confectionery);
            }
        }
    }
}

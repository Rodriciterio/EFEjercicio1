using EFEjercicio1Data.Interfaces;

namespace EFEjercicio1Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ConfectioneryContext _context;

        public UnitOfWork(ConfectioneryContext context,
            IConfectioneriesRepository confectioneries,
            IDrinksRepository drinks)
        {
            _context = context;
            Confectioneries = confectioneries;
            Drinks = drinks;
        }

        public IConfectioneriesRepository Confectioneries { get; }
        public IDrinksRepository Drinks { get; }

        public int Complete()
        {
            return _context.SaveChanges();
        }
    }
}

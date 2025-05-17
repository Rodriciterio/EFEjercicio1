using EFEjercicio1Data.Interfaces;

namespace EFEjercicio1Data
{
    public interface IUnitOfWork
    {
        IConfectioneriesRepository Confectioneries { get; }
        IDrinksRepository Drinks { get; }
        int Complete();
    }
}

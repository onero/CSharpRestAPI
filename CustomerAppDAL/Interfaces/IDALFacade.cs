namespace RestAppDAL.Interfaces
{
    public interface IDALFacade
    {
        IUnitOfWork UnitOfWork { get; }
    }
}
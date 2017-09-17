using RestAppDAL.Interfaces;
using RestAppDAL.UOW;

namespace RestAppDAL
{
    public class DALFacade : IDALFacade
    {
        public IUnitOfWork UnitOfWork => new UnitOfWork();
    }
}
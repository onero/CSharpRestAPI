using RestAppDAL.Interfaces;
using RestAppDAL.UOW;

namespace RestAppDAL
{
    public class MockDALFacade : IDALFacade
    {
        public IUnitOfWork UnitOfWork => new MockUnitOfWork();
    }
}
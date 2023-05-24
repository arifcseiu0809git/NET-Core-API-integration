using DMS.DAL.Repositories.PromotionRepository;

namespace DMS.DAL.UnitOfWorks
{
    public interface IUnitOfWork
    {
        IESSRepository ESSRepository { get; }
    }
}

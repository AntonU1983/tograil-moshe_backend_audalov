using GenericBackend.DataModels;
using GenericBackend.DataModels.Actual;
using GenericBackend.DataModels.Document;
using GenericBackend.DataModels.Plan;
using GenericBackend.DataModels.Total;
using GenericBackend.Repository;
using GenericBackend.Repository.Admin;

namespace GenericBackend.UnitOfWork.GoodNightMedical
{
    public interface IUnitOfWork
    {
        IMongoRepository<ActualSheet> ActualSheets { get; }
        IMongoRepository<PlanSheet> PlanSheets { get; }
        IMongoRepository<TotalSheet> TotalSheets { get; }
        IMongoRepository<DocumentInfo> DocumentsInfo { get; }
        UserRepository Users { get; } 
    }

}

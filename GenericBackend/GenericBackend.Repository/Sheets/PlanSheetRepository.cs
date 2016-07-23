using GenericBackend.DataModels.Actual;
using GenericBackend.DataModels.Plan;
using PlanSheet = GenericBackend.DataModels.Plan.PlanSheet;

namespace GenericBackend.Repository.Sheets
{
    public class PlanSheetRepository : MongoRepository<PlanSheet>, IMongoRepository<PlanSheet>
    {
    }
}

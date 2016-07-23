using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GenericBackend.Core;
using GenericBackend.DataModels.Actual;

namespace GenericBackend.DataModels.Plan
{
    public class PlanSheetItem : MongoEntityBase
    {
        public string Subject { get; set; }
        public string FirstUknknown { get; set; }
        public string SecondUknknown { get; set; }
        public string ThirdUknknown { get; set; }
        public string Nis { get; set; }
        public string CummulativePActualEachMonth { get; set; }
        public string CummulativePlan { get; set; }
        public string Diff { get; set; }
        public List<PlanTimelineData> TimelineData { get; set; }
    }
}

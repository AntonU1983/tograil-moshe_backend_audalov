using System;
using GenericBackend.Core;

namespace GenericBackend.DataModels.Plan
{
    public class PlanTimelineData : MongoEntityBase
    {
        public string DateTime { get; set; }
        public string Plan { get; set; }
        public string AccumulatedPlan { get; set; }
        public string SupervisorComments { get; set; }
    }
}

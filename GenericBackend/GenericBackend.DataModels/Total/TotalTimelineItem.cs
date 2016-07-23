using System;
using GenericBackend.Core;

namespace GenericBackend.DataModels.Total
{
    public class TotalTimelineItem : MongoEntityBase
    {
        public DateTime DateTime { get; set; }
        public long AccumulatePlan { get; set; }
        public long AccumulateActual { get; set; }
        public long AccumulateContractorSubmit { get; set; }
        public long AccumulateUpdateActual { get; set; }
    }
}

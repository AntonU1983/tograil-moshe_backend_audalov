using System;
using GenericBackend.Core;

namespace GenericBackend.DataModels.Actual
{
    public class ActualTimelineData : MongoEntityBase
    {
        public DateTime DateTime { get; set; }
        public decimal Actual { get; set; }
        public decimal UpdateActual { get; set; }
        public decimal AccumulatedActual { get; set; }
        public decimal AccumulatedUpdate { get; set; }
        public string SupervisorComments { get; set; }
    }
}

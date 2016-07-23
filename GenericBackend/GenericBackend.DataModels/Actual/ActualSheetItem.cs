using System.Collections.Generic;
using GenericBackend.Core;

namespace GenericBackend.DataModels.Actual
{
    public class ActualSheetItem : MongoEntityBase
    {
        public string Subject { get; set; }
        public long FirstUknknown { get; set; }
        public long SecondUknknown { get; set; }
        public long ThirdUknknown { get; set; }
        public long FirstNis { get; set; }
        public long SecondNis { get; set; }
        public long Diff { get; set; }
        public List<ActualTimelineData> TimelineData { get; set; }
    }
}

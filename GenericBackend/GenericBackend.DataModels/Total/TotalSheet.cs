using System.Collections.Generic;
using GenericBackend.Core;

namespace GenericBackend.DataModels.Total
{
    public class TotalSheet : MongoEntityBase
    {
        public string Name { get; set; }
        public List<TotalTimelineItem> TimelineData { get; set; }
    }
}

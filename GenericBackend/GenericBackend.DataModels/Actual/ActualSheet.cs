using System.Collections.Generic;
using GenericBackend.Core;

namespace GenericBackend.DataModels.Actual
{
    public class ActualSheet : MongoEntityBase
    {
        public string Name { get; set; }
        public List<ActualSheetItem> ActualItems { get; set; }
    }
}

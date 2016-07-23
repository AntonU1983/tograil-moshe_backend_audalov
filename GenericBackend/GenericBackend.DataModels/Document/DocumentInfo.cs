using System;
using GenericBackend.Core;

namespace GenericBackend.DataModels.Document
{
    public class DocumentInfo : MongoEntityBase
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public DateTime DateOfPost { get; set; }
        public string User { get; set; }

    }
}

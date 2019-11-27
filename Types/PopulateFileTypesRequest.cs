using System.Runtime.Serialization;

namespace FIleAPI_CLI
{
    [DataContract]
    public class PopulateFileTypesRequest
    {
        [DataMember(Name = "header")]
        public RequestHeader Header { get; set; }
        [DataMember(Name = "payload")]
        public PopulateFileTypesRequestPayload Payload { get; set; }
    }

    [DataContract]
    public class PopulateFileTypesRequestPayload
    {
        [DataMember(Name = "userId")]
        public string UserID { get; set; }
    }
}

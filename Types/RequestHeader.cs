using System.Runtime.Serialization;

namespace FIleAPI_CLI
{
    [DataContract]
    public class RequestHeader
    {
        [DataMember(Name = "ID")]
        public string ID { get; set; }
        [DataMember(Name = "application")]
        public string Application { get; set; }
        [DataMember(Name = "channel")]
        public string Channel { get; set; }
    }
}

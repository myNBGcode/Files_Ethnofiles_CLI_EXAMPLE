using FIleAPI_CLI.Types;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FIleAPI_CLI
{
    [DataContract]
    public class PopulateFileTypesResponse
    {
        [DataMember(Name = "payload")]
        public PopulateFileTypeResponsePayload Payload { get; set; }
        [DataMember(Name = "exception")]
        public ResponseMessage Exception { get; set; }
        [DataMember(Name = "messages")]
        public ICollection<ResponseMessage> Messages { get; set; }
        [DataMember(Name = "executionTime")]
        public decimal ExecutionTime { get; set; }
    }

    [DataContract]
    public class PopulateFileTypeResponsePayload
    {
        [DataMember(Name = "fileTypeList")]
        public EthnofilesFileTypesResponse[] FileTypeList { get; set; }
    }
}

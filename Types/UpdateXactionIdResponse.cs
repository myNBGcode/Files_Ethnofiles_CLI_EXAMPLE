using FIleAPI_CLI.Types;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FIleAPI_CLI
{
    [DataContract]
    public class UpdateXactionIdResponse
    {
        [DataMember(Name = "payload")]
        public UpdateXactionIdResponsePayload Payload { get; set; }
        [DataMember(Name = "exception")]
        public ResponseMessage Exception { get; set; }
        [DataMember(Name = "messages")]
        public ICollection<ResponseMessage> Messages { get; set; }
        [DataMember(Name = "executionTime")]
        public decimal ExecutionTime { get; set; }

    }

    [DataContract]
    public class UpdateXactionIdResponsePayload
    {
        [DataMember(Name = "inboxId")]
        public string InboxId { get; set; }

        [DataMember(Name = "xactionId")]
        public string XactionId { get; set; }

        [DataMember(Name = "isDeferred")]
        public bool IsDeferred { get; set; }

        [DataMember(Name = "tanCheck")]
        public string TanCheck { get; set; }

        [DataMember(Name = "transactionDate")]
        public DateTime TransactionDate { get; set; }

    }
}

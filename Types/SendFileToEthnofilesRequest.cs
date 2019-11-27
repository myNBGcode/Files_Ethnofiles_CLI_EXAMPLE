using System;
using System.Runtime.Serialization;

namespace FIleAPI_CLI
{
    /// <summary>
    /// Sends a file from the file api to ethnofiles
    /// </summary>
    [DataContract]
    public class SendFileToEthnofilesRequest
    {
        [DataMember(Name = "header")]
        public RequestHeader Header { get; set; }
        [DataMember(Name = "payload")]
        public SendFileToEthnofilesRequestPayload Payload { get; set; }
    }

    [DataContract]
    public class SendFileToEthnofilesRequestPayload
    {
        [DataMember(Name = "fileApiFileId")]
        public Guid FileApiFileId { get; set; }
        [DataMember(Name = "userId")]
        public string UserID { get; set; }

        [DataMember(Name = "fileTypeId")]
        public string FileTypeId { get; set; }

        [DataMember(Name = "rowCount")]
        public int? RowCount { get; set; }

        [DataMember(Name = "totalSum")]
        public double? TotalSum { get; set; }

        [DataMember(Name = "tanNumber")]
        public string TanNumber { get; set; }

        [DataMember(Name = "locale")]
        public string Locale { get; set; }

        [DataMember(Name = "inboxId")]
        public string InboxId { get; set; }

        [DataMember(Name = "xactionId")]
        public string XactionId { get; set; }

        [DataMember(Name = "isSmsOtp")]
        public bool? IsSmsOtp { get; set; }

        [DataMember(Name = "convId")]
        public string ConvId { get; set; }

        [DataMember(Name = "xmlFileField")]
        public bool? XmlFileField { get; set; }

        [DataMember(Name = "debtorName")]
        public string DebtorName { get; set; }

        [DataMember(Name = "debtorIBAN")]
        public string DebtorIBAN { get; set; }

        [DataMember(Name = "fileId")]
        public string FileId { get; set; }

        [DataMember(Name = "acceptTerms")]
        public bool? AcceptTerms { get; set; }

        [DataMember(Name = "acceptTrnTerms")]
        public bool? AcceptTrnTerms { get; set; }
    }
}

using System.Runtime.Serialization;

namespace FIleAPI_CLI
{
    [DataContract]
    public class EthnofilesFileTypesResponse
    {
        [DataMember(Name = "descriptionField")]
        public string DescriptionField { get; set; }

        [DataMember(Name = "filenamePatternField")]
        public string FilenamePatternField { get; set; }

        [DataMember(Name = "idField")]
        public string IdField { get; set; }

        [DataMember(Name = "recallXMLFileField")]
        public bool RecallXMLFileField { get; set; }

        [DataMember(Name = "validationTypeField")]
        public string ValidationTypeField { get; set; }

        [DataMember(Name = "validationTypeFieldSpecified")]
        public bool ValidationTypeFieldSpecified { get; set; }

        [DataMember(Name = "xmlFileField")]
        public bool XmlFileField { get; set; }

        [DataMember(Name = "convId")]
        public string ConvId { get; set; }

        /// <summary>
        /// Flag to include row count as int, in the send to ethnofiles request
        /// </summary>
        /// <returns></returns>
        public bool SendRowNum()
        {
            return this.ValidationTypeField != null && this.ValidationTypeField != "none";
        }

        /// <summary>
        /// Flag to include total sum as float, in the send to ethnofiles request
        /// </summary>
        /// <returns></returns>
        public bool SendTotalSum()
        {
            return this.ValidationTypeField == "countAndSum";
        }

        /// <summary>
        /// Flag to include debtor iban as string, in the send to ethnofiles request
        /// </summary>
        /// <returns></returns>
        public bool HasAccountInput()
        {
            return this.ConvId != null && this.ConvId.Length > 0 && (this.ConvId.Contains("Payroll") || this.ConvId.Contains("SPH"));
        }
    }
}

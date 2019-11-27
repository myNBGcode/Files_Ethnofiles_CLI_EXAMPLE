using System;
using System.Runtime.Serialization;

namespace FIleAPI_CLI
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class AuditEntity
    {
        /// <summary>
        /// user id of the creator
        /// </summary>
        /// <value>user id of the creator</value>
        [DataMember(Name = "createdByUserId")]
        public string CreatedByUserId { get; set; }

        /// <summary>
        /// user registry of the creator
        /// </summary>
        /// <value>user registry of the creator</value>
        [DataMember(Name = "createdByUserRegistry")]
        public string CreatedByUserRegistry { get; set; }

        /// <summary>
        /// user id of the person tho made the update
        /// </summary>
        /// <value>user id of the person tho made the update</value>
        [DataMember(Name = "updatedByUserId")]
        public string UpdatedByUserId { get; set; }

        /// <summary>
        /// user registry of the person tho made the update
        /// </summary>
        /// <value>user registry of the person tho made the update</value>
        [DataMember(Name = "updatedByUserRegistry")]
        public string UpdatedByUserRegistry { get; set; }

        /// <summary>
        /// Folder creation timestamp (Format yyyy-MM-ddTHH:mm:ss.fffZ)
        /// </summary>
        /// <value>Folder creation timestamp (Format yyyy-MM-ddTHH:mm:ss.fffZ)</value>
        [DataMember(Name = "createdOn")]
        public DateTime? CreatedOn { get; set; }

        /// <summary>
        /// Folder updated timestamp (Format yyyy-MM-ddTHH:mm:ss.fffZ)
        /// </summary>
        /// <value>Folder updated timestamp (Format yyyy-MM-ddTHH:mm:ss.fffZ)</value>
        [DataMember(Name = "updatedOn")]
        public DateTime? UpdatedOn { get; set; }
    }
}

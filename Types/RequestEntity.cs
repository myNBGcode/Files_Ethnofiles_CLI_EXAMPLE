using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace FIleAPI_CLI
{
    /// <summary>
    /// Request entity details e.g. id and registry
    /// </summary>
    [DataContract]
    public class RequestEntity 
    {
        /// <summary>
        /// Id e.g. IBank User Identifier
        /// </summary>
        /// <value>Id e.g. IBank User Identifier</value>
        [Required]
        [DataMember(Name = "id")]
        public string Id { get; set; }

        /// <summary>
        /// Registry e.g. IBank
        /// </summary>
        /// <value>Registry e.g. IBank</value>
        [Required]
        [DataMember(Name = "registry")]
        public string Registry { get; set; }
    }
}

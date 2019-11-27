using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace FIleAPI_CLI
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class BaseRequest
    {
        /// <summary>
        /// Gets or Sets Requester
        /// </summary>
        [Required]
        [DataMember(Name = "requester")]
        public RequestEntity Requester { get; set; }

        /// <summary>
        /// Gets or Sets Subject
        /// </summary>
        [Required]
        [DataMember(Name = "subject")]
        public RequestEntity Subject { get; set; }
    }
}

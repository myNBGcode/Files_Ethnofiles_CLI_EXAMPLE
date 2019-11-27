using System.Runtime.Serialization;

namespace FIleAPI_CLI
{
    /// <summary>
    /// File Content
    /// </summary>
    [DataContract]
    public class FileContent
    {
        /// <summary>
        /// Gets or Sets _FileContent
        /// </summary>
        [DataMember(Name = "fileContent")]
        public byte[] _FileContent { get; set; }
    }
}

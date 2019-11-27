using System;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace FIleAPI_CLI
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class UploadRequest : BaseRequest
    {
        /// <summary>
        /// File content
        /// </summary>
        /// <value>File content</value>
        [Required]
        [DataMember(Name = "fileContent")]        
        public byte[] FileContent { get; set; }

        /// <summary>
        /// File Chunk part
        /// </summary>
        /// <value>File Chunk part</value>
        [Required]
        [DataMember(Name = "chunkPart")]
        public int ChunkPart { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FIleAPI_CLI
{
    /// <summary>
    /// Uploaded file information
    /// </summary>
    [DataContract]
    public class FileDetails : AuditEntity
    {
        /// <summary>
        /// File identifier
        /// </summary>
        /// <value>File identifier</value>
        [DataMember(Name = "fileId")]
        public Guid? FileId { get; set; }

        /// <summary>
        /// File name
        /// </summary>
        /// <value>File name</value>
        [DataMember(Name = "fileName")]
        public string FileName { get; set; }

        /// <summary>
        /// Uploaded file description
        /// </summary>
        /// <value>Uploaded file description</value>
        [DataMember(Name = "fileDescription")]
        public string FileDescription { get; set; }

        /// <summary>
        /// File size in MB
        /// </summary>
        /// <value>File size in MB</value>
        [DataMember(Name = "fileSize")]
        public long FileSize { get; set; }

        /// <summary>
        /// Uploaded Chunks
        /// </summary>
        /// <value>File size in MB</value>
        [DataMember(Name = "chunksUploaded")]
        public int ChunksUploaded { get; set; }

        /// <summary>
        /// Total chunks
        /// </summary>
        /// <value>Total chunks</value>
        [DataMember(Name = "totalChunks")]
        public int TotalChunks { get; set; }

        /// <summary>
        /// User custom tag, used like a hashtag
        /// </summary>
        /// <value>User custom tag, used like a hashtag</value>
        [DataMember(Name = "userTags")]
        public List<string> UserTags { get; set; }

        /// <summary>
        /// System defined tag e.g. for ethnofiles
        /// </summary>
        /// <value>System defined tag e.g. for ethnofiles</value>
        [DataMember(Name = "systemTags")]
        public List<string> SystemTags { get; set; }

        /// <summary>
        /// File status
        /// </summary>
        /// <value>File status</value>
        [DataMember(Name = "status")]
        public FileStatusEnum? Status { get; set; }

        /// <summary>
        /// Status description
        /// </summary>
        /// <value>Status description</value>
        [DataMember(Name = "statusReason")]
        public string StatusReason { get; set; }

        /// <summary>
        /// File Custom metadata
        /// </summary>
        /// <value>File Custom metadata</value>
        [DataMember(Name = "metadata")]
        public string Metadata { get; set; }

        /// <summary>
        /// File image
        /// </summary>
        /// <value>File image</value>
        [DataMember(Name = "fileIcon")]
        public byte[] FileIcon { get; set; }
}
}

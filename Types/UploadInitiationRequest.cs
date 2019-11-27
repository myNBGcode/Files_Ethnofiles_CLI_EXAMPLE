using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace FIleAPI_CLI
{
    /// <summary>
    /// 
    /// </summary>
    [DataContract]
    public class UploadInitiationRequest : BaseRequest
    {
        /// <summary>
        /// Folder identifier
        /// </summary>
        /// <value>Folder identifier</value>
        [DataMember(Name = "folderId")]
        public Guid? FolderId { get; set; }

        /// <summary>
        /// File name
        /// </summary>
        /// <value>File name</value>
        [Required]
        [DataMember(Name = "fileName")]
        public string FileName { get; set; }

        /// <summary>
        /// File description
        /// </summary>
        /// <value>File description</value>
        [DataMember(Name = "fileDescription")]
        public string FileDescription { get; set; }

        /// <summary>
        /// User custom tag e.g. hashtag
        /// </summary>
        /// <value>User custom tag e.g. hashtag</value>
        [DataMember(Name = "userTags")]
        public List<string> UserTags { get; set; }

        /// <summary>
        /// File size in MB
        /// </summary>
        /// <value>File size in MB</value>
        [Required]
        [DataMember(Name = "fileSize")]
        public long FileSize { get; set; }

        /// <summary>
        /// File custom metadata
        /// </summary>
        /// <value>File custom metadata</value>
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

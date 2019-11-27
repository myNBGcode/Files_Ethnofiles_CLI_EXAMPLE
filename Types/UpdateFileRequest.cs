using System;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace FIleAPI_CLI
{
    /// <summary>
    /// Update file details
    /// </summary>
    [DataContract]
    public class UpdateFileRequest : BaseRequest
    {
        /// <summary>
        /// File name
        /// </summary>
        /// <value>File name</value>
        [DataMember(Name = "fileName")]
        public string FileName { get; set; }

        /// <summary>
        /// User custom tag, used like a hashtag
        /// </summary>
        /// <value>User custom tag, used like a hashtag</value>
        [DataMember(Name = "userTags")]
        public List<string> UserTags { get; set; }

        /// <summary>
        /// File description
        /// </summary>
        /// <value>File description</value>
        [DataMember(Name = "fileDescription")]
        public string FileDescription { get; set; }

        /// <summary>
        /// File image
        /// </summary>
        /// <value>File image</value>
        [DataMember(Name = "fileIcon")]
        public byte[] FileIcon { get; set; }

        /// <summary>
        /// folderId can be used in order to move the specific file inside the folder with id folderId
        /// </summary>
        /// <value>folderId can be used in order to move the specific file inside the folder with id folderId</value>
        [DataMember(Name = "folderId")]
        public Guid? FolderId { get; set; }

        /// <summary>
        /// True  : If folderId is null set it null
        /// False : If folderId is null ignore it
        /// </summary>
        [DataMember(Name = "useFolderId")]
        public bool UseFolderId { get; set; }
    }
}

using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace FIleAPI_CLI
{
    /// <summary>
    /// File Status
    /// </summary>
    /// <value>File Status</value>
    [JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
    public enum FileStatusEnum
    {        
        [EnumMember(Value = "WaitingForUpload")]
        WaitingForUpload = 0,
        
        [EnumMember(Value = "WaitingForMerge")]
        WaitingForMerge = 1,
        
        [EnumMember(Value = "MergingChunks")]
        MergingChunks = 2,
        
        [EnumMember(Value = "Completed")]
        Completed = 3,
        
        [EnumMember(Value = "Deleted")]
        Deleted = 4,
        
        [EnumMember(Value = "Immutable")]
        Immutable = 5
    }
}

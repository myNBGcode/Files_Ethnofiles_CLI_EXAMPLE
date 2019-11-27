using Newtonsoft.Json;

namespace FIleAPI_CLI
{
    public class UploadInitiationResponse
    {
        [JsonProperty("fileId")]
        public string FileId { get; set; }
        [JsonProperty("fileChunks")]
        public int FileChunks { get; set; }
        [JsonProperty("fileChunkSize")]
        public int FileChunkSize { get; set; }
    }
}

using RestSharp;
using System;

namespace FIleAPI_CLI
{
    public interface IFileApiRestRequest
    {
        /// <summary>
        /// POST : files/upload-initiation
        /// </summary>
        /// <param name="request">body</param>
        UploadInitiationResponse InitiateUpload(UploadInitiationRequest request);
        /// <summary>
        /// PUT : files/{fileId}/upload
        /// </summary>
        /// <param name="request">body</param>
        /// <param name="fileId">route</param>
        IRestResponse UploadChunk(UploadRequest request, Guid? fileId);
        /// <summary>
        /// GET : files/{fileId}
        /// </summary>
        /// <param name="requester">header</param>
        /// <param name="subject">header</param>
        /// <param name="fileId">route</param>
        FileDetails GetFile(string requester, string subject, Guid? fileId);
        /// <summary>
        /// GET : files/{fileId}/{chunkPart}
        /// </summary>
        /// <param name="chunkPart">route</param>
        /// <param name="requester">header</param>
        /// <param name="subject">header</param>
        /// <param name="fileId">route</param>
        FileContent DownloadFile(int chunkPart, string requester, string subject, Guid? fileId);
        /// <summary>
        /// PUT : files/{fileId}
        /// </summary>
        /// <param name="request"></param>
        /// <param name="fileId"></param>
        /// <returns></returns>
        IRestResponse UpdateFile(UpdateFileRequest request, Guid? fileId);
        /// <summary>
        /// POST : /populatefiletypes
        /// Call to ethnofiles api.
        /// </summary>
        /// <param name="populateFileTyesRequest"></param>
        /// <returns></returns>
        PopulateFileTypesResponse PopulateFileTypes(PopulateFileTypesRequest populateFileTyesRequest);
        /// <summary>
        /// POST : /sendfiletoethnofiles
        /// Call to ethnofiles api.
        /// </summary>
        /// <param name="sendFileToEthnofilesRequest"></param>
        /// <returns></returns>
        UpdateXactionIdResponse SendFileToEthnofiles(SendFileToEthnofilesRequest sendFileToEthnofilesRequest);
    }
}

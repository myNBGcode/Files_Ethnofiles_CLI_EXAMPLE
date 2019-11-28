using FIleAPI_CLI.Types;
using RestSharp;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace FIleAPI_CLI
{
    public class CliService
    {
        public static void UploadFile(IFileApiRestRequest fileApiRestRequest, UploadInitiationRequest uploadInitiationRequest, string inputFile)
        {
            // rest call to upload init
            var uploadInitiationResponse = fileApiRestRequest.InitiateUpload(uploadInitiationRequest);

            // handle upload init response object
            var chunkSize = uploadInitiationResponse.FileChunkSize * 1024 * 1024;
            var fileId = Guid.Parse(uploadInitiationResponse.FileId);
            var totalChunks = uploadInitiationResponse.FileChunks;

            Console.WriteLine($"Upload Operation Started. File Name: {uploadInitiationRequest.FileName}{Environment.NewLine}" +
                $"with File Id: {fileId.ToString()}");
            // read file from file system
            using (Stream input = File.OpenRead(inputFile))
            {
                int index = 1;
                const int BUFFER_SIZE = 20 * 1024;
                byte[] buffer = new byte[BUFFER_SIZE]; // use buffer to write to memory stream

                // before hits end of file
                while (input.Position < input.Length)
                {
                    // write a single chunk to memory stream
                    using (MemoryStream ms = new MemoryStream())
                    {
                        byte[] chunkPart = new List<byte>().ToArray();
                        int remaining = chunkSize;
                        int bytesRead;

                        // use buffer to write to memory stream until the chunk size is reached
                        while (remaining > 0 && (bytesRead = input.Read(buffer, 0, Math.Min(remaining, BUFFER_SIZE))) > 0)
                        {
                            ms.Write(buffer, 0, bytesRead);
                            remaining -= bytesRead;
                        }

                        // create upload chunk request message
                        var uploadRequest = new UploadRequest
                        {
                            Requester = uploadInitiationRequest.Requester,
                            Subject = uploadInitiationRequest.Subject,
                            ChunkPart = index,
                            FileContent = ms.ToArray()
                        };
                        UploadChunkWithRetry(fileApiRestRequest, uploadRequest, fileId, index, totalChunks, 3);
                    }
                    index++;
                }
            }
        }

        public static void DownloadFile(IFileApiRestRequest fileApiRestRequest, Guid fileId, string outputFilePath, string requester, string subject)
        {
            // Rest call to Get File
            var fileDetails = fileApiRestRequest.GetFile($"{requester}", $"{subject}", fileId);
            var fileName = fileDetails.FileName;
            if (File.Exists(outputFilePath + fileName)) fileName = "Copy_of_" + fileName;

            Console.WriteLine($"Downloading file with id: {fileId} as '{fileName}'{Environment.NewLine}");
            // open stream to append chunks to output file
            using (var stream = new FileStream($"{outputFilePath}{fileName}", FileMode.Append))
            {
                for (int chunkPart = 1; chunkPart <= fileDetails.TotalChunks; chunkPart++)
                {
                    // rest call to download file chunk part
                    var fileContent = fileApiRestRequest.DownloadFile(chunkPart, $"{requester}", $"{subject}", fileId);
                    stream.Write(fileContent._FileContent, 0, fileContent._FileContent.Length);

                    Console.Write("\rDownloading: " + (int)((decimal)chunkPart / fileDetails.TotalChunks * 100) + "%");
                }
            }
            Console.WriteLine("\rDownload Completed.");
        }

        public static void AddUserTags(IFileApiRestRequest fileApiRestRequest, Guid fileId, UpdateFileRequest request)
        {
            Console.WriteLine($"Add tags Staring file with id: {fileId} ");

            var fileDetails = fileApiRestRequest.GetFile($"{request.Requester.Registry}:{request.Requester.Id}", $"{request.Subject.Registry}:{request.Subject.Id}", fileId);
            if (fileDetails == null)
            {
                Console.WriteLine($"\nAddUserTags: File Not Found with id {fileId}");
                return;
            }

            request.FileName = fileDetails.FileName;
            if (fileDetails.UserTags != null && fileDetails.UserTags.Any())
                request.UserTags.AddRange(fileDetails.UserTags);

            // update file
            fileApiRestRequest.UpdateFile(request, fileId);
            Console.WriteLine("\nAddUserTags Completed.");
        }

        public static void RemoveUserTags(IFileApiRestRequest fileApiRestRequest, Guid fileId, UpdateFileRequest request, List<string> userTagsToRemove)
        {
            Console.WriteLine($"Remove tags Staring file with id: {fileId} ");

            var fileDetails = fileApiRestRequest.GetFile($"{request.Requester.Registry}:{request.Requester.Id}", $"{request.Subject.Registry}:{request.Subject.Id}", fileId);
            if (fileDetails == null)
            {
                Console.WriteLine($"\nRemoveUserTags: File Not Found with id {fileId}");
                return;
            }

            request.FileName = fileDetails.FileName;
            request.UseFolderId = false;
            if (fileDetails.UserTags != null && fileDetails.UserTags.Any())
            {
                foreach (string userTag in userTagsToRemove)
                    fileDetails.UserTags.Remove(userTag);

                request.UserTags = new List<string>();
                request.UserTags.AddRange(fileDetails.UserTags);

                // update file
                if (!request.UserTags.Any())
                {
                    Console.WriteLine("\nRemoveUserTags: You cannot remove all user tags from the file.");
                    return;
                }

                fileApiRestRequest.UpdateFile(request, fileId);
                Console.WriteLine("\nRemoveUserTags Completed.");
            }
        }

        public static void SendToEthnofiles(IFileApiRestRequest fileApiRestRequest, SendFileToEthnofilesRequest sendFileToEthnofilesRequest)
        {
            Console.WriteLine($"send to ethnofilese with id: {sendFileToEthnofilesRequest.Payload.FileApiFileId} ");

            var updateXactionIdResponse =  fileApiRestRequest.SendFileToEthnofiles(sendFileToEthnofilesRequest);
            if (updateXactionIdResponse.Exception != null)
            {
                Console.Write("\nSendFileToEthnofiles returned Exception : Code - " + updateXactionIdResponse.Exception.Code + " , Description - " + updateXactionIdResponse.Exception.Description);
                return;
            }
            else if (updateXactionIdResponse.Payload == null)
            {
                Console.Write("\nSendFileToEthnofiles response Payload is empty");
                return;
            }

            if (updateXactionIdResponse.Payload.IsDeferred)
                Console.Write("\nSuccesfully send file with id " + sendFileToEthnofilesRequest.Payload.FileApiFileId + " for Approval. TransactionDate: " + updateXactionIdResponse.Payload.TransactionDate);
            else
                Console.Write("\nSuccesfully send file with id " + sendFileToEthnofilesRequest.Payload.FileApiFileId + " to ethnofiles. TransactionDate: " + updateXactionIdResponse.Payload.TransactionDate + ", XactionId: "+ updateXactionIdResponse.Payload.XactionId + ", InboxId: " + updateXactionIdResponse.Payload.InboxId);
        }

        internal static EthnofilesFileTypesResponse PopulateFileTypes(IFileApiRestRequest fileApiRestRequest, PopulateFileTypesRequest populateFileTyesRequest, string idField)
        {

            var populateFileTypesResponse = fileApiRestRequest.PopulateFileTypes(populateFileTyesRequest);

            if (populateFileTypesResponse.Exception != null)
            {
                Console.Write("\nPopulateFileTypes returned Exception : Code - " + populateFileTypesResponse.Exception.Code + " , Description - " + populateFileTypesResponse.Exception.Description);
                return null;
            }
            else if (populateFileTypesResponse.Payload == null)
            {
                Console.Write("\nFileType not found, with id: " + idField);
                return null;
            }

            var fileType = populateFileTypesResponse.Payload.FileTypeList.Where(p => p.IdField == idField).FirstOrDefault();
            if (fileType == null)
                Console.Write("\nFileType not found, with id: " + idField);
            else
                Console.Write("\nFileType with id:" + idField + " , is selected from the following file type ids list: [" + String.Join(',', populateFileTypesResponse.Payload.FileTypeList.Select(p => p.IdField)) + "]");

            return fileType;

        }

        private static void UploadChunkWithRetry(IFileApiRestRequest fileApiRestRequest, UploadRequest uploadRequest, Guid fileId, int index, int totalChunks, int retryAttempts)
        {
            var tries = 0;
            do
            {
                // rest call to upload chunk 
                IRestResponse response = fileApiRestRequest.UploadChunk(uploadRequest, fileId);
                Console.Write("\rChunk #" + (index) + "/" + totalChunks + ". Response: " + response.Content + " " + (int)response.StatusCode + ", " + response.StatusCode);
                Console.Write("\nUploading: " + (int)((decimal)index / totalChunks * 100) + "%");

                if ((int)response.StatusCode == 308 || (int)response.StatusCode == 200) break;

                tries++;
                Console.WriteLine("Error uploading Chunk #" + index + ". Retry attempt:" + tries + "/" + retryAttempts + ".");

            } while (tries < retryAttempts);
        }

        

    }
}
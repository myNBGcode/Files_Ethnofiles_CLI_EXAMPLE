using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using static FIleAPI_CLI.ConfigurationSingleton;

namespace FIleAPI_CLI
{
    public class FileApiRestRequest : IFileApiRestRequest
    {
        IRestResponse restResponse { get; set; }

        public UploadInitiationResponse InitiateUpload(UploadInitiationRequest request)
        {
            string path = $"{Settings.configuration["proxyUrl"]}/files/upload-initiation";
            var jsonBody = JsonConvert.SerializeObject(request);
            restResponse = HttpRequestClient.ExecuteRestPost(path, jsonBody, HeadersHelper.GetHeaders(Settings.configuration["client_id"], Guid.NewGuid().ToString(), GetAccessToken(Settings.configuration["scope"]), jsonBody.Length, Settings.configuration["sandbox_id"]));

            if (!restResponse.IsSuccessful && restResponse.StatusCode != System.Net.HttpStatusCode.Created)
                throw new Exception(string.IsNullOrEmpty(restResponse.Content) ? restResponse.ErrorMessage : restResponse.Content);

            return JsonConvert.DeserializeObject<UploadInitiationResponse>(JsonConvert.DeserializeObject(restResponse.Content).ToString());
        }

        public IRestResponse UploadChunk(UploadRequest request, Guid? fileId)
        {
            string path = $"{Settings.configuration["proxyUrl"]}/files/" + fileId + "/upload";
            var jsonBody = JsonConvert.SerializeObject(request);
            restResponse = HttpRequestClient.ExecuteRestPut(path, jsonBody, HeadersHelper.GetHeaders(Settings.configuration["client_id"], Guid.NewGuid().ToString(), GetAccessToken(Settings.configuration["scope"]), jsonBody.Length, Settings.configuration["sandbox_id"]));

            return restResponse;
        }

        public FileDetails GetFile(string requester, string subject, Guid? fileId)
        {
            string path = $"{Settings.configuration["proxyUrl"]}/files/" + fileId.ToString();
            restResponse = HttpRequestClient.ExecuteRestGet(path, HeadersHelper.GetHeaders(Settings.configuration["client_id"], Guid.NewGuid().ToString(), GetAccessToken(Settings.configuration["scope"]), requester, subject, Settings.configuration["sandbox_id"]));

            if (!restResponse.IsSuccessful)
                throw new Exception(string.IsNullOrEmpty(restResponse.Content) ? restResponse.ErrorMessage : restResponse.Content);

            return JsonConvert.DeserializeObject<FileDetails>(JsonConvert.DeserializeObject(restResponse.Content).ToString());
        }

        public FileContent DownloadFile(int chunkPart, string requester, string subject, Guid? fileId)
        {
            string path = $"{Settings.configuration["proxyUrl"]}/files/" + fileId + "/" + chunkPart.ToString();
            restResponse = HttpRequestClient.ExecuteRestGet(path, HeadersHelper.GetHeaders(Settings.configuration["client_id"], Guid.NewGuid().ToString(), GetAccessToken(Settings.configuration["scope"]), requester, subject, Settings.configuration["sandbox_id"]));

            if (!restResponse.IsSuccessful)
                throw new Exception(string.IsNullOrEmpty(restResponse.Content) ? restResponse.ErrorMessage : restResponse.Content);

            return JsonConvert.DeserializeObject<FileContent>(JsonConvert.DeserializeObject(restResponse.Content).ToString());
        }

        public IRestResponse UpdateFile(UpdateFileRequest request, Guid? fileId)
        {
            string path = $"{Settings.configuration["proxyUrl"]}/files/" + fileId;
            var jsonBody = JsonConvert.SerializeObject(request);
            restResponse = HttpRequestClient.ExecuteRestPut(path, jsonBody, HeadersHelper.GetHeaders(Settings.configuration["client_id"], Guid.NewGuid().ToString(), GetAccessToken(Settings.configuration["scope"]), Settings.configuration["sandbox_id"]));

            if (!restResponse.IsSuccessful)
                throw new Exception(string.IsNullOrEmpty(restResponse.Content) ? restResponse.ErrorMessage : restResponse.Content);

            return restResponse;
        }

        public PopulateFileTypesResponse PopulateFileTypes(PopulateFileTypesRequest request)
        {
            Console.WriteLine($"PopulateFileTypes starting");

            string path = $"{Settings.configuration["ethnoProxyUrl"]}/populatefiletypes";
            request.Header = new RequestHeader() { ID = Guid.NewGuid().ToString() , Application = Settings.configuration["client_id"]};
            var jsonBody = JsonConvert.SerializeObject(request);
            restResponse = HttpRequestClient.ExecuteRestPost(path, jsonBody, HeadersHelper.GetHeaders(Settings.configuration["client_id"], Guid.NewGuid().ToString(), GetAccessToken(Settings.configuration["ethnoScope"]), Settings.configuration["sandbox_id"]));

            if (!restResponse.IsSuccessful)
                throw new Exception(string.IsNullOrEmpty(restResponse.Content) ? restResponse.ErrorMessage : restResponse.Content);
            
            return JsonConvert.DeserializeObject<PopulateFileTypesResponse>(JsonConvert.DeserializeObject(restResponse.Content).ToString());
        }

        public UpdateXactionIdResponse SendFileToEthnofiles(SendFileToEthnofilesRequest request)
        {
            Console.WriteLine($"SendFileToEthnofiles starting");

            string path = $"{Settings.configuration["ethnoProxyUrl"]}/sendfiletoethnofiles";
            request.Header = new RequestHeader() { ID = Guid.NewGuid().ToString(), Application = Settings.configuration["client_id"] };
            var jsonBody = JsonConvert.SerializeObject(request);
            restResponse = HttpRequestClient.ExecuteRestPost(path, jsonBody, HeadersHelper.GetHeaders(Settings.configuration["client_id"], Guid.NewGuid().ToString(), GetAccessToken(Settings.configuration["ethnoScope"]), Settings.configuration["sandbox_id"]));

            if (!restResponse.IsSuccessful)
                throw new Exception(string.IsNullOrEmpty(restResponse.Content) ? restResponse.ErrorMessage : restResponse.Content);

            return JsonConvert.DeserializeObject<UpdateXactionIdResponse>(JsonConvert.DeserializeObject(restResponse.Content).ToString());
        }

        #region Private Methods
        private string GetAccessToken(string scope)
        {
            if (!CheckAlreadyActiveToken())
            {
                string path = Settings.configuration["AuthorizationServer"] + "connect/token";
                var headers = new Dictionary<string, string> { { "content-type", "application/x-www-form-urlencoded" } };
                var parameter = new Parameter
                {
                    Name = "application/x-www-form-urlencoded",
                    Value = $"grant_type=password" +
                            $"&client_id={Settings.configuration["client_id"]}" +
                            $"&client_secret={Settings.configuration["client_secret"]}" +
                            $"&scope={scope}&username={Settings.configuration["username"]}" +
                            $"&password={Settings.configuration["password"]}" +
                            $"&acr_values={Settings.configuration["acr_values"]}",
                    Type = ParameterType.RequestBody
                };

                restResponse = HttpRequestClient.ExecuteRestPost(path, null, headers, parameter);
                if (!restResponse.IsSuccessful)
                    throw new Exception(string.IsNullOrEmpty(restResponse.Content) ? restResponse.ErrorMessage : restResponse.Content);

                TokenHelper.Token = restResponse.GetValue<string>("access_token");
                TokenHelper.TokenCreation = DateTime.Now;
            }

            return TokenHelper.Token;
        }

        private bool CheckAlreadyActiveToken()
        {
            return TokenHelper.TokenCreation != null && TokenHelper.Token.Clear() != null && DateTime.Now <= TokenHelper.TokenCreation.Value.AddSeconds(Convert.ToDouble(Settings.configuration["TokenExpirationTimeSeconds"]));
        }

        #endregion
    }
}

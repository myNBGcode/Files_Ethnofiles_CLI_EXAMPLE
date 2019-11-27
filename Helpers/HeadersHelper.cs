using System.Collections.Generic;

namespace FIleAPI_CLI
{
    public static class HeadersHelper
    {
        public static Dictionary<string, string> GetHeaders(string request_id, string token) => new Dictionary<string, string>
        {
            { "Request-id", request_id },
            { "Authorization", "Bearer " + token }
        };

        public static Dictionary<string, string> GetHeaders(string request_id, string token, string sandboxId) => new Dictionary<string, string>
        {
            { "Request-id", request_id },
            { "Authorization", "Bearer " + token },
            { "sandbox_Id", sandboxId }
        };

        public static Dictionary<string, string> GetHeaders(string client_id, string request_id, string token, int content_length, string sandboxId) => new Dictionary<string, string>
        {
            { "client-id", client_id },
            { "Request-id", request_id },
            { "Authorization", "Bearer " + token },
            { "content-length", content_length.ToString() },
            { "sandbox_Id", sandboxId }
        };

        public static Dictionary<string, string> GetHeaders(string client_id, string request_id, string token, string sandboxId) => new Dictionary<string, string>
        {
            { "client-id", client_id },
            { "Request-id", request_id },
            { "Authorization", "Bearer " + token },
            { "sandbox_Id", sandboxId }
        };

        public static Dictionary<string, string> GetHeaders(string client_id, string request_id, string token, string requester, string subject, string sandboxId) => new Dictionary<string, string>
        {
            { "client-id", client_id },
            { "Request-id", request_id },
            { "Authorization", "Bearer " + token },            
            { "Requester", requester  },
            { "Subject", subject },
            { "sandbox_Id", sandboxId }

        };
    }
}

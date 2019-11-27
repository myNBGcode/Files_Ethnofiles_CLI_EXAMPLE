namespace FIleAPI_CLI.Types
{
    public class ResponseMessage
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public ErrorSeverity Severity { get; set; }
        public ErrorCategory Category { get; set; }
    }

    public enum ErrorSeverity
    {
        Warning = 0,
        Error = 1,
        Info = 2
    }

    public enum ErrorCategory
    {
        Business = 0,
        Communication = 1,
        Technical = 2,
        Security = 3
    }
}

namespace Core.Results
{
    public class Error(string code, string message, int statusCode)
    {
        public string Code { get; set; } = code;
        public string Message { get; set; } = message;
        public int StatusCode { get; set; } = statusCode;
    }
}
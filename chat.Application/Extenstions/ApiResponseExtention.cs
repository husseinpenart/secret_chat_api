namespace secre_chat_api.chat.Application.Extenstions
{
    public class ApiResponseExtention<T>
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public int? itemLength { get; set; }
        public int StatusCode { get; set; }
        public T? Data { get; set; }
        public object? Extra { get; set; }
        public List<string> Errors { get; set; } = new List<string>();
    }
}

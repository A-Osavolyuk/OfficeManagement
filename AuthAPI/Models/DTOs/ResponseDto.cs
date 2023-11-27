namespace AuthAPI.Models.DTOs
{
    public class ResponseDto
    {
        public string Message { get; set; } = string.Empty;
        public bool IsSucceeded { get; set; } = false;
        public object? Result { get; set; }
    }
}

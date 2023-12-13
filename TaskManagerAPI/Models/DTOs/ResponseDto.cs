namespace TaskManagerAPI.Models.DTOs
{
    public class ResponseDto
    {
        public bool IsSucceeded { get; set; } = false;
        public object? Result { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}

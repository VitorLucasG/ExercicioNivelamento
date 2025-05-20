namespace Questao5.Domain.Exceptions
{
    public class ErrorResponse
    {
        public bool Success { get; set; }

        public string Title { get; set; }
        public string Message { get; set; }
    }
}

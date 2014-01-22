namespace BooksCollector.Core
{
    public class Result
    {
        public bool Success { get; set; }
        public int ErrorCode { get; set; }
        public string ErrorMessage { get; set; }
        public Book Book { get; set; }
    }
}
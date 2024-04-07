namespace Trendy_BackEnd.Models
{
    public class ResponseResult
    {
        public string Status { get; set; }//Success or Fail
        public string? Message { get; set; }//Message of response
        public object? Content { get; set; }//Result Content
    }
}

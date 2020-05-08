namespace Hydrospot.Core.Domain.Dtos
{
    public class ResponseDto
    {
        public StatusCodeEnum StatusCode { get; set; }
        public long? Id { get; set; }
        public string Message { get; set; }
    }
}

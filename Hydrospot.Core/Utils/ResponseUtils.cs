using Hydrospot.Core.Domain;
using Hydrospot.Core.Domain.Dtos;

namespace Hydrospot.Core.Utils
{
    public static class ResponseUtils
    {
        public static ResponseDto CreateResponseDto(
            StatusCodeEnum statusCode,
            long? id = null,
            string message = null)
        {
            return new ResponseDto
            {
                StatusCode = statusCode,
                Message = message,
                Id = id
            };
        }
    }
}

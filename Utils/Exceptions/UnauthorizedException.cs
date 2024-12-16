
using System.Net;

namespace PublicInquiriesAPI.Utils.Exceptions
{
    public class UnauthorizedException : CustomException
    {
        public UnauthorizedException(string message)
            : base(message, HttpStatusCode.Unauthorized)
        {
        }
    }
}

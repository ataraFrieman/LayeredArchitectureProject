
using System.Net;

namespace PublicInquiriesAPI.Utils.Exceptions
{
    public class ValidationException : CustomException
    {
        public ValidationException(string message)
            : base(message, HttpStatusCode.UnprocessableEntity)
        {
        }
    }
}

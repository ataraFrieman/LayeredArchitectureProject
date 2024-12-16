
using System.Net;

namespace PublicInquiriesAPI.Utils.Exceptions
{
    public class NotFoundException : CustomException
    {
        public NotFoundException(string message)
            : base(message, HttpStatusCode.NotFound)
        {
        }
    }
}

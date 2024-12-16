using PublicInquiriesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicInquiriesAPI.Services.Interfaces
{
    public interface IInquiryService
    {
        Task<List<Inquiry>> GetAllInquiriesAsync();
        Task<Inquiry> GetInquiryByIdAsync(int id);
        Task CreateInquiryAsync(Inquiry inquiry);
        Task DeleteInquiryAsync(int id);
    }
}

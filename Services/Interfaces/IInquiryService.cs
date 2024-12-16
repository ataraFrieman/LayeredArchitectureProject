using PublicInquiriesAPI.Models;

namespace PublicInquiriesAPI.Services.Interfaces
{
    public interface IInquiryService
    {
        Task<List<Inquiry>> GetAllInquiriesAsync();
        Task<Inquiry> GetInquiryByIdAsync(int id);
        Task CreateInquiryAsync(Inquiry inquiry, List<IFormFile> attachments);
        Task DeleteInquiryAsync(int id);
    }
}

using PublicInquiriesAPI.Models;

namespace PublicInquiriesAPI.Repositories.Interfaces
{
    public interface IInquiryRepository
    {
        Task<List<Inquiry>> GetAllAsync();
        Task<Inquiry> GetByIdAsync(int id);
        Task CreateAsync(Inquiry inquiry);
        Task DeleteAsync(int id);
    }
}

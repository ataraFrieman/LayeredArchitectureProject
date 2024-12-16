using PublicInquiriesAPI.Models;


namespace PublicInquiriesAPI.Repositories.Interfaces
{
    public interface IAttachmentRepository
    {
        Task<List<Attachment>> GetAttachmentsByInquiryIdAsync(int inquiryId);
        Task<Attachment> GetByIdAsync(int id);
        Task CreateAsync(Attachment attachment);
        Task DeleteAsync(int id);
        Task DeleteByInquiryIdAsync(int inquiryId);

    }
}

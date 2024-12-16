using PublicInquiriesAPI.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicInquiriesAPI.Repositories.Interfaces
{
    public interface IAttachmentRepository
    {
        Task<List<Attachment>> GetAttachmentsByInquiryIdAsync(int inquiryId);
        Task<Attachment> GetByIdAsync(int id);
        Task CreateAsync(Attachment attachment);
        Task DeleteAsync(int id);
    }
}

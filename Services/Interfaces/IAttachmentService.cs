using Microsoft.AspNetCore.Http;
using PublicInquiriesAPI.Models;

namespace PublicInquiriesAPI.Services.Interfaces
{
    public interface IAttachmentService
    {
        Task<List<Attachment>> GetAttachmentsByInquiryIdAsync(int inquiryId);
        Task<Attachment> UploadAttachmentAsync(IFormFile file, int inquiryId);
        Task DeleteAttachmentAsync(int id);
        Task DeleteAttachmentsByInquiryIdAsync(int inquiryId);
    }
}

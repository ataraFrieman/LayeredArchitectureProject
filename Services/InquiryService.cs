using Microsoft.AspNetCore.Http;
using PublicInquiriesAPI.Models;
using PublicInquiriesAPI.Repositories.Interfaces;
using PublicInquiriesAPI.Services.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublicInquiriesAPI.Services
{
    public class InquiryService : IInquiryService
    {
        private readonly IInquiryRepository _inquiryRepository;
        private readonly IAttachmentService _attachmentService;

        public InquiryService(IInquiryRepository inquiryRepository, IAttachmentService attachmentService)
        {
            _inquiryRepository = inquiryRepository;
            _attachmentService = attachmentService;
        }

       public async Task<List<Inquiry>> GetAllInquiriesAsync()
        {
           List<Inquiry> inquiries = context.Inquiries
                .Include(i => i.Attachments) 
                .ToListAsync();

            if (inquiries == null || inquiries.Count == 0)
            {
                throw new NotFoundException("No inquiries found.");
            }
            return inquiries;
        }

        public async Task<Inquiry> GetInquiryByIdAsync(int id)
        {
            Inquiry inquiry = await _inquiryRepository.GetByIdAsync(id);

            if (inquiry == null)
                throw new KeyNotFoundException($"Inquiry with ID {id} not found.");

            return inquiry;
        }

        public async Task CreateInquiryAsync(Inquiry inquiry, List<IFormFile> attachments)
        {
            await _inquiryRepository.CreateAsync(inquiry);

            if (attachments != null && attachments.Count > 0)
            {
                foreach (var file in attachments)
                {
                    await _attachmentService.UploadAttachmentAsync(file, inquiry.Id);
                }
            }
        }

        public async Task DeleteInquiryAsync(int id)
        {
            await _attachmentService.DeleteAttachmentsByInquiryIdAsync(id);
            await _inquiryRepository.DeleteAsync(id);
        }
    }
}

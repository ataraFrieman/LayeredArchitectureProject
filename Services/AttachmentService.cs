using Microsoft.AspNetCore.Http;
using PublicInquiriesAPI.Models;
using PublicInquiriesAPI.Repositories.Interfaces;
using PublicInquiriesAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace PublicInquiriesAPI.Services
{
    public class AttachmentService : IAttachmentService
    {
        private readonly IAttachmentRepository _attachmentRepository;
        private readonly string _storagePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "attachments");

        public AttachmentService(IAttachmentRepository attachmentRepository)
        {
            _attachmentRepository = attachmentRepository;

            if (!Directory.Exists(_storagePath))
            {
                Directory.CreateDirectory(_storagePath);
            }
        }

        public async Task<List<Attachment>> GetAttachmentsByInquiryIdAsync(int inquiryId)
        {
            return await _attachmentRepository.GetAttachmentsByInquiryIdAsync(inquiryId);
        }

        public async Task<Attachment> UploadAttachmentAsync(IFormFile file, int inquiryId)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("Invalid file.");

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            var filePath = Path.Combine(_storagePath, fileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var attachment = new Attachment
            {
                FileName = file.FileName,
                FilePath = filePath,
                ContentType = file.ContentType,
                FileSize = file.Length,
                InquiryId = inquiryId
            };

            await _attachmentRepository.CreateAsync(attachment);

            return attachment;
        }

        public async Task DeleteAttachmentsByInquiryIdAsync(int inquiryId)
        {
            var attachments = await _attachmentRepository.GetAttachmentsByInquiryIdAsync(inquiryId);
            foreach (var attachment in attachments)
            {
                if (File.Exists(attachment.FilePath))
                {
                    File.Delete(attachment.FilePath);
                }
            }

            await _attachmentRepository.DeleteByInquiryIdAsync(inquiryId);
        }
    }
}

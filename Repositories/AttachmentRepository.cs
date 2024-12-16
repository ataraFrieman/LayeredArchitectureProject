using Microsoft.EntityFrameworkCore;
using PublicInquiriesAPI.Data;
using PublicInquiriesAPI.Models;
using PublicInquiriesAPI.Repositories.Interfaces;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PublicInquiriesAPI.Repositories
{
    public class AttachmentRepository : IAttachmentRepository
    {
        private readonly AppDbContext _context;

        public AttachmentRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Attachment>> GetAttachmentsByInquiryIdAsync(int inquiryId)
        {
            return await _context.Attachments.Where(a => a.InquiryId == inquiryId).ToListAsync();
        }

        public async Task CreateAsync(Attachment attachment)
        {
            await _context.Attachments.AddAsync(attachment);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteByInquiryIdAsync(int inquiryId)
        {
            var attachments = await GetAttachmentsByInquiryIdAsync(inquiryId);
            _context.Attachments.RemoveRange(attachments);
            await _context.SaveChangesAsync();
        }
    }
}

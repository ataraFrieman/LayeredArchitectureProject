using Microsoft.EntityFrameworkCore;
using PublicInquiriesAPI.Data;
using PublicInquiriesAPI.Models;
using PublicInquiriesAPI.Repositories.Interfaces;
using PublicInquiriesAPI.Utils.Exceptions;


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

        public async Task<Attachment> GetByIdAsync(int id)
        {
            var attachment = await _context.Attachments.FindAsync(id);

            if (attachment == null)
            {
                throw new NotFoundException($"attachment with ID {id} not found.");
            }
            return attachment;
        }

        public async Task DeleteAsync(int id)
        {
            var attachment = await _context.Attachments.FindAsync(id);
            if (attachment != null)
            {
                var inquiry = await _context.Inquiries
                                            .Include(i => i.Attachments)
                                            .FirstOrDefaultAsync(i => i.Id == attachment.InquiryId);

                if (inquiry != null)
                {
                    inquiry.Attachments.Remove(attachment);
                }

                _context.Attachments.Remove(attachment);
                await _context.SaveChangesAsync();
            }
        }


    }
}

using PublicInquiriesAPI.Data;
using PublicInquiriesAPI.Models;
using PublicInquiriesAPI.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using PublicInquiriesAPI.Utils.Exceptions;

namespace PublicInquiriesAPI.Repositories
{
    public class InquiryRepository : IInquiryRepository
    {
        private readonly AppDbContext _context;

        public InquiryRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Inquiry>> GetAllAsync() =>
            await _context.Inquiries.Include(i => i.Attachments).ToListAsync();
        public async Task<Inquiry> GetByIdAsync(int id)
        {
            var inquiry = await _context.Inquiries.FindAsync(id);
            if (inquiry == null)
            {
                throw new NotFoundException($"Inquiry with ID {id} not found.");
            }
            return inquiry;
        }

        public async Task CreateAsync(Inquiry inquiry)
        {
            if (inquiry == null)
            {
                throw new ValidationException("Inquiry cannot be null.");
            }

            try
            {
                await _context.Inquiries.AddAsync(inquiry);
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException ex)
            {
                throw new CustomException($"Failed to save inquiry to the database. {ex}");
            }
        }


        public async Task DeleteAsync(int id)
        {
            var inquiry = await _context.Inquiries
                                        .Include(i => i.Attachments)
                                        .FirstOrDefaultAsync(i => i.Id == id);

            if (inquiry != null)
            {
                if (inquiry.Attachments != null && inquiry.Attachments.Any())
                {
                    _context.Attachments.RemoveRange(inquiry.Attachments);
                }

                _context.Inquiries.Remove(inquiry);
                await _context.SaveChangesAsync();
            }
            else
            {
                throw new NotFoundException($"Inquiry with ID {id} not found.");
            }
        }

    }
}

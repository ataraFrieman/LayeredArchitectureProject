using Microsoft.AspNetCore.Mvc;
using PublicInquiriesAPI.Models;
using PublicInquiriesAPI.Services.Interfaces;
using System.Threading.Tasks;

namespace PublicInquiriesAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AttachmentController : ControllerBase
    {
        private readonly IAttachmentService _attachmentService;

        public AttachmentController(IAttachmentService attachmentService)
        {
            _attachmentService = attachmentService;
        }

        [HttpGet("{inquiryId}")]
        public async Task<IActionResult> GetAttachmentsByInquiryId(int inquiryId)
        {
            var attachments = await _attachmentService.GetAttachmentsByInquiryIdAsync(inquiryId);
            return Ok(attachments);
        }

        [HttpPost("{inquiryId}")]
        public async Task<IActionResult> UploadAttachment(int inquiryId, [FromForm] IFormFile file)
        {
            var attachment = await _attachmentService.UploadAttachmentAsync(file, inquiryId);
            return CreatedAtAction(nameof(GetAttachmentsByInquiryId), new { inquiryId = attachment.InquiryId }, attachment);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttachment(int id)
        {
            await _attachmentService.DeleteAttachmentAsync(id);
            return NoContent();
        }
    }
}

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PublicInquiriesAPI.Models;
using PublicInquiriesAPI.Services.Interfaces;


namespace PublicInquiriesAPI.Controllers
{
    [ApiController]
    [Route("api/inquiries")]
    [Authorize]
    public class InquiryController : ControllerBase
    {
        private readonly IInquiryService _inquiryService;

        public InquiryController(IInquiryService inquiryService)
        {
            _inquiryService = inquiryService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var inquiries = await _inquiryService.GetAllInquiriesAsync();
            return Ok(inquiries);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var inquiry = await _inquiryService.GetInquiryByIdAsync(id);
            return Ok(inquiry);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Inquiry inquiry, [FromForm] List<IFormFile> attachments)
        {
            await _inquiryService.CreateInquiryAsync(inquiry, attachments);
            return Ok(new { message = "Inquiry created successfully." });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _inquiryService.DeleteInquiryAsync(id);
            return NoContent();
        }
    }
}

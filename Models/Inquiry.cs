
namespace PublicInquiriesAPI.Models
{
    public class Inquiry
    {
        public int Id { get; set; }
        public string InquiryType { get; set; }
        public PersonalInfo PersonalInfo { get; set; }
        public ComplaintDetails ComplaintDetails { get; set; }
        public List<Attachment> Attachments { get; set; } = new List<Attachment>();
    }

}


namespace PublicInquiriesAPI.Models
{
    public class Attachment
    {
        public int Id { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public long FileSize { get; set; }
        public int InquiryId { get; set; }
        public Inquiry Inquiry { get; set; }
    }
}


namespace Sepehr.Domain.ViewModels
{
    public class DraftOrderViewModel
    {
        public Guid Id { get; set; }
        public int DraftOrderCode { get; set; }
        public string CreatorName { get; set; } = string.Empty;
        public string CreatedDate { get; set; } = string.Empty;
        public bool Converted { get; set; }
        public string Description { get; set; } = string.Empty;
        public List<AttachmentViewModel> Attachments { get; set; }  

    }
}

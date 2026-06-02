using CertificatesApp.Enums;

namespace CertificatesApp.DTO
{
    public class CreateCertificateDto
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public CertificateTypes Type { get; set; }
        public int Copies { get; set; }
        public string Reason { get; set; } = String.Empty;
        public CertificateStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<RequestHistoryDto> RequestHistory { get; set; }
    }
}

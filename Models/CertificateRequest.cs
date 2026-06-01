using CertificatesApp.Enums;

namespace CertificatesApp.Models
{
    public class CertificateRequest
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid EmployeeId { get; set; }
        public CertificateTypes Type { get; set; }
        public int Copies { get; set; }
        public required string Reason { get; set; }
        public CertificateStatus Status { get; set; } = CertificateStatus.Created;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}

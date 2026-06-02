using CertificatesApp.Enums;

namespace CertificatesApp.DTO
{
    public class RequestHistoryDto
    {
        public Guid Id { get; set; }
        public Guid RequestId { get; set; }
        public Guid UserId { get; set; }
        public CertificateStatus FromStatus { get; set; }
        public CertificateStatus ToStatus { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

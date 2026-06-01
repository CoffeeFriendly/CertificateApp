using CertificatesApp.Enums;

namespace CertificatesApp.Models
{
    public class RequestHistory
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RequestId { get; set; }
        public Guid UserId { get; set; }
        public CertificateStatus FromStatus {  get; set; }
        public CertificateStatus ToStatus {  get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

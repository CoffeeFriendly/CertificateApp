using CertificatesApp.Enums;
using CertificatesApp.Models;

namespace CertificatesApp.DTO
{
    public class CertificateRequestDto
    {
        public Guid Id { get; set; }
        public Guid EmployeeId { get; set; }
        public CertificateTypes Type { get; set; }
        public int Copies { get; set; }
        public required string Reason { get; set; }
        public CertificateStatus Status { get; set; }
        public DateTime CreatedAt { get; set; }
        public List<RequestHistory> History { get; set; }
    }
}

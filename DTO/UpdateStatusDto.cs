using CertificatesApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace CertificatesApp.DTO
{
    public class UpdateStatusDto
    {
        [Required]
        public Guid Id;
        [Required]
        public Guid UserId;
        [Required]
        public CertificateStatus NewStatus { get; set; }
    }
}

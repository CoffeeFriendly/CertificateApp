using CertificatesApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace CertificatesApp.DTO
{
    public class UpdateStatusDto
    {
        [Required]
        public Guid Id;
        [Required]
        public Guid InitiatorId;
        [Required]
        public CertificateStatus NewStatus { get; set; }
    }
}

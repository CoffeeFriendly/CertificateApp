using CertificatesApp.Enums;
using System.ComponentModel.DataAnnotations;

namespace CertificatesApp.DTO
{
    public class CreateCertificateRequestDto
    {
        [Required]
        public Guid EmployeeId { get; set; }
        [Required]
        public CertificateTypes Type { get; set; }
        [Range(1, 10, ErrorMessage = "Количество копий должно быть от 1 до 10")]
        public int Copies { get; set; } = 1;
        [Required]
        [MinLength(3, ErrorMessage = "Причина должна содержать как минимум 3 символа")]
        [MaxLength(300, ErrorMessage = "Причина не может содержать более 300 символов")]
        public string Reason { get; set; } = String.Empty;
    }
}

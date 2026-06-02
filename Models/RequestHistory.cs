using CertificatesApp.Enums;

namespace CertificatesApp.Models
{
    public class RequestHistory
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid RequestId { get; set; }
        public Guid InitiatorId { get; set; } /* В контексте "истории", это не тот кому предназначена справка,
                                           а тот кто внёс изменение */
        public CertificateStatus FromStatus {  get; set; }
        public CertificateStatus ToStatus {  get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}

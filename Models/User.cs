using CertificatesApp.Enums;

namespace CertificatesApp.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public UserRoles Role {  get; set; }
    }
}

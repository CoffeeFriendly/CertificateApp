using CertificatesApp.DTO;

namespace CertificatesApp.Services
{
    public interface ICertificateService
    {
        Task<CertificateRequestDto> CreateRequestAsync(CreateCertificateRequestDto dto);
        Task<List<CertificateRequestDto>> GetByEmployeeAsync(Guid employeeId);
        Task<CertificateRequestDto> GetByIdAsync(Guid id);
        Task<List<CertificateRequestDto>> GetAllRequests();
        Task<CertificateRequestDto> UpdateStatusAsync(Guid requestId, Guid initiatorId, UpdateStatusDto dto);
    }
}

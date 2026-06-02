using CertificatesApp.Data;
using CertificatesApp.DTO;
using CertificatesApp.Enums;
using CertificatesApp.Exceptions;
using CertificatesApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace CertificatesApp.Services
{
    public class CertificateService : ICertificateService
    {
        private readonly AppDbContext _context;

        public CertificateService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<CertificateRequestDto> CreateRequestAsync(CreateCertificateRequestDto dto)
        {
            var employeeExists = await _context.Users
                .AnyAsync(u => u.Id == dto.EmployeeId);
            var activeRequestExists = await _context.CertificateRequests
                .AnyAsync(r =>
                    r.EmployeeId == dto.EmployeeId &&
                    r.Type == dto.Type &&
                    r.Status != Enums.CertificateStatus.Completed &&
                    r.Status != Enums.CertificateStatus.Cancelled);
            if (activeRequestExists)
            {
                throw new DuplicateRequestException("Обнаружен дубликат");
            }
            if (!employeeExists)
            {
                throw new NotFoundException("Пользователь с id " + dto.EmployeeId + " не найден.");
            }

            var request = new CertificateRequest
            {
                EmployeeId = dto.EmployeeId,
                Type = dto.Type,
                Copies = dto.Copies,
                Reason = dto.Reason
            };
            _context.CertificateRequests.Add(request);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(request.Id);
        }

        public async Task<List<CertificateRequestDto>> GetByEmployeeAsync(Guid employeeId)
        {
            var request = await _context.CertificateRequests
                .Where(r => r.EmployeeId == employeeId)
                .Include(r => r.History)
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
            return request.Select(MapToDto).ToList();
        }

        public async Task<CertificateRequestDto> GetByIdAsync(Guid requestId)
        {
            var request = await _context.CertificateRequests
                .Include(r => r.History)
                .FirstOrDefaultAsync(r => r.Id == requestId);
            if (request == null)
            {
                throw new NotFoundException("Заявка с id " + requestId + " не найдена.");
            }
            return MapToDto(request);
        }

        public async Task<CertificateRequestDto> UpdateStatusAsync(Guid requestId, UpdateStatusDto dto)
        {
            var request = await _context.CertificateRequests.FindAsync(requestId);

            if (request == null)
            {
                throw new NotFoundException("Заявка с id " + requestId + " не найдена.");
            }

            var oldStatus = request.Status;
            var newStatus = dto.NewStatus;

            if (!IsValidStatusChange(oldStatus, newStatus))
            {
                throw new InvalidStatusChangeException("Недопустимый переход статуса заявки.");
            }

            request.Status = newStatus;

            var history = new RequestHistory
            {
                RequestId = requestId,
                UserId = dto.UserId,
                FromStatus = oldStatus,
                ToStatus = newStatus,
                UpdatedAt = DateTime.UtcNow
            };

            _context.RequestHistory.Add(history);
            await _context.SaveChangesAsync();

            return await GetByIdAsync(requestId);
        }

        public async Task<List<CertificateRequestDto>> GetAllRequests()
        {
            var request = await _context.CertificateRequests
                .OrderByDescending(r => r.CreatedAt)
                .ToListAsync();
            return request.Select(MapToDto).ToList();
        }

        // Вспомогательные методы. Если будет время - возможно лучше вынести в отдельный файл utils как в прошлом проекте
        private bool IsValidStatusChange(CertificateStatus oldStatus, CertificateStatus newStatus)
        {
            if (oldStatus == newStatus)
            {
                throw new InvalidStatusChangeException("Новый статус должен отличаться от старого.");
            }
            if (oldStatus == CertificateStatus.Completed || oldStatus == CertificateStatus.Cancelled)
            {
                throw new InvalidStatusChangeException("Заявки в статусе ЗАВЕРШЕНО или ОТМЕНЕНО недопустимо менять");
            }
            if (newStatus == CertificateStatus.Cancelled)
            {
                return true;
            }
            // Возможные варианты изменения статуса
            return (oldStatus == CertificateStatus.Created && newStatus == CertificateStatus.InProgress) ||
                   (oldStatus == CertificateStatus.InProgress && newStatus == CertificateStatus.Ready) ||
                   (oldStatus == CertificateStatus.Ready && newStatus == CertificateStatus.Completed);
            // Если дошло до этой строчки - значит что-то пошло не по плану
            throw new InvalidStatusChangeException("Неожиданная ошибка транзакции статуса");
        }

        private CertificateRequestDto MapToDto(CertificateRequest request)
        {
            return new CertificateRequestDto
            {
                Id = request.Id,
                EmployeeId = request.EmployeeId,
                Type = request.Type,
                Copies = request.Copies,
                Reason = request.Reason,
                Status = request.Status,
                CreatedAt = request.CreatedAt,
                History = request.History
            };
        }
    }
}

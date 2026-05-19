using APBD_TASK_7.DTOs;

namespace APBD_TASK_7.Services
{
    public interface IPcService
    {
        Task<IEnumerable<PcDto>> GetAllPcsAsync();
        Task<IEnumerable<PcComponentDto>?> GetPcComponentsAsync(int pcId);
        Task<PcDto> CreatePcAsync(PcCreationDto dto);
        Task<bool> UpdatePcAsync(int id, PcUpdateDto dto);
        Task<bool> DeletePcAsync(int id);
    }
}
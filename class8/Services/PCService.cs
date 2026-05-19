using APBD_TASK_7.Data;
using APBD_TASK_7.DTOs;
using APBD_TASK_7.Models;
using Microsoft.EntityFrameworkCore;

namespace APBD_TASK_7.Services
{
    public class PcService : IPcService
    {
        private readonly AppDbContext _context;

        public PcService(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<PcDto>> GetAllPcsAsync()
        {
            return await _context.PCs
                .Select(pc => new PcDto
                {
                    Id = pc.Id,
                    Name = pc.Name,
                    Weight = pc.Weight,
                    Warranty = pc.Warranty,
                    CreatedAt = pc.CreatedAt,
                    Stock = pc.Stock
                })
                .ToListAsync();
        }

        public async Task<IEnumerable<PcComponentDto>?> GetPcComponentsAsync(int pcId)
        {
            var pcExists = await _context.PCs.AnyAsync(p => p.Id == pcId);
            if (!pcExists) return null;

            return await _context.PCComponents
                .Where(pcc => pcc.PCId == pcId)
                .Include(pcc => pcc.Component)
                .Select(pcc => new PcComponentDto
                {
                    ComponentCode = pcc.ComponentCode,
                    Name = pcc.Component.Name,
                    Description = pcc.Component.Description,
                    Amount = pcc.Amount
                })
                .ToListAsync();
        }

        public async Task<PcDto> CreatePcAsync(PcCreationDto dto)
        {
            var pc = new PC
            {
                Name = dto.Name,
                Weight = dto.Weight,
                Warranty = dto.Warranty,
                CreatedAt = dto.CreatedAt,
                Stock = dto.Stock
            };

            _context.PCs.Add(pc);
            await _context.SaveChangesAsync();

            return new PcDto
            {
                Id = pc.Id,
                Name = pc.Name,
                Weight = pc.Weight,
                Warranty = pc.Warranty,
                CreatedAt = pc.CreatedAt,
                Stock = pc.Stock
            };
        }

        public async Task<bool> UpdatePcAsync(int id, PcUpdateDto dto)
        {
            var pc = await _context.PCs.FindAsync(id);
            if (pc == null) return false;

            pc.Name = dto.Name;
            pc.Weight = dto.Weight;
            pc.Warranty = dto.Warranty;
            pc.CreatedAt = dto.CreatedAt;
            pc.Stock = dto.Stock;

            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeletePcAsync(int id)
        {
            var pc = await _context.PCs.FindAsync(id);
            if (pc == null) return false;

            // Removes the computer; EF Core handles bindings via Cascade Delete [cite: 126]
            _context.PCs.Remove(pc);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
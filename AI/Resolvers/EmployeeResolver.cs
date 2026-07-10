using GestionDesPresences.Data;
using GestionDesPresences.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionDesPresences.AI.Resolvers
{
    public class EmployeeResolver : IEntityResolver
    {
        private readonly ApplicationDbContext _context;

        public EmployeeResolver(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Collaborateur?> ResolveEmployeeAsync(string? employeeName)
        {
            if (string.IsNullOrWhiteSpace(employeeName))
                return null;

            employeeName = employeeName.Trim();

            return await _context.Collaborateurs
                .AsNoTracking()
                .FirstOrDefaultAsync(c =>
                    c.Nom.ToLower() == employeeName.ToLower());
        }

        public async Task<Collaborateur?> ResolveEmployeeAsync(int id)
        {
            return await _context.Collaborateurs
                .FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}

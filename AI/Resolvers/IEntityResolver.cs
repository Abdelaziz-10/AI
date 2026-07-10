using GestionDesPresences.Models;

namespace GestionDesPresences.AI.Resolvers
{
    public interface IEntityResolver
    {
        Task<Collaborateur?> ResolveEmployeeAsync(string? employeeName);
        Task<Collaborateur?> ResolveEmployeeAsync(int id);
    }
}
namespace GestionDesPresences.AI.Services.Storage
{
    public interface IFileStorageService
    {
        Task<string> SavePdfAsync(byte[] pdf, string fileName);

        Task DeleteOldFilesAsync(int olderThanHours = 24);
    }
}

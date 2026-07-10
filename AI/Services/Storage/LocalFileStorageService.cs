namespace GestionDesPresences.AI.Services.Storage
{
    public class LocalFileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _environment;

        public LocalFileStorageService(IWebHostEnvironment environment)
        {
            _environment = environment;
        }

        public async Task<string> SavePdfAsync(byte[] pdf, string fileName)
        {
            var folder = Path.Combine(
                _environment.WebRootPath,
                "ai-files",
                "reports");

            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            var path = Path.Combine(folder, fileName);

            await File.WriteAllBytesAsync(path, pdf);

            return $"/ai-files/reports/{fileName}";
        }

        public Task DeleteOldFilesAsync(int olderThanHours = 24)
        {
            var folder = Path.Combine(
                _environment.WebRootPath,
                "ai-files",
                "reports");

            if (!Directory.Exists(folder))
                return Task.CompletedTask;

            var files = Directory.GetFiles(folder);

            foreach (var file in files)
            {
                var info = new FileInfo(file);

                if (info.CreationTime < DateTime.Now.AddHours(-olderThanHours))
                {
                    info.Delete();
                }
            }

            return Task.CompletedTask;
        }
    }
}

using Manhwa.Application.Common.Interfaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Manhwa.Infrastructure.FileStorage
{
    public class LocalStorageService : IStorageService
    {
        private readonly FileStorageOptions _options;

        public LocalStorageService(IOptions<FileStorageOptions> options)
        {
            _options = options.Value;
        }

        public async Task<string> UploadAsync(Stream fileStream, string path, string contentType, bool isImmutable = false, CancellationToken ct = default)
        {
            var physicalPath = Path.Combine(_options.RootPath, path);
            var directory = Path.GetDirectoryName(physicalPath);

            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory!);
            }

            using var targetStream = File.Create(physicalPath);
            await fileStream.CopyToAsync(targetStream, ct);

            return $"{path.Replace("\\", "/")}";
        }

        public Task DeleteAsync(string key, CancellationToken ct = default)
        {
            var physicalPath = Path.Combine(_options.RootPath, key);
            if (File.Exists(physicalPath))
            {
                File.Delete(physicalPath);
            }
            return Task.CompletedTask;
        }
        public Task DeleteDirectoryAsync(string path, CancellationToken ct = default)
        {
            var physicalPath = Path.Combine(_options.RootPath, path);

            if (Directory.Exists(physicalPath))
            {
                Directory.Delete(physicalPath, true); 
            }

            return Task.CompletedTask;
        }
    }
}

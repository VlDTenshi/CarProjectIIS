using CarProjectIIS.Core.Domain;
using CarProjectIIS.Core.Dto;
using CarProjectIIS.Core.ServiceInterface;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace CarProjectIIS.ApplicationServices.Services
{
    public class FileServices : IFileServices
    {
        private readonly IHostEnvironment _webHost;
        private readonly Data.CarContext _context;

        public FileServices
            (
            IHostEnvironment webHost,
            Data.CarContext context
            )
        {
            _webHost = webHost;
            _context = context;
        }
        public void UploadFilesToDatabase(CarItemDto dto, CarItem domain)
        {
            if (dto.Files != null && dto.Files.Count > 0)
            {
                foreach (var file in dto.Files)
                {
                    using (var target = new MemoryStream())
                    {
                        FileToDatabase1 files = new FileToDatabase1()
                        {
                            Id = Guid.NewGuid(),
                            ImageTitle = file.FileName,
                            CarItemId = domain.Id,
                        };
                        file.CopyTo(target);
                        files.ImageData = target.ToArray();

                        _context.FileToDatabases1.Add(files);
                    }
                }
            }
        }
        public async Task<FileToDatabase1> RemoveImageFromDatabase(FileToDatabaseDto dto)
        {
            var image = await _context.FileToDatabases1
                .Where(x => x.Id == dto.Id)
                .FirstOrDefaultAsync();

            _context.FileToDatabases1.Remove(image);
            await _context.SaveChangesAsync();

            return image;
        }
        public async Task<FileToDatabase1> RemovePhotosFromDatabase(FileToDatabaseDto[] dto)
        {
            foreach (var dtos in dto)
            {
                var photoId = await _context.FileToDatabases1
                    .Where(x => x.Id == dtos.Id)
                    .FirstOrDefaultAsync();
                _context.FileToDatabases1.Remove(photoId);
                await _context.SaveChangesAsync();
            }
            return null;
        }
    }
}

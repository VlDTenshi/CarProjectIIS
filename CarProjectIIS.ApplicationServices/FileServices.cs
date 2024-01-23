using CarProjectIIS.Core.Domain;
using CarProjectIIS.Core.Dto;
using CarProjectIIS.Core.ServiceInterface;
using Microsoft.Extensions.Hosting;

namespace CarProjectIIS.ApplicationServices
{
    public class FileServices : IFileServices
    {
        private readonly IHostEnvironment _webHost;
        private readonly CarContext _context;

        public FileServices
            (
            IHostEnvironment webHost,
            CarContext context
            )
        {
            _webHost = webHost;
            _context = context;
        }
        public void UploadFilesToDatabase(CarItemDto dto, CarItem domain)
        {
            if(dto.Files != null && dto.Files.Count > 0)
            {
                foreach(var file in dto.Files)
                {
                    using( var target = new MemoryStream())
                    {
                        FileToDatabase files = new FileToDatabase()
                        {
                            Id = Guid.NewGuid(),
                            ImageTitle = file.FileName,
                            CarItemId = domain.Id,
                        };
                        file.CopyTo( target );
                        files.ImageData = target.ToArray();

                        _context.FileToDatabases.Add( files );
                    }
                }
            }
        }
        public async Task<FileToDatabase> RemovePhotosFromDatabase(FileToDatabaseDto[] dto)
        {
            foreach(var dtos in dto)
            {
                var photoId = await _context.FileToDatabases
                    .Where(x => x.Id == dtos.Id)
                    .FirstOrDefaultAsync();
                _context.FileToDatabases.Remove( photoId );
                await _context.SaveChangesAsync();
            }
            return null;
        }
    }
}

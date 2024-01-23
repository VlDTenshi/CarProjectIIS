using CarProjectIIS.Core.Domain;
using CarProjectIIS.Core.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace CarProjectIIS.Core.ServiceInterface
{
    public interface IFileServices
    {
        void UploadFilesToDatabase(CarItemDto dto, CarItem domain);
        Task<FileToDatabase> RemoveImageFromDatabase(FileToDatabaseDto dto);
        Task<FileToDatabase> RemovePhotosFromDatabase(FileToDatabaseDto[] dto);
    }
}

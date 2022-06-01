using ImageServer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageServer.Services.ImageService
{
    public interface IImageService
    {
        Task<ServiceResponce<List<Image>>> GetAllImages();
        Task<ServiceResponce<Image>> GetImageByName(string name);
        Task<ServiceResponce<List<Image>>> DeleteImageById(int id);
        Task<ServiceResponce<List<Image>>> AddImage(Image image);
        Task<ServiceResponce<List<Image>>> UpdateImage(Image image);
    }
}

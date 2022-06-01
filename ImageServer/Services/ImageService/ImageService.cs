using ImageServer.Data;
using ImageServer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ImageServer.Services.ImageService
{
    public class ImageService : IImageService
    {
        private readonly DataContext dataContext;
        public ImageService(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }
        public async Task<ServiceResponce<List<Image>>> AddImage(Image image)
        {
            dataContext.Images.Add(image);
            await dataContext.SaveChangesAsync();
            return new ServiceResponce<List<Image>> { Data = dataContext.Images.ToList() };
        }

        public async Task<ServiceResponce<List<Image>>> DeleteImageById(int id)
        {
            var item = await dataContext.Images.FirstOrDefaultAsync(image => image.Id == id);
            if (item == default(Image))
                return new ServiceResponce<List<Image>> { Success = false, Message = "We havn't this image" };
            dataContext.Images.Remove(item);
            await dataContext.SaveChangesAsync();
            var serviceResponce = new ServiceResponce<List<Image>>();
            serviceResponce.Data = await dataContext.Images.ToListAsync();
            return serviceResponce;
        }

        public async Task<ServiceResponce<List<Image>>> GetAllImages()
        {
            var responce = new ServiceResponce<List<Image>>();
            responce.Data = await dataContext.Images.ToListAsync();
            return responce;
        }

        public async Task<ServiceResponce<Image>> GetImageByName(string name)
        {
            var responce = new ServiceResponce<Image>();
            responce.Data = await dataContext.Images.FirstOrDefaultAsync(item => item.Name == name);
            return responce;
        }

        public async Task<ServiceResponce<List<Image>>> UpdateImage(Image image)
        {
            var updatingItem = await dataContext.Images.FirstOrDefaultAsync(item => item.Id == image.Id);
            if (updatingItem == null)
                return new ServiceResponce<List<Image>> { Success = false, Message = "This image not found" };
            updatingItem.Name = image.Name;
            updatingItem.Url = image.Url;
            updatingItem.Themes = image.Themes;

            return new ServiceResponce<List<Image>> { Data = dataContext.Images.ToList() };
        }
    }
}

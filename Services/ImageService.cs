using ExpressVoitures.Models;
using Microsoft.AspNetCore.Authorization;

namespace ExpressVoitures.Services
{    
    [Authorize(Roles = "Admin")]
    public class ImageService
    {
        private readonly PathService pathService;

        public ImageService(PathService pathService)
        {
            this.pathService = pathService;
        }

        public async Task<Image> UploadAsync(Image image)
        {
            try 
            { 
                var uploadsPath = pathService.GetUploadsPath();

                var imageFile = image.File;
                var imageFileName = GetRandomFileName(imageFile.FileName);
                var imageUploadPath = Path.Combine(uploadsPath, imageFileName);

                using (var fs = new FileStream(imageUploadPath, FileMode.Create))
                {
                    await imageFile.CopyToAsync(fs);
                }

                image.Name = imageFile.FileName;
                image.Path = pathService.GetUploadsPath(imageFileName, withWebRootPath: false);

                return image;
            }
            catch (Exception e)
            {
                Console.WriteLine("Image failure (2)...");
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        public void DeleteUploadedFile(Image? image)
        {
            if (image == null)
                return;

            var imagePath = pathService.GetUploadsPath(Path.GetFileName(image.Path));

            if (File.Exists(imagePath))
                File.Delete(imagePath);
        }

        private string GetRandomFileName(string filename)
        {
            return Guid.NewGuid() + Path.GetExtension(filename);
        }
    }
}
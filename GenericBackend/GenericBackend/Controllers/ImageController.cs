using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using GenericBackend.Core.Images;
using GenericBackend.Images;

namespace GenericBackend.Controllers
{
    
    [AllowAnonymous]
    public class ImageController : ApiController
    {
        private const string ContainerName = "goodnightmedical";

        [HttpGet]
        public IHttpActionResult Get()
        {
            return Ok();
        }

        private const int MaxSize = 1024*1024*1;
        private async Task<IHttpActionResult> Upload(HttpRequestMessage request, string path, string imageName)
        {
            if (request.Content.Headers.ContentLength > MaxSize)
            {
                return BadRequest($"Only images less then {MaxSize / 1024} Kb are allowed");
            } 
            var provider = new ImageMultipartFormDataStreamProvider(path);
            var result = await request.Content.ReadAsMultipartAsync(provider);
            if (result.FileData.Count <= 0)
                return BadRequest("No files");

            if (result.FileData.Count > 1)
                return BadRequest("you can only upload one file");
            
            var fileData = result.FileData.First();
            StorageManager storageManager = new StorageManager();
            FileInfo fileInfo = new FileInfo(fileData.LocalFileName);
            
            var isJpeg = string.CompareOrdinal(fileData.Headers.ContentType.MediaType, "image/jpeg") == 0;
            if (!isJpeg || fileInfo.Extension !=".jpg")
            {
                return BadRequest("Only jpeg images are allowed");
            }
            var filePath = Path.Combine(path, fileData.LocalFileName);
            var imageFileName = imageName + fileInfo.Extension;
            var imageUrl = await storageManager.UploadFromStreamAsync(ContainerName, imageFileName, filePath);
            
            return Ok(imageUrl.ToString());
        }
        [HttpPost]
        public async Task<IHttpActionResult> UploadImage()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType));
            }
            try
            {
                var path = HttpRuntime.AppDomainAppPath + @"\App_Data\Images";
                var result = await Upload(Request, path, Guid.NewGuid().ToString("N"));
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
            
        }

       
    }
}

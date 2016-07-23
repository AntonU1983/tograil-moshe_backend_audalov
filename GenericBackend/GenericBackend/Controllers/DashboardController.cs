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
using GenericBackend.Excel;
using GenericBackend.Images;
using GenericBackend.Models;

namespace GenericBackend.Controllers
{
    [Authorize]
    [RoutePrefix("api/dashboard")]
    public class DashboardController : ApiController
    {

        [HttpGet]
        [Route("parse")]
        [AllowAnonymous]
        public async Task<IHttpActionResult> Get2()
        {
            var userInfo = new ParsePlanActual(@"F:\Git Repositories\moshe_backend\GenericBackend\GenericBackend\App_Start\ExcelDocumentsPlanActual.xlsx");
            userInfo.ParsePlanSheet();
          
            return Ok();
        }

        [HttpGet]
        public async Task<IHttpActionResult> Get()
        {
            var userInfo = UserModel.GetUserInfo(User);
            var model = new DashboardViewModel
            {
                IsUploadAvailable = userInfo.IsSuperUser
            };
            return Ok(model) ;
        }
        [HttpPost]
        [AllowAnonymous]
       // [Authorize(Roles = UserModel.SuperuserRole)]
        public async Task<IHttpActionResult> UploadFile()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType));
            }
            try
            {
                var path = HttpRuntime.AppDomainAppPath + @"\App_Data\";
                var result = await Upload(Request, path, Guid.NewGuid().ToString("N"));
                return result;
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

        private const int MaxSize = 1024 * 1024 * 10;
        private async Task<IHttpActionResult> Upload(HttpRequestMessage request, string path, string fileName)
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
            
            FileInfo fileInfo = new FileInfo(fileData.LocalFileName);

            var isExcel = string.CompareOrdinal(fileData.Headers.ContentType.MediaType, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") == 0;
            if (!isExcel || fileInfo.Extension.StartsWith("xls"))
            {
                return BadRequest("Only Excel files are allowed");
            }
            var filePath = Path.Combine(path, fileData.LocalFileName);
            var imageFileName = fileName + fileInfo.Extension;

            return Ok(imageFileName);
        }
    }
}

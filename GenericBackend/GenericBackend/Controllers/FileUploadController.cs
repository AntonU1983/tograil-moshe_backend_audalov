using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Results;
using GenericBackend.DataModels.Plan;
using GenericBackend.Excel;
using GenericBackend.Helpers;
using GenericBackend.Images;
using GenericBackend.Models;
using GenericBackend.Repository;
using GenericBackend.UnitOfWork.GoodNightMedical;
using Microsoft.Owin.Logging;

namespace GenericBackend.Controllers
{
    public class FileUploadController : ApiController
    {

        private const int MaxSize = 1024 * 1024 * 10;
        private readonly IMongoRepository<PlanSheet> _planSheetRepository;


        public FileUploadController(IUnitOfWork unitOfWork)
        {
            _planSheetRepository = unitOfWork.PlanSheets;
        }

        [HttpPost]
        [AuthorizeUser(Roles = UserModel.SuperuserRole)]
        public async Task<IHttpActionResult> UploadFiles()
        {
            // Check if the request contains multipart/form-data.
            if (!Request.Content.IsMimeMultipartContent())
            {
                return ResponseMessage(new HttpResponseMessage(HttpStatusCode.UnsupportedMediaType));
            }

            string sPath = "";
            sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Start/ExcelDocuments/");

            var provider = new ImageMultipartFormDataStreamProvider(sPath);
            var result = await Request.Content.ReadAsMultipartAsync(provider);
            int requestedFilesCnt = result.FileData.Count;

            if (requestedFilesCnt <= 0)
                return BadRequest("No files");

            try
            {
                int uploadedFilesCnt = 0;
                for (int i = 0; i < requestedFilesCnt; i++)
                {
                    var hpf = result.FileData[i];

                    if (hpf.Headers.ContentLength > 0 && hpf.Headers.ContentLength < MaxSize)
                    {
                        if (!File.Exists(sPath + Path.GetFileName(hpf.LocalFileName)))
                        {
                            var isExcel = string.CompareOrdinal(hpf.Headers.ContentType.MediaType,
                                "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet") == 0;
                            if (isExcel || new FileInfo(hpf.LocalFileName).Extension.StartsWith("xls"))
                            {
                                string newPath = sPath + Path.GetFileName(hpf.LocalFileName);

                                BinaryWriter bw = new BinaryWriter(new FileStream(newPath, FileMode.Create));
                                bw.Close();

                                var parser = new ParsePlanActual(newPath);
                                var planSheet = parser.ParsePlanSheet();

                                _planSheetRepository.Add(planSheet);


                                uploadedFilesCnt++;
                            }
                            else
                            {
                                //Log error("Only Excel files are allowed");
                            }
                        }
                        
                    }
                    else
                    {
                        //Log error("Wrong File Size");
                    }
                }



                if (uploadedFilesCnt == requestedFilesCnt)
                {
                    return Ok($"All of {requestedFilesCnt} Files Uploaded Successfully");

                }

                else if (uploadedFilesCnt > 0)
                {
                    return Ok($"{uploadedFilesCnt} From {requestedFilesCnt} Files Uploaded Successfully");
                }

                else
                {
                    return BadRequest("Upload Failed");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }

       // [HttpPost] 
       //  [AuthorizeUser(AccessLevel = "SuperUser")] 
       //  public string UploadFiles()
       //  { 
       //      int iUploadedCnt = 0; 
              
       //      string sPath = ""; 
       //      sPath = System.Web.Hosting.HostingEnvironment.MapPath("~/App_Start/ExcelDocuments/"); 
 
 
       //      System.Web.HttpFileCollection hfc = System.Web.HttpContext.Current.Request.Files; 
              
       //      for (int iCnt = 0; iCnt <= hfc.Count - 1; iCnt++) 
       //     { 
       //         System.Web.HttpPostedFile hpf = hfc[iCnt]; 
 
 
       //          if (hpf.ContentLength > 0) 
       //          { 
       //              if (!File.Exists(sPath + Path.GetFileName(hpf.FileName))) 
       //              { 
       //                 var filename = sPath + Path.GetFileName(hpf.FileName); 
       //                  hpf.SaveAs(filename); 
       //                  iUploadedCnt = iUploadedCnt + 1; 
       //                  var parser = new ParsePlanActual(filename); 
       //                 var result = parser.ParsePlanSheet(); 
 
 
       //                  _planSheetRepository.Add(result); 
       //              } 
       //          } 
       //      } 
            
       //      if (iUploadedCnt > 0) 
       //      { 
       //          return iUploadedCnt + " Files Uploaded Successfully"; 
       //      } 
       //      else 
       //      { 
       //          return "Upload Failed"; 
       //      } 
       //} 


    }
}

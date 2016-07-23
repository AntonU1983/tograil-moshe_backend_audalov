using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using GenericBackend.DataModels.Document;
using GenericBackend.Models;
using GenericBackend.UnitOfWork.GoodNightMedical;

namespace GenericBackend.Controllers
{
    [Authorize]
    [RoutePrefix("api/document")]
    public class DocumentController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public DocumentController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult AddDocument(DocumentRequest request)
        {
            if (request == null)
                return BadRequest("document request can't be null");
            if (string.IsNullOrEmpty(request.Name))
                return BadRequest("document name can't be empty");
            var userName = UserModel.GetUserInfo(User).Name;
            var doc = new DocumentInfo
            {
                User = userName,
                DateOfPost = DateTime.UtcNow,
                Name = request.Name,
                Type = request.Type
            };
            _unitOfWork.DocumentsInfo.Add(doc);
            return Ok();
        }
        [HttpGet]
        public IHttpActionResult Get()
        {
            var user = UserModel.GetUserInfo(User);
            var query = _unitOfWork.DocumentsInfo.AsQueryable();
            if (!user.IsSuperUser)
                query = _unitOfWork.DocumentsInfo.Where(x => x.User == user.Name);

            var documentInfos = query.ToList();
            return Ok(documentInfos);
        }
    }
}

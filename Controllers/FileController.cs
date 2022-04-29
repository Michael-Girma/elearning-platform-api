using System.Text;
using elearning_platform.Models;
using elearning_platform.Services;
using Microsoft.AspNetCore.Mvc;

namespace elearning_platform.Controllers
{
    [Route("cdn")]
    [ApiController]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        public FileController(IFileService fileService)
        {
            _fileService = fileService;
        }


        [HttpGet]
        [Route("download/{fileId}")]
        public async Task<ActionResult<TemporaryFileRef?>> GetFileDownloadLink(Guid fileId)
        {
            var fileRef = await _fileService.GetTemporaryRef(fileId);
            if (fileRef != null)
            {
                return Ok(fileRef);
            }
            return BadRequest("File Not Found");
        }

        [HttpPost]
        [Route("upload")]
        public async Task<ActionResult<InternalFileMetadata>> UploadFile()
        {
            var file = Request.Form?.Files?.FirstOrDefault();
            if (file != null)
            {
                var metadata = await _fileService.UploadFile(file);
                return Ok(metadata);
            }
            return BadRequest();
        }
    }
}
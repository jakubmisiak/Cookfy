using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.StaticFiles;

namespace Cookfy.Controllers
{
    [Route("file")]
    public class FileController : ControllerBase
    {
        [HttpGet("pic")]
        public ActionResult GetPic([FromQuery] string fileName)
        {
            var rootPath = Directory.GetCurrentDirectory();
            var filePath = $"{rootPath}/PrivateFiles/Pictures/{fileName}";
            var fileExists = System.IO.File.Exists(filePath);
            if (!fileExists)
            {
                return NotFound();
            }

            var contentProvider = new FileExtensionContentTypeProvider();
            contentProvider.TryGetContentType(fileName, out string contentType);

            var fileContents = System.IO.File.ReadAllBytes(filePath);

            return File(fileContents, contentType, fileName);
        }

        [HttpGet("profilePic")]
        public ActionResult GetProfilePic([FromQuery] string fileName)
        {
            var rootPath = Directory.GetCurrentDirectory();
            var filePath = $"{rootPath}/PrivateFiles/ProfilePictures/{fileName}";
            var fileExists = System.IO.File.Exists(filePath);
            if (!fileExists)
            {
                return NotFound();
            }

            var contentProvider = new FileExtensionContentTypeProvider();
            contentProvider.TryGetContentType(fileName, out string contentType);

            var fileContents = System.IO.File.ReadAllBytes(filePath);

            return File(fileContents, contentType, fileName);
        }

        [HttpPost("pic")]
        public ActionResult UploadPicture([FromForm] IFormFile file)
        {
            if(file != null && file.Length > 0)
            {
                var rootPath = Directory.GetCurrentDirectory();
                var fileName = file.FileName;
                var fullPath = $"{rootPath}/PrivateFiles/Pictures/{fileName}";
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Ok();
            }
            return BadRequest();
        }

        [HttpPost("profilePic")]
        public ActionResult UploadProfilePicture([FromForm] IFormFile file)
        {
            if (file != null && file.Length > 0)
            {
                var rootPath = Directory.GetCurrentDirectory();
                var fileName = file.FileName;
                var fullPath = $"{rootPath}/PrivateFiles/ProfilePictures/{fileName}";
                using (var stream = new FileStream(fullPath, FileMode.Create))
                {
                    file.CopyTo(stream);
                }
                return Ok();
            }
            return BadRequest();
        }
    }
}

using FreeCourse.Services.PhotoStock.Dtos;
using FreeCourse.Shared.ControllerBases;
using FreeCourse.Shared.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FreeCourse.Services.PhotoStock.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : CustomBaseController
    {
        [HttpPost]
        public async Task<IActionResult> PhotoSave(IFormFile photo,CancellationToken ct)
        {
            if(photo != null&& photo.Length > 0)
            {
                var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/photos", photo.FileName);
                using var stream = new FileStream(path, FileMode.Create);
                await photo.CopyToAsync(stream, cancellationToken: ct);
                
                var returnpath="photos/"+photo.FileName;
                
                PhotoDto photodto = new() { URL = returnpath };
                return CreateActionResultInstance(Response<PhotoDto>.Success(photodto,200));
            }
            return CreateActionResultInstance(Response<PhotoDto>.Fail("empty",400));
        }   

        public IActionResult PhotoDelete(string path)
        {
            var dpath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/photos", path);
            if (!System.IO.File.Exists(dpath))
            {
                return CreateActionResultInstance(Response<NoContent>.Fail("empty directory",404));
            }

               System.IO.File.Delete(dpath);
            return CreateActionResultInstance(Response<NoContent>.Success(204));
        }
        

        
    }
}

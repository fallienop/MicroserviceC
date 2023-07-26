using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Shared.Dtos;

namespace FreeCourse.Services.Catalog.Services
{
    public interface ICourseService
    {
      Task<Response<List<CourseDto>>> GetAllAsync();


     
     Task<Response<CourseDto>> GetByID(string id);
     
    
     Task<Response<List<CourseDto>>> GetAllByUserId(string userid);
     
    
    
     Task<Response<CourseDto>> CourseCreateAsync(CourseCreateDto course);
     
    
    
     Task<Response<NoContent>> CourseUpdateAsync(CourseUpdateDto course);
     
    
    
     Task<Response<NoContent>> CourseDeleteAsync(string id);
    }   
}
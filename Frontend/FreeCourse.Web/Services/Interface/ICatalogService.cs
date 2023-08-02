using FreeCourse.Web.Models.Catalog;

namespace FreeCourse.Web.Services.Interface
{
    public interface ICatalogService
    {
        Task<List<CategoryViewModel>> GetAllCategories();

        Task<List<CourseViewModel>> GetAllCoursesAsync();
        Task<CourseViewModel> GetCourseByUserIdAsync(string userid);

        Task<bool>DeleteCourseAsync(string courseid);
        Task<CourseViewModel> GetByCourseId(string courseid);
        Task<bool>UpdateCourse(CourseUpdatePut course);
        Task<bool>CreateCourse(CourseCreateInput input);
    }
}

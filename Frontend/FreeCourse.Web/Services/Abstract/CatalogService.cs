using FreeCourse.Shared.Dtos;
using FreeCourse.Web.Models;
using FreeCourse.Web.Models.Catalog;
using FreeCourse.Web.Services.Interface;

namespace FreeCourse.Web.Services.Abstract
{
    public class CatalogService : ICatalogService
    {
       private readonly HttpClient _httpClient;

        public CatalogService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<bool> CreateCourse(CourseCreateInput input)
        {
            var response = await _httpClient.PostAsJsonAsync("courses", input);
            return response.IsSuccessStatusCode;
        }

        public async Task<bool> DeleteCourseAsync(string courseid)
        {
            var response = await _httpClient.DeleteAsync($"courses/{courseid}");
                return response.IsSuccessStatusCode;
        }

        public async Task<List<CategoryViewModel>> GetAllCategories()
        {
            var resp = await _httpClient.GetAsync("categories");
            if (!resp.IsSuccessStatusCode)
            {
                return null;
            }

            var respsuccess = await resp.Content.ReadFromJsonAsync<Response<List<CategoryViewModel>>>();
            return respsuccess.Data;
        }

        public async Task<List<CourseViewModel>> GetAllCoursesAsync()
        {
            var resp = await _httpClient.GetAsync("courses");
            if (!resp.IsSuccessStatusCode)
            {
                return null;
            }
            
           var respsuccess =await resp.Content.ReadFromJsonAsync<Response<List<CourseViewModel>>>();
            return respsuccess.Data;
        }

        public async Task<CourseViewModel> GetByCourseId(string courseid)
        {
            var resp = await _httpClient.GetAsync($"courses/GetByUserId/{courseid}");
            if (!resp.IsSuccessStatusCode)
            {
                return null;
            }

            var respsuccess = await resp.Content.ReadFromJsonAsync<Response<CourseViewModel>>();
            return respsuccess.Data;
        }

        public async Task<CourseViewModel> GetCourseByUserIdAsync(string userid)
        {
            var resp = await _httpClient.GetAsync($"courses/GetByUserId/{userid}");
            if (!resp.IsSuccessStatusCode)
            {
                return null;
            }

            var respsuccess = await resp.Content.ReadFromJsonAsync<Response<CourseViewModel>>();
            return respsuccess.Data;
        }

        public async Task<bool> UpdateCourse(CourseUpdatePut course)
        {
            var response = await _httpClient.PutAsJsonAsync("courses", course);
            return response.IsSuccessStatusCode;
        }
    }
}

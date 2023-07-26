using AutoMapper;
using FreeCourse.Services.Catalog.Dtos;
using FreeCourse.Services.Catalog.Model;
using FreeCourse.Services.Catalog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;
using System.Runtime.CompilerServices;

namespace FreeCourse.Services.Catalog.Services
{
    public class CourseService:ICourseService
    {
        private readonly IMongoCollection<Course> _courseCollection;
        private readonly IMongoCollection<Category> _categoryCollection;

                

        private readonly IMapper _mapper;

        public CourseService(IDatabaseSettings databaseSettings, IMapper    mapper)
        {
            var client= new MongoClient(databaseSettings.ConnectionString);
            var database = client.GetDatabase(databaseSettings.DatabaseName);

            _courseCollection = database.GetCollection<Course>(databaseSettings.CourseCollectionName);
            _categoryCollection=database.GetCollection<Category>(databaseSettings.CategoryCollectionName);

            _mapper = mapper;
        }

        public async Task<Response<List<CourseDto>>> GetAllAsync()
        {
            var courses = await _courseCollection.Find(x => true).ToListAsync();

           
            if (courses.Any())
            {

                foreach(var course in courses)
                {
                        course.category= await _categoryCollection.Find(x=>x.Id==course.CategoryId).FirstAsync();   
                }
            }

            else
            {
                courses = new List<Course>(); 
            }
            
            return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses),200);
        }

        public async Task<Response<CourseDto>> GetByID(string id)
        {

            var course = await _courseCollection.Find(x => x.Id == id).FirstAsync();
            if(course == null)
            {
                return Response<CourseDto>.Fail("Course not found", 404);
            }
            course.category=await _categoryCollection.Find<Category>(x=>x.Id==course.CategoryId).FirstAsync();
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(course),200);
        }   

        public async Task<Response<List<CourseDto>>> GetAllByUserId(string userid)
        {
            var courses = await _courseCollection.Find(x => x.UserId == userid).ToListAsync();

            if (courses.Any())
            {
                foreach(var course in courses)
                {
                    course.category= await _categoryCollection.Find<Category>(x=>x.Id == course.CategoryId).FirstAsync();

                }
            }
            else
            {
                courses = new List<Course>();
            }

             return Response<List<CourseDto>>.Success(_mapper.Map<List<CourseDto>>(courses), 200);
        }

        public async Task<Response<CourseDto>> CourseCreateAsync(CourseCreateDto course)
        {
            var newcourse=_mapper.Map<Course>(course);
            newcourse.CreatedDate = DateTime.Now;
            await _courseCollection.InsertOneAsync(newcourse);
            return Response<CourseDto>.Success(_mapper.Map<CourseDto>(newcourse), 200);
        }

        public async Task<Response<NoContent>> CourseUpdateAsync(CourseUpdateDto course)
        {

            var update = _mapper.Map<Course>(course);
            var result = await _courseCollection.FindOneAndReplaceAsync(x => x.Id == course.id, update);

            if (result == null)
            {
                return Response<NoContent>.Fail("not found",404);
            }

            return Response<NoContent>.Success(204);
        }

        public async Task<Response<NoContent>> CourseDeleteAsync(string id)
        {

            var result = await _courseCollection.DeleteOneAsync(x => x.Id == id);
                
            if(result.DeletedCount>0 ) 
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("not deleted", 404);
            }
        }


    }
}

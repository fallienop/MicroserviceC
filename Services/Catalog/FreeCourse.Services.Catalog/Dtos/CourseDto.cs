using FreeCourse.Services.Catalog.Model;
using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FreeCourse.Services.Catalog.Dtos
{
    public class CourseDto
    {
        public string Id { get; set; }
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Picture { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UserId { get; set; }

        public FeatureDto feature { get; set; }


        public string CategoryId { get; set; }

        public string Description { get; set; }

        public CategoryDto category { get; set; }
    }
}

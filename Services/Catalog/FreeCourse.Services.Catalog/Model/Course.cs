using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace FreeCourse.Services.Catalog.Model
{
     class Course
    {
        [BsonId]
        [BsonRepresentation(BsonType.ObjectId)]
        public string Id { get; set; }
        public string Name { get; set; }

        [BsonRepresentation(BsonType.Decimal128)]
        public decimal Price { get; set; }

        public string Picture {get; set; }

        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedDate { get; set; }

        public string UserId { get; set; }

        public Feature feature { get; set; }


        [BsonRepresentation(BsonType.ObjectId)]
        public string CategoryId { get; set; }  

        public string Description { get; set; }

        [BsonIgnore]
        public Category category { get; set; }  

    }
}

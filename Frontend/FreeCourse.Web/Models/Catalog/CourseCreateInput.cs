namespace FreeCourse.Web.Models.Catalog
{
    public class CourseCreateInput
    {
        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Picture { get; set; }

        public DateTime CreatedDate { get; set; }

        public string UserId { get; set; }

        public FeatureViewModel feature { get; set; }


        public string CategoryId { get; set; }

        public string Description { get; set; }

    }
}

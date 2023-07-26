namespace FreeCourse.Services.Basket.Dtos
{
    public class BasketDto
    {
        public string UserId { get; set; }
        public string DiscountCode { get; set;}
        public List<BasketItemDto> Items { get; set; }  
        public decimal Total 
        {
            get => Items.Sum(x => x.Price);
        }

    }
}

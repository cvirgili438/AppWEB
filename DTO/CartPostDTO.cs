namespace AppWEB.DTO
{
    public class CartPostDTO
    {
        public int ProductId { get; set; }
        public string UserName { get; set; } = null!;
        public int Quantity { get; set; }

    }
}

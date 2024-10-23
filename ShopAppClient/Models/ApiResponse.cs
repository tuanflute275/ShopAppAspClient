namespace ShopAppClient.Models
{
    public class ApiResponse
    {
        public int Status { get; set; }
        public string Message { get; set; }
        public List<Category> Data { get; set; }
    }
}

namespace ManageRevenue.Domain.Models
{
    public class CategoryViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Type { get; set; } 
        public bool IsPersonal { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }

}

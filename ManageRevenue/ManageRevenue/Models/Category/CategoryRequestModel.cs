using System.Runtime.CompilerServices;

namespace ManageRevenue.Models.Category
{
    public class CategoryRequestModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Type { get; set; }
        public string Color { get; set; } = string.Empty;
    }
}

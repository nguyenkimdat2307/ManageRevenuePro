namespace ManageRevenue.Models.Category
{
    public class UpdateCategoryRequestModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Type { get; set; }
        public string Color { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }
}

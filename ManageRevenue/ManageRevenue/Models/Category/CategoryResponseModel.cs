namespace ManageRevenue.Models.Category
{
    public class CategoryResponseModel
    {
        public List<CategoryCollect> ListCategoryCollect {  get; set; }
        public List<CategorySpend> ListCategorySpend {  get; set; }

    }
    public class CategoryCollect
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Type { get; set; }
        public string Color { get; set; } = string.Empty;

        public string Icon {get; set; } = string.Empty;
    }
    public class CategorySpend
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public bool Type { get; set; }
        public string Color { get; set; } = string.Empty;
        public string Icon { get; set; } = string.Empty;
    }
}

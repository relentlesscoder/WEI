namespace WEI.Web.Models
{
    public class MenuItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int? ParentMenuItemId { get; set; }
        public string Url { get; set; }
        public int Position { get; set; }
    }
}
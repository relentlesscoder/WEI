namespace WEI.Domain.Model
{
    public class MenuItem
    {
        public int MenuItemId { get; set; }
        public string Name { get; set; }
        public int? ParentMenuItemId { get; set; }
        public string Url { get; set; }
        public int Position { get; set; }
    }
}

using System.Collections.Generic;

namespace WEI.Domain.Model
{
    /// <summary>
    /// Domain Tag object
    /// </summary>
    public class Tag
    {
        public int TagId { get; set; }
        public string Name { get; set; }
        public virtual List<Article> Articles { get; set; } 
    }
}

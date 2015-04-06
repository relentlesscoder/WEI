using System;
using System.Collections.Generic;

namespace WEI.Domain.Model
{
    /// <summary>
    /// Domain Article object
    /// </summary>
    public class Article
    {
        public int ArticleId { get; set; }
        public string Title { get; set; }
        public string Body { get; set; }
        public DateTime PublishDate { get; set; }
        public bool IsPublished { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsFeatured { get; set; }
        public string Deck { get; set; }
        public int Count { get; set; }
        public string EntryContent { get; set; }
        public string ImageUrl { get; set; }
        public int ArticleTypeId { get; set; }
        public virtual List<Tag> Tags { get; set; }
    }
}

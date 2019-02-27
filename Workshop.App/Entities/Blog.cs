using System.Collections.Generic;

namespace Workshop.App.Entities
{
    public class Blog
    {
        public int BlogId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public ICollection<Post> Posts { get; set; }
    }
}

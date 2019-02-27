using System.Collections.Generic;

namespace Workshop.App.Models
{
    public class Blog
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public IEnumerable<Post> Posts { get; set; }
    }
}

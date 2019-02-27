using System.Collections.Generic;
using System.Threading.Tasks;
using Workshop.App.Models;

namespace Workshop.App.Repositories
{
    public interface IBlogRepository
    {
        Task<IEnumerable<Blog>> Read();
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coda.Objects;

namespace Coda
{
    public interface IBlogRepository
    {
        List<Post> Posts(int pageNo, int pageSize);
        int TotalPosts();
    }
}


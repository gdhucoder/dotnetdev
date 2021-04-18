using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DapperAdvancedDemo.Model
{
    public class User
    {
        public string UserName { get; set; }
    }

    public class PagedResults<T>
    {
        public IEnumerable<T> Items { get; set; }
        public int Total { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public abstract class Pagination
    {
        public static int PageSize { get; set; }
        public static int RecordCount { get; set; }

        public static int GetNumberOfPages()
        {
            return (int)Math.Ceiling((double)RecordCount / PageSize);
        }
    }
}

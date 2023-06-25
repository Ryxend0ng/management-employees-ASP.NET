using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MisaWeb05.Core.Dto
{
    public abstract class APage
    {
        public int pageNumber;
        public int pageSize;
        public long totalElements;
        public int totalPages;
        public abstract int GetTotalPages();
        public abstract long GetTotalElements();
        public abstract int GetPageSize();
        public abstract int GetPageNumber();
    }
}

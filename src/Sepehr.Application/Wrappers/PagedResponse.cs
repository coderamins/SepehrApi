using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Sepehr.Application.Wrappers
{
    public class PagedResponse<T> : Response<T>
    {
        public int PageNumber { get; set; }

        public int PageSize { get; set; }
        public int TotalCount { get; set; }

        public PagedResponse(T data, int pageNumber, int pageSize,int totalCount=0)
        {
            this.PageNumber = pageNumber;
            this.PageSize = pageSize;
            this.Data = data;
            this.Message = null;
            this.Succeeded = true;
            this.Errors=null;
            this.TotalCount = totalCount;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.ShareHolders.Queries.GetAllShareHolders
{
    public class GetAllShareHoldersParameter :RequestParameter
    {
        public int? ShareHolderCode { get; set; }
    }
}
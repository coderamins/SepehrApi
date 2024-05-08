using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.PettyCashs.Queries.GetAllPettyCashs
{
    public class GetAllPettyCashsParameter :RequestParameter
    {
        public int? PettyCashId { get; set; }
    }
}
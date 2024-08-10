using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.UnloadingPermits.Queries.GetAllUnloadingPermits
{
    public class GetAllUnloadingPermitsParameter :RequestParameter
    {
        public int? UnloadingPermitCode { get; set; }
    }
}
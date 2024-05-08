using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.LadingExitPermits.Queries.GetAllLadingExitPermits
{
    public class GetAllLadingExitPermitsParameter :RequestParameter
    {
        public int? LadingPermitId { get; internal set; }
    }
}
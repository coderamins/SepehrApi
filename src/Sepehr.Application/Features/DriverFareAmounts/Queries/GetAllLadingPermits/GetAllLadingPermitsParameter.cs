using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.LadingPermits.Queries.GetAllLadingPermits
{
    public class GetAllLadingPermitsParameter :RequestParameter
    {
        public bool? HasExitPermit { get; set; }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.CashDesks.Queries.GetAllCashDesks
{
    public class GetAllCashDesksParameter :RequestParameter
    {
        public int? CashDeskId { get; set; }
    }
}
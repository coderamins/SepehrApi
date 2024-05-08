using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;

namespace Sepehr.Application.Features.CustomerOfficialCompanys.Queries.GetAllCustomerOfficialCompanys
{
    public class GetAllCustomerOfficialCompanysParameter :RequestParameter
    {
        public bool? IsActive { get; set; }
        public Guid? CustomerId { get; set; }
    }
}
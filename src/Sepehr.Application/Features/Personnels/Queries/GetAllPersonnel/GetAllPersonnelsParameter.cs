using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Sepehr.Application.Parameters;
using Sepehr.Domain.Enums;

namespace Sepehr.Application.Features.Personnels.Queries.GetAllPersonnels
{
    public class GetAllPersonnelsParameter : RequestParameter
    {
        public int? PersonnelCode { get; set; }
        public string? PersonnelName { get; set; } = string.Empty;
        public string? PhoneNumber { get; set; }=string.Empty;
        public string? NationalCode { get; set; } = string.Empty;
    }
}
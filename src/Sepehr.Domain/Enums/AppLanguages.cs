using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.Enums
{
    public enum AppLanguages
    {
        [Display(Name = "EN")]
        EN = 1,
        [Display(Name = "SP")]
        SP = 2,
        [Display(Name = "AR")]
        AR = 3
    }
}

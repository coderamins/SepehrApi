using Sepehr.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class ApplicationMenuNameViewModel
    {
        public string MenuName { get; set; } = string.Empty;
        public AppLanguages Language { get; set; }
    }
}

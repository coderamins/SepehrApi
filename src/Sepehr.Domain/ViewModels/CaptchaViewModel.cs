using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Domain.ViewModels
{
    public class CaptchaViewModel
    {
        public string CImage { get; set; }
        public DateTime ExpireTime { get; set; }
        public string Key { get; set; }
    }
}

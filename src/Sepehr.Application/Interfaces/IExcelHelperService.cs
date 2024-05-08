using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.Interfaces
{
    public interface IExcelHelperService<T> where T : class
    {
        public List<T> Import<T>(string filePath);
    }
}

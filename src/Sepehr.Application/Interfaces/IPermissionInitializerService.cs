using Sepehr.Domain.Entities.UserEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Application.Interfaces
{
    public interface IPermissionInitializerService : IGenericRepositoryAsync<Permission>
    {
        Task<bool> CheckPermissionExists(string policy);
    }
}

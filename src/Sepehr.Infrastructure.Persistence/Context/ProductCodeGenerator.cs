using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using Sepehr.Domain.Entities;

namespace Sepehr.Infrastructure.Persistence.Context
{
    public class ProductCodeGenerator : ValueGenerator<int>
    {
        public override bool GeneratesTemporaryValues => throw new NotImplementedException();

        public override int Next(EntityEntry entry)
        {
            if(entry.Entity==typeof(Product))
            {

            }
            throw new NotImplementedException();
        }

    }
}
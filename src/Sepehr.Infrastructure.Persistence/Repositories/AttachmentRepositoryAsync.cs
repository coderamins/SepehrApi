using Microsoft.EntityFrameworkCore;
using Sepehr.Application.Interfaces.Repositories;
using Sepehr.Domain.Entities;
using Sepehr.Infrastructure.Persistence.Context;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class AttachmentRepositoryAsync : GenericRepositoryAsync<Attachment>, IAttachmentRepositoryAsync
    {
        private readonly DbSet<Attachment> _attachment;

        public AttachmentRepositoryAsync(ApplicationDbContext dbContext) : base(dbContext)
        {
            _attachment = dbContext.Set<Attachment>();
        }

        
    }
}
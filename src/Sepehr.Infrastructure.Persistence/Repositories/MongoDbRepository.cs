using MongoDB.Driver;
using Sepehr.Application.Interfaces.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sepehr.Infrastructure.Persistence.Repositories
{
    public class MongoDbRepository:IMongoDbRepository
    {
        private readonly IMongoCollection<Order> _orders;

        public MongoDbRepository(IMongoDatabase database)
        {
            _orders = database.GetCollection<Order>("Orders");
        }

        public async Task<Order> GetByIdAsync(Guid id)
        {
            return await _orders.Find(x => x.Id == id).FirstOrDefaultAsync();
        }
    }
}

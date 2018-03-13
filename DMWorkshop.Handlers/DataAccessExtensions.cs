using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DMWorkshop.Handlers
{
    public static class DataAccessExtensions
    {
        public static async Task Save<T>(this IMongoDatabase database, string collectionName, Expression<Func<T, bool>> match, T entity, CancellationToken cancellationToken)
        {
            var collection = database.GetCollection<T>(collectionName);
            
            var replaceOneResult = await collection.ReplaceOneAsync(
                match,
                entity,
                new UpdateOptions { IsUpsert = true },
                cancellationToken);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Azure.Documents;

namespace Backend.API.CosmosDB.Data.Services.Infrastructure
{
    internal interface IDocumentDbRepository<T> where T : Document
    {
        T Get(Expression<Func<T, bool>> predicate);
        T GetById(long id);
        IEnumerable<T> Find(Expression<Func<T, bool>> predicate);
        Task<T> CreateAsync(T entity);
        Task<T> UpdateAsync(string id, T entity);
        Task DeleteAsync(long id);
        String DatabaseId { get; }
        String CollectionId { get; }
        DocumentCollection GetOrCreateCollection(string databaseLink, string collectionId);
        Database GetOrCreateDatabase(string databaseId);
    }
}
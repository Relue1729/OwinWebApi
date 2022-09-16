using OwinWebApi.Common;
using OwinWebApi.Interfaces;
using Polly;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace OwinWebApi.Repositories
{
    public class InMemoryRepository : IRepository
    {
        private ConcurrentDictionary<int, int> storage { get; set; } = new ConcurrentDictionary<int, int>();
        public async Task IncrementAtIndexAsync(int index)
        {
            var retryPolicy = Policy
                .Handle<DataAccessException>()
                .WaitAndRetry(5, x => TimeSpan.FromMilliseconds(50));

            retryPolicy.Execute(() =>
            {
                if (storage.TryGetValue(index, out var value))
                {
                    if (!storage.TryUpdate(index, storage[index] + 1, storage[index]))
                        throw new DataAccessException($"Can't access InMemory storage at index {index}");
                }
                else
                {
                    if (!storage.TryAdd(index, 1))
                        throw new DataAccessException($"Can't access InMemory storage at index {index}");
                }
            });
        }
        public async Task<int?> GetValue(int index)
        {
            if (storage.TryGetValue(index, out var result))
                return result;

            return null;
        }
    }
}
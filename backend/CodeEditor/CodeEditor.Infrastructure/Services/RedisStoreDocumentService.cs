using CodeEditor.Domain.Services.Interfaces;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Text;

namespace CodeEditor.Infrastructure.Services
{
    public class RedisStoreDocumentService : IStoreDocumentService
    {
        private readonly IDatabase _redis;

        public RedisStoreDocumentService(IConnectionMultiplexer redis)
        {
            _redis = redis.GetDatabase();
        }

        public async Task<string?> GetDocumentAsync(string documentId)
        {
            var content = await _redis.StringGetAsync($"document:{documentId}");
            return content.HasValue ? content.ToString() : null;
        }

        public async Task<bool> SaveDocumentAsync(string documentId, string content)
        {
            return await _redis.StringSetAsync($"document:{documentId}", content);
        }

        public async Task<bool> DeleteDocumentAsync(string documentId)
        {
            return await _redis.KeyDeleteAsync($"document:{documentId}");
        }
    }
}

using DepartmentsAPI.Services.Interfaces;
using StackExchange.Redis;
using System.Text.Json;

namespace DepartmentsAPI.Services
{
    public class CacheService : ICacheService
    {
        private readonly IDatabase database;
        private readonly IConfiguration configuration;

        public CacheService(IConfiguration configuration)
        {
            var redis = ConnectionMultiplexer.Connect(configuration.GetSection("Redis").ToString());
            database = redis.GetDatabase();

            this.configuration = configuration;
        }

        public async ValueTask<T> GetAsync<T>(string key)
        {
            var data = await database.StringGetAsync(key);

            if (!string.IsNullOrEmpty(data))
            {
                return JsonSerializer.Deserialize<T>(data);
            }

            return default!;
        }

        public async ValueTask<object?> RemoveAsync(string key)
        {
            var isExists = await database.KeyExistsAsync(key);

            if (isExists)
            {
                return await database.KeyDeleteAsync(key);
            }

            return false;
        }

        public async ValueTask<bool> SetAsync<T>(string key, T value, DateTimeOffset expirationTime)
        {
            var expiryTime = expirationTime.DateTime.Subtract(DateTime.UtcNow);
            var isSet = default(bool);
            if (value != null)
            {
                isSet = await database.StringSetAsync(key, JsonSerializer.Serialize(value), expiryTime);
            }
            return isSet;
        }
    }
}

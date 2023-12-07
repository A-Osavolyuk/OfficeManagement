namespace EmployeesAPI.Services.Interfaces
{
    public interface ICacheService
    {
        ValueTask<T> GetAsync<T>(string key);
        ValueTask<bool> SetAsync<T>(string key, T value, DateTimeOffset expirationTime);
        ValueTask<object?> RemoveAsync(string  key);
    }
}

using FluentAssertions;

namespace DepartmentAPI.tests
{
    public class CacheServiceTests
    {
        public readonly Mock<CacheService> _cacheService = new();

        [Fact]
        public async void IsCachingServiceHasConnectionWithRedis()
        {
            //arrange
            var testData = "test";

            var key = "test-data";
            var cacheService = _cacheService.Object;

            //act
            var isSucceeded = await cacheService.SetAsync(key, testData, DateTimeOffset.UtcNow.AddMinutes(1));
            await cacheService.RemoveAsync(key);

            //assert
            isSucceeded.Should().BeTrue();
        }

        [Fact]
        public async void CanCachingServiceWriteAndReadToRedis()
        {
            //arrange
            var testData = "test";

            var key = "test-data";
            var cacheService = _cacheService.Object;

            //act
            await cacheService.SetAsync(key, testData, DateTimeOffset.UtcNow.AddMinutes(1));
            var resultData = await cacheService.GetAsync<string>(key);
            await cacheService.RemoveAsync(key);

            //assert
            resultData.Should().Be(testData);
        }

        [Fact]
        public async void CanCachingServiceDeleteDataFromRedis()
        {
            //arrange
            var testData = "test";

            var key = "test-data";
            var cacheService = _cacheService.Object;

            //act
            await cacheService.SetAsync(key, testData, DateTimeOffset.UtcNow.AddMinutes(1));
            await cacheService.RemoveAsync(key);
            var resultData = await cacheService.GetAsync<string>(key);

            //assert
            resultData.Should().BeNull();
        }
    }
}
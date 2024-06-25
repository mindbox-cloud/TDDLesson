using FluentAssertions;
using Moq;
using TDDLesson;

namespace TestProject1;

[TestClass]
public class UnitTest1
{
    [TestMethod]
    public async Task HybridCache_ItemExistsInMemoryCache()
    {
        // Arrange
        const string cacheKey = "cache-key";
        const string cacheValue = "value";

        var memoryCacheMock = new Mock<IMemoryCache<string, string>>();
        var redisCacheMock = new Mock<IRedisCache<string, string>>();
        memoryCacheMock
            .Setup(m => m.TryGetAsync(cacheKey))
            .ReturnsAsync(cacheValue);

        var cache = new HybridCache<string, string>(
            memoryCacheMock.Object,
            redisCacheMock.Object,
            Mock.Of<IRepository<string, string>>(),
            TimeSpan.FromMinutes(5));

        // Act
        var cachedItem = await cache.GetOrAddAsync(cacheKey);

        // Assert
        cachedItem.Should().Be(cacheValue);
        memoryCacheMock.Verify(m => m.TryGetAsync(cacheKey), Times.Once);
        redisCacheMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task HybridCache_ItemDoesNotExistInMemoryCache()
    {
        // Arrange
        const string cacheKey = "cache-key";
        const string cacheValue = "value";

        var memoryCacheMock = new Mock<IMemoryCache<string, string>>();
        var redisCacheMock = new Mock<IRedisCache<string, string>>();
        memoryCacheMock
            .Setup(m => m.TryGetAsync(cacheKey))
            .ReturnsAsync(null as string);
        redisCacheMock
            .Setup(m => m.TryGetAsync(cacheKey))
            .ReturnsAsync(cacheValue);

        var cache = new HybridCache<string, string>(
            memoryCacheMock.Object,
            redisCacheMock.Object,
            Mock.Of<IRepository<string, string>>(),
            TimeSpan.FromMinutes(5));

        // Act
        var cachedItem = await cache.GetOrAddAsync(cacheKey);

        // Assert
        cachedItem.Should().Be(cacheValue);
        memoryCacheMock.Verify(m => m.TryGetAsync(cacheKey), Times.Once);
        memoryCacheMock.Verify(m => m.AddAsync(cacheKey, cacheValue), Times.Once);
        redisCacheMock.Verify(m => m.TryGetAsync(cacheKey), Times.Once);
    }
    
    [TestMethod]
    public async Task HybridCache_ItemDoesNotExistInCaches_SQLRequest_AddValuesToCaches()
    {
        // Arrange
        const string cacheKey = "cache-key";
        const string cacheValue = "value";

        var memoryCacheMock = new Mock<IMemoryCache<string, string>>();
        var redisCacheMock = new Mock<IRedisCache<string, string>>();
        var repositoryMock = new Mock<IRepository<string, string>>();
        memoryCacheMock
            .Setup(m => m.TryGetAsync(cacheKey))
            .ReturnsAsync(null as string);
        redisCacheMock
            .Setup(m => m.TryGetAsync(cacheKey))
            .ReturnsAsync(null as string);
        repositoryMock
            .Setup(m => m.TryGetAsync(cacheKey))
            .ReturnsAsync(cacheValue);

        var cache = new HybridCache<string, string>(
            memoryCacheMock.Object,
            redisCacheMock.Object,
            repositoryMock.Object,
            TimeSpan.FromMinutes(5));

        // Act
        var cachedItem = await cache.GetOrAddAsync(cacheKey);

        // Assert
        cachedItem.Should().Be(cacheValue);
        //memoryCacheMock.Verify(m => m.TryGetAsync(cacheKey), Times.Once);
        memoryCacheMock.Verify(m => m.AddAsync(cacheKey, cacheValue), Times.Once);
        redisCacheMock.Verify(m => m.AddAsync(cacheKey, cacheValue), Times.Once);
        //redisCacheMock.Verify(m => m.TryGetAsync(cacheKey), Times.Once);
    }

    [TestMethod]
    public async Task HybridCache_ItemDoesNotExistInCachesAndDatabase_ReturnNull()
    {
        
    }
}
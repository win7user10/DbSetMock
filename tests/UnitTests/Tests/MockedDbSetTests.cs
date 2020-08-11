using DbSetMock;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using UnitTests.Infrastructure;
using Xunit;

namespace UnitTests
{
    public class MockedDbSetTests
    {
        private readonly TestDbContext _db;

        public MockedDbSetTests()
        {
            _db = new TestDbContext();
        }

        [Fact]
        public async Task FirstOrDefaultAsync()
        {
            _db.TestEntities = new[] { new TestEntity { Field = "123" } }.CreateMockedDbSet();

            var query = "some query which should be mocked";
            var testEntity = await _db.TestEntities
                .FromSqlRaw(query)
                .AsNoTracking()
                .FirstOrDefaultAsync();

            Assert.NotNull(testEntity);
        }

        [Fact]
        public async Task ToListAsync()
        {
            _db.TestEntities = new[] { new TestEntity { Field = "123" }, new TestEntity { Field = "123" } }.CreateMockedDbSet();

            var query = "some query which should be mocked";
            var testEntities = await _db.TestEntities
                .FromSqlRaw(query)
                .AsNoTracking()
                .ToListAsync();

            Assert.Equal(2, testEntities.Count);
        }
    }
}

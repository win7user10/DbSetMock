using Microsoft.EntityFrameworkCore;

namespace UnitTests.Infrastructure
{
    public class TestDbContext
    {
        public DbSet<TestEntity> TestEntities { get; set; }
    }
}

using Microsoft.EntityFrameworkCore;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace DbSetMock
{
    public static class DbSetExtensions
    {
        public static DbSet<T> CreateMockedDbSet<T>(this IEnumerable<T> spItems) where T : class
        {
            return CreateMockFromDbSet(spItems).Object;
        }

        public static Mock<DbSet<T>> CreateMockFromDbSet<T>(this IEnumerable<T> spItems) where T : class
        {
            var queryProviderMock = new Mock<IQueryProvider>();
            queryProviderMock.Setup(p => p.CreateQuery<T>(It.IsAny<MethodCallExpression>()))
                .Returns<MethodCallExpression>(x => new AsyncEnumerable<T>(spItems));

            var dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>()
                .SetupGet(q => q.Provider)
                .Returns(() => queryProviderMock.Object);

            dbSetMock.As<IQueryable<T>>()
                .Setup(q => q.Expression)
                .Returns(Expression.Constant(dbSetMock.Object));

            return dbSetMock;
        }
    }
}
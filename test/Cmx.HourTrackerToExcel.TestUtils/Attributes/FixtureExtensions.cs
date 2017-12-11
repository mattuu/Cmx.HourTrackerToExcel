using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Moq;
using Ploeh.AutoFixture;

namespace J2BI.Holidays.PCPS.TestUtils.Attributes
{
    public static class FixtureExtensions
    {
        public static IDbSet<TEntity> SetupDbMock<TEntity>(this IFixture fixture, IEnumerable<TEntity> collection) where TEntity : class
        {
            var queryable = collection.AsQueryable();
            var dbSetMock = new Mock<IDbSet<TEntity>>();
            dbSetMock.Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.Setup(m => m.GetEnumerator()).Returns(queryable.GetEnumerator());
            dbSetMock.Setup(m => m.ElementType).Returns(queryable.ElementType);

            return dbSetMock.Object;
        }
    }
}
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace TokenGenerator.Tests
{
    public static class Globals
    {
        public static Mock<DbSet<T>> MockDbSet<T>(IEnumerable<T> list) where T : class, new()
        {
            IQueryable<T> queryableList = list.AsQueryable();
            Mock<DbSet<T>> dbSetMock = new Mock<DbSet<T>>();
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Provider).Returns(queryableList.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.Expression).Returns(queryableList.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.ElementType).Returns(queryableList.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(x => x.GetEnumerator()).Returns(() => queryableList.GetEnumerator());
            dbSetMock.Setup(x => x.FindAsync()).Returns(new System.Threading.Tasks.ValueTask<T>());
            dbSetMock.Setup(x => x.Add(It.IsAny<T>())).Callback<T>((s) => queryableList.Append(s));

            return dbSetMock;
        }

        public static Mock<DbSet<T>> MockDbSetWrongResult<T>(IEnumerable<T> list) where T : class, new()
        {
            {
                IQueryable<T> queryableList = list.AsQueryable();
                Mock<DbSet<T>> dbSetMock = new Mock<DbSet<T>>();
                dbSetMock.Setup(x => x.FindAsync()).Returns(null);

                return dbSetMock;
            }
        }
    }
}
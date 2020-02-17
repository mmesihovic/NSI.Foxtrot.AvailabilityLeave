using Moq;
using System.Collections.Generic;
using NSI.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace NSI.Test.Mocks {
    public class MockDbSet<T> where T : class{ 
        public static Mock<DbSet<T>> MockQuery(List<T> data){
            
            Mock<DbSet<T>> mock = new Mock<DbSet<T>>();

            IQueryable<T> queryable = data.AsQueryable();

            mock.As<IQueryable<T>>().Setup( x => x.Provider).Returns(queryable.Provider);
            mock.As<IQueryable<T>>().Setup( x => x.Expression).Returns(queryable.Expression);
            mock.As<IQueryable<T>>().Setup( x => x.ElementType).Returns(queryable.ElementType);
            mock.As<IQueryable<T>>().Setup( x => x.GetEnumerator()).Returns(queryable.GetEnumerator());

            return mock;

        }

    }
}
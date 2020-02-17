using Moq;
using Xunit;
using System.Linq;
using System.Collections.Generic;
using NSI.DAL;
using NSI.DAL.Entities;
using NSI.DAL.Repository;
using Microsoft.EntityFrameworkCore;
using NSI.Test.Mocks;

namespace NSI.Test.RepositoryTests {
    public class EventTypeTests {
        // [Fact]
        // public void GetByNameTest(){
        //     Mock<AvailabilityLeaveContext> context = new Mock<AvailabilityLeaveContext>();

        //     List<EventType> data = new List<EventType>();
        //     data.Add(new EventType { ID = 1, Name = "LEAVE"});
        //     Mock<DbSet<EventType>> dbSet = MockDbSet<EventType>.MockQuery(data); 

        //     context.
        //     context.Setup( x => x.EventTypes ).Returns(dbSet.Object);

        //     EventTypeRepository eventTypeRepository = new EventTypeRepository(context.Object);

        //     List<EventType> eventTypes = eventTypeRepository.GetAll().ToList();
        //     Assert.Single(eventTypes);
        // }
       
    }

}

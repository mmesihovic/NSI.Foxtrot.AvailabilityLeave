using Xunit;
using NSI.BLL.Services;
using NSI.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using NSI.Test.Mocks.Repositories;

namespace NSI.Test.ServiceTests {

    public class EventTypeTests {
        
        [Theory]
        [InlineData(1, "LEAVE")]
        [InlineData(2, "ABSENCE")]
        public void GetAllTest(long ID, string Name){
            
            MockEventTypeRepository eventTypeRepository = new MockEventTypeRepository();
            
            eventTypeRepository.MockGetAll(ID, Name);

            EventTypeService eventTypeService = new EventTypeService(eventTypeRepository.Object);

            List<EventType> eventTypes =  eventTypeService.GetAll().ToList();

            Assert.Single(eventTypes);
            Assert.Equal(ID, eventTypes[0].ID);
            Assert.Equal(Name, eventTypes[0].Name);
        }
    }    

}
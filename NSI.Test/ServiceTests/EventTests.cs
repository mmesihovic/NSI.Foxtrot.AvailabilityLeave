using Xunit;
using System.Collections.Generic;
using System.Linq;
using NSI.Test.Mocks.Repositories;
using NSI.DAL.Entities;
using NSI.BLL.Services;

namespace NSI.Test.ServiceTests {

    public class EventTests {
        
        [Theory]
        [InlineData(1, "LEAVE", 1, "Meeting")]
        [InlineData(2, "ABSENCE", 2, "Meeting")]
        public void GetAllTest(long ID, string Name, long typeID, string typeName){

            MockEventTypeRepository eventTypeRepository = new MockEventTypeRepository();
            MockEventRepository eventRepository = new MockEventRepository();

            eventRepository.MockGetAll(ID, Name, typeID, typeName);

            EventService eventService = new EventService(eventRepository.Object, eventTypeRepository.Object);

            List<Event> events = eventService.GetAll().ToList();
            Assert.Single(events);
            Assert.Equal(ID, events[0].ID);
            Assert.Equal(Name, events[0].Name);
            Assert.Equal(typeID, events[0].Type.ID);
            Assert.Equal(typeName, events[0].Type.Name);
        }

        [Theory]
        [InlineData(1, "LEAVE", 1, "Meeting")]
        [InlineData(2, "ABSENCE", 2, "Meeting")]
        public void GetByIdTest(long ID, string Name, long typeID, string typeName){

            MockEventTypeRepository eventTypeRepository = new MockEventTypeRepository();
            MockEventRepository eventRepository = new MockEventRepository();

            eventRepository.MockGetById(ID, Name, typeID, typeName);

            EventService eventService = new EventService(eventRepository.Object, eventTypeRepository.Object);

            Event _event = eventService.GetById(ID);
            
            Assert.NotNull(_event);
            Assert.Equal(ID, _event.ID);
            Assert.Equal(Name, _event.Name);
            Assert.Equal(typeID, _event.Type.ID);
            Assert.Equal(typeName, _event.Type.Name);
        }
        
        [Theory]
        [InlineData(1, "LEAVE", 1, "Meeting")]
        [InlineData(2, "ABSENCE", 2, "Meeting")]
        public void CreateByNameTest(long ID, string Name, long typeID, string typeName){

            MockEventTypeRepository eventTypeRepository = new MockEventTypeRepository();
            MockEventRepository eventRepository = new MockEventRepository();
            
            eventTypeRepository.MockGetByName(typeID, typeName);
            eventRepository.MockCreate();

            EventService eventService = new EventService(eventRepository.Object, eventTypeRepository.Object);
            Event _event = new Event { ID = ID, Name = Name };

            Event createdEvent = eventService.Create(_event, null, typeName);

            Assert.NotNull(createdEvent.Type);
            Assert.Equal(ID, createdEvent.ID);
            Assert.Equal(Name, createdEvent.Name);
            Assert.Equal(typeID, createdEvent.Type.ID);
            Assert.Equal(typeName, createdEvent.Type.Name);
        }
        
        [Theory]
        [InlineData(1, "LEAVE", 1, "Meeting")]
        [InlineData(2, "ABSENCE", 2, "Meeting")]
        public void CreateByIdTest(long ID, string Name, long typeID, string typeName){
            MockEventTypeRepository eventTypeRepository = new MockEventTypeRepository();
            MockEventRepository eventRepository = new MockEventRepository();
            
            eventTypeRepository.MockGetById(typeID, typeName);
            eventRepository.MockCreate();

            EventService eventService = new EventService(eventRepository.Object, eventTypeRepository.Object);
            Event _event = new Event { ID = ID, Name = Name };

            Event createdEvent = eventService.Create(_event, typeID, null);

            Assert.NotNull(createdEvent.Type);
            Assert.Equal(ID, createdEvent.ID);
            Assert.Equal(Name, createdEvent.Name);
            Assert.Equal(typeID, createdEvent.Type.ID);
            Assert.Equal(typeName, createdEvent.Type.Name);

        }
        [Fact]
        public void CreateFail(){
            MockEventTypeRepository eventTypeRepository = new MockEventTypeRepository();
            MockEventRepository eventRepository = new MockEventRepository();
            
            // ADD one ID
            eventTypeRepository.MockGetById(1, "LEAVE");
            eventRepository.MockCreate();

            EventService eventService = new EventService(eventRepository.Object, eventTypeRepository.Object);
            Event _event = new Event { ID = 1, Name = "Meeting" };

            // Pass wrong id
            KeyNotFoundException exception = Assert.Throws<KeyNotFoundException>(() => {
                                                    eventService.Create(_event, 2, null);
                                                });
            Assert.Equal(exception.Message, "Either Type ID or Type Name was incorrect.");
        }

    }    

}
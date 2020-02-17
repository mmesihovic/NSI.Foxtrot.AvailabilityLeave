using Moq;
using NSI.DAL.Repository.Interfaces;
using NSI.DAL.Entities;
using System;
using System.Collections.Generic;

namespace NSI.Test.Mocks.Repositories {

    public class MockEventTypeRepository : Mock<IEventTypeRepository> {

        public MockEventTypeRepository MockGetAll(long ID, string Name){
            List<EventType> eventTypes = new List<EventType>();
            eventTypes.Add(new EventType{ID = ID, Name = Name});

            Setup(x => x.GetAll()).Returns(eventTypes);
            return this;
        }
        public MockEventTypeRepository MockGetByName(long ID, string Name){
            Setup(x => x.GetByName(It.Is<string>( name => String.Equals(name, Name) ))).Returns(new EventType{ ID = ID, Name = Name });
            return this;
        }
        public MockEventTypeRepository MockGetById(long ID, string Name){
            Setup(x => x.GetById(It.Is<long>( id => id == ID ))).Returns(new EventType{ ID = ID, Name = Name });
            return this;
        }
    }

}
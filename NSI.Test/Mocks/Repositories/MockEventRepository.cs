using Moq;
using NSI.DAL.Repository.Interfaces;
using NSI.DAL.Entities;
using System.Collections.Generic;

namespace NSI.Test.Mocks.Repositories {

    public class MockEventRepository : Mock<IEventRepository> {

        public MockEventRepository MockGetAll(long ID, string Name, long typeID, string typeName){
            List<Event> events = new List<Event>();
            events.Add(
                new Event { 
                    ID = ID, 
                    Name = Name,
                    Type = new EventType { ID = typeID, Name = typeName }
                }
            );
            
            Setup(x => x.GetAll()).Returns(events);
            return this;
        }

        public MockEventRepository MockGetById(long ID, string Name, long typeID, string typeName){
            Event _event =  new Event { 
                                ID = ID, 
                                Name = Name,
                                Type = new EventType { ID = typeID, Name = typeName }
                            };
            Setup(x => x.GetById(It.Is<long> ( id => id == ID ))).Returns(_event);
            return this;
        }
        public MockEventRepository MockCreate(){
            Setup(x => x.Create(It.IsAny<Event>())).Returns<Event>(_event => _event);
            return this;   
        }

    }

}
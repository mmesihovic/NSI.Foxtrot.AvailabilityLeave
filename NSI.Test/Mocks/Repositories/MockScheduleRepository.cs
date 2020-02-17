using Moq;
using NSI.DAL.Repository.Interfaces;
using NSI.DAL.Entities;
using System.Collections.Generic;
using System;
namespace NSI.Test.Mocks.Repositories {

    public class MockScheduleRepository : Mock<IScheduleRepository> {
        public MockScheduleRepository MockGetAll(long ID, long ResourceID, bool Approved, long? ApprovedByID, 
            Event Event, long RequstedByID, DateTime FromDate, DateTime UntilDate){
            List<ResourceSchedule> resourceSchedules = new List<ResourceSchedule>();
            resourceSchedules.Add(new ResourceSchedule {
                ID = ID,
                ResourceID = ResourceID,
                Approved = Approved,
                ApprovedByID = ApprovedByID,
                RequestByID = RequstedByID,
                Event = Event,
                FromDate = FromDate,
                UntilDate = UntilDate
            });
            
            Setup( x => x.GetAll()).Returns(resourceSchedules);
            return this;
        }

        public MockScheduleRepository MockGetById(long ID, long ResourceID, bool Approved, long? ApprovedByID, 
            Event Event, long RequstedByID, DateTime FromDate, DateTime UntilDate){
        
            Setup( x => x.GetById( It.Is<long> (id => id == ID))).Returns(new ResourceSchedule {
                ID = ID,
                ResourceID = ResourceID,
                Approved = Approved,
                ApprovedByID = ApprovedByID,
                RequestByID = RequstedByID,
                Event = Event,
                FromDate = FromDate,
                UntilDate = UntilDate
            });
            return this;
        }
        public MockScheduleRepository MockGetAllByResourceId(long ID, long ResourceID, bool Approved, long? ApprovedByID, 
            Event Event, long RequstedByID, DateTime FromDate, DateTime UntilDate){

            List<ResourceSchedule> resourceSchedules = new List<ResourceSchedule>();
            resourceSchedules.Add(new ResourceSchedule {
                ID = ID,
                ResourceID = ResourceID,
                Approved = Approved,
                ApprovedByID = ApprovedByID,
                RequestByID = RequstedByID,
                Event = Event,
                FromDate = FromDate,
                UntilDate = UntilDate
            });
            Setup( x => x.GetAllByResourceID(It.Is<long>(id => id == ResourceID))).Returns(resourceSchedules);
            return this;
        }
        public MockScheduleRepository MockGetByIdNull(){
            Setup( x => x.GetById( It.IsAny<long>())).Returns<ResourceSchedule>(null);
            return this;
        }
        public MockScheduleRepository MockUpdate(){
            Setup( x => x.Update( It.IsAny<ResourceSchedule>())).Returns<ResourceSchedule> ( schedule => schedule );
            return this;
        }
        public MockScheduleRepository MockCreate(){
            Setup( x => x.Create( It.IsAny<ResourceSchedule>())).Returns<ResourceSchedule> ( schedule => schedule );
            return this;
        }
    }

}
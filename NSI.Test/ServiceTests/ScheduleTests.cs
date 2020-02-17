using Xunit;
using System.Collections.Generic;
using NSI.Test.Mocks.Repositories;
using NSI.DAL.Entities;
using NSI.DataContracts.Models.Requests;
using NSI.BLL.Services;
using System;
using System.Linq;

namespace NSI.Test.ServiceTests {

    public class ScheduleTests {
        
        [Theory]
        [InlineData(1, 1, false, null, 1)]
        [InlineData(1, 1, true, 2, 1)]
        public void GetAllTest(long ID, long ResourceID, bool Approved, long? ApprovedByID, long RequstedByID){
            
            MockScheduleRepository scheduleRepository = new MockScheduleRepository();

            Event _event = new Event {
                ID = 1,
                Name = "Meeting",
                Type = new EventType {
                    ID = 1,
                    Name = "LEAVE"
                }
            };
            DateTime FromDate = DateTime.Now;
            DateTime UntilDate = DateTime.Now.AddHours(8);

            scheduleRepository.MockGetAll(ID, RequstedByID, Approved, ApprovedByID, _event, RequstedByID, FromDate, UntilDate);
            
            ScheduleService scheduleService = new ScheduleService(scheduleRepository.Object);

            List<ResourceSchedule> resourceSchedules =  scheduleService.GetAll().ToList();
            Assert.Single(resourceSchedules);
            Assert.Equal(ID, resourceSchedules[0].ID);
            Assert.Equal(RequstedByID, resourceSchedules[0].RequestByID);
            Assert.Equal(Approved, resourceSchedules[0].Approved);
            Assert.Equal(ApprovedByID, resourceSchedules[0].ApprovedByID);
        
        }

        [Theory]
        [InlineData(1, 1, false, null, 1)]
        [InlineData(1, 1, true, 2, 1)]
        public void GetByIdTest(long ID, long ResourceID, bool Approved, long? ApprovedByID, long RequstedByID){

            long eventID = 1;
            string eventName = "Meeting";
            long typeID = 2;
            string typeName = "LEAVE";

            DateTime FromDate = DateTime.Now;
            DateTime UntilDate = DateTime.Now.AddHours(8);

            Event _event = new Event {
                ID = eventID,
                Name = eventName,
                Type = new EventType {
                    ID = typeID,
                    Name = typeName
                }
            };
            
            MockScheduleRepository scheduleRepository = new MockScheduleRepository();

            scheduleRepository.MockGetById(ID, ResourceID, Approved, ApprovedByID, _event, RequstedByID, FromDate, UntilDate);
            
            ScheduleService scheduleService = new ScheduleService(scheduleRepository.Object);

            ResourceSchedule resourceSchedule =  scheduleService.GetById(ID);
            Assert.NotNull(resourceSchedule);
            Assert.Equal(ID, resourceSchedule.ID);
            Assert.Equal(ResourceID, resourceSchedule.ResourceID);
            Assert.Equal(RequstedByID, resourceSchedule.RequestByID);
            Assert.Equal(Approved, resourceSchedule.Approved);
            Assert.Equal(ApprovedByID, resourceSchedule.ApprovedByID);
            Assert.Equal(eventID, resourceSchedule.Event.ID);
            Assert.Equal(eventName, resourceSchedule.Event.Name);
            Assert.Equal(typeID, resourceSchedule.Event.Type.ID);
            Assert.Equal(typeName, resourceSchedule.Event.Type.Name);
            Assert.Equal(FromDate, resourceSchedule.FromDate);
            Assert.Equal(UntilDate, resourceSchedule.UntilDate);
            
        }
        [Theory]
        [InlineData(1)]
        public void ApproveFailTest(long ScheduleID){
            MockScheduleRepository scheduleRepository = new MockScheduleRepository();

            scheduleRepository.MockGetByIdNull();

            ScheduleService scheduleService = new ScheduleService(scheduleRepository.Object);

            ApproveRequest request = new ApproveRequest { ApprovedByID = 1, ScheduleID = ScheduleID };
            ResourceSchedule schedule = scheduleService.Approve(request);
            Assert.Null(schedule);
        }
        [Theory]
        [InlineData(1, 1, 1)]
        public void ApproveTest(long ID, long ScheduleID, long ApprovedByID){
            MockScheduleRepository scheduleRepository = new MockScheduleRepository();
            scheduleRepository.MockGetById(ID, 1, false, null, new Event(), 1, DateTime.Now, DateTime.Now)
                              .MockUpdate();

            ScheduleService scheduleService = new ScheduleService(scheduleRepository.Object);

            ApproveRequest request = new ApproveRequest { ApprovedByID = ApprovedByID, ScheduleID = ScheduleID };
            ResourceSchedule schedule = scheduleService.Approve(request);
            
            Assert.NotNull(schedule);
            Assert.True(schedule.Approved);
            Assert.Equal(ApprovedByID, schedule.ApprovedByID);
        }

        [Fact]
        public void CreateTest(){
            long ID = 1523;
            MockScheduleRepository scheduleRepository = new MockScheduleRepository();
            scheduleRepository.MockCreate();

            ScheduleService scheduleService = new ScheduleService(scheduleRepository.Object);

            ResourceSchedule schedule = new ResourceSchedule {
                ID = ID
            };

            ResourceSchedule createSchedule = scheduleService.Create(schedule);
            Assert.Equal(ID, createSchedule.ID);
        }
    }

}
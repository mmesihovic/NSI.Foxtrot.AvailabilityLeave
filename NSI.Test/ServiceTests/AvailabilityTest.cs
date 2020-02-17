using Xunit;
using System;
using System.Collections.Generic;
using NSI.DAL.Entities;
using NSI.BLL.Services;
using NSI.Test.Mocks.Repositories;
using NSI.DataContracts.Models.Responses;
using AutoMapper;
using NSI.Common.Helpers;
using System.Linq;

namespace NSI.Test.ServiceTests {
    public class AvailabilityTests{
        [Theory]
        [InlineData(1, 2, true, 3, 4)]
        public void GetByIdTest(long ID, long ResourceID, bool Approved, long? ApprovedByID, long RequstedByID)
        {
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
            MapperConfiguration configuration = new MapperConfiguration(mc => { mc.AddProfile(new AutoMapperProfile()); });

            MockScheduleRepository scheduleRepository = new MockScheduleRepository();
            scheduleRepository.MockGetAllByResourceId(ID, ResourceID, Approved, ApprovedByID, _event, RequstedByID, FromDate, UntilDate);
            
            ScheduleService scheduleService = new ScheduleService(scheduleRepository.Object);

            AvailabilityService availabilityService = new AvailabilityService(scheduleService, configuration.CreateMapper());
            TimetableResponse timetableResponse = availabilityService.GetById(ResourceID);
            Assert.Equal(ResourceID, timetableResponse.ResourceID);
            Assert.Equal(1, timetableResponse.AbsenceDetails.Count);

            AbsenceDetails details = timetableResponse.AbsenceDetails.ToList()[0];
            Assert.Equal(ID,  details.ID);
            Assert.Equal(ApprovedByID, details.ApprovedByID);
            Assert.Equal(RequstedByID, details.RequestByID);
            Assert.Equal(Approved, details.Approved);
        }
    }
}
using System;
using Xunit;
using NSI.Test.Mocks.Repositories;
using NSI.BLL.Services;
using System.Collections.Generic;
using NSI.DAL.Entities;
using System.Linq;
using NSI.Test.TestCase;

namespace NSI.Test.ServiceTests
{
    public class AvailabilityRuleTests
    {
        [Theory]
        [MemberData("Data", MemberType = typeof(AvailabilityTestCase))]
        public void GetAllTest(long ID, long ResourceID, string WeekDay,
            DateTime FromTime, DateTime ToTime, bool Available)
        {
            MockAvailabilityRuleRepository mockAvailabilityRuleRepository = new MockAvailabilityRuleRepository();
            mockAvailabilityRuleRepository.MockGetAll(ID, ResourceID, WeekDay, FromTime, ToTime, Available);

            AvailabilityRuleService availabilityRuleService = new AvailabilityRuleService(mockAvailabilityRuleRepository.Object);

            List<AvailabilityRule> availabilityRules = availabilityRules = availabilityRuleService.GetAll().ToList();
            Assert.Single(availabilityRules);
            Assert.Equal(ID, availabilityRules[0].ID);
            Assert.Equal(ResourceID, availabilityRules[0].ResourceID);
            Assert.Equal(WeekDay, availabilityRules[0].WeekDay);
            Assert.Equal(FromTime, availabilityRules[0].FromTime);
            Assert.Equal(ToTime, availabilityRules[0].ToTime);
            Assert.Equal(Available, availabilityRules[0].Available);
        
        }


        [Theory]
        [MemberData("Data", MemberType = typeof(AvailabilityTestCase))]
        public void GetAllByResourceIdTest(long ID, long ResourceID, string WeekDay,
            DateTime FromTime, DateTime ToTime, bool Available)
        {
            MockAvailabilityRuleRepository mockAvailabilityRepository = new MockAvailabilityRuleRepository();
            mockAvailabilityRepository.MockGetAllByResourceId(ID, ResourceID, WeekDay, FromTime, ToTime, Available);

            AvailabilityRuleService availabilityRuleService = new AvailabilityRuleService(mockAvailabilityRepository.Object);

            List<AvailabilityRule> availabilityRules = availabilityRuleService.GetAllByResourceId(ResourceID).ToList();

            Assert.Single(availabilityRules);
            Assert.Equal(ID, availabilityRules[0].ID);
            Assert.Equal(ResourceID, availabilityRules[0].ResourceID);
            Assert.Equal(WeekDay, availabilityRules[0].WeekDay);
            Assert.Equal(FromTime, availabilityRules[0].FromTime);
            Assert.Equal(ToTime, availabilityRules[0].ToTime);
            Assert.Equal(Available, availabilityRules[0].Available);
        }

        [Theory]
        [MemberData("Data", MemberType = typeof(AvailabilityTestCase))]
        public void CreateTest(long ID, long ResourceID, string WeekDay,
            DateTime FromTime, DateTime ToTime, bool Available)
        {
            MockAvailabilityRuleRepository mockAvailabilityRuleRepository = new MockAvailabilityRuleRepository();
            mockAvailabilityRuleRepository.MockCreate();
            AvailabilityRuleService availabilityRuleService = new AvailabilityRuleService(mockAvailabilityRuleRepository.Object);

            List<AvailabilityRule> availabilityRules = new List<AvailabilityRule>();
            
            AvailabilityRule createdAvailabilityRule = availabilityRuleService.Create(new AvailabilityRule{
                ID = ID,
                ResourceID = ResourceID,
                WeekDay = WeekDay,
                FromTime = FromTime,
                ToTime = ToTime,
                Available = Available
            });
            
            Assert.NotNull(createdAvailabilityRule);
            Assert.Equal(ID, createdAvailabilityRule.ID);
            Assert.Equal(ResourceID, createdAvailabilityRule.ResourceID);
            Assert.Equal(WeekDay, createdAvailabilityRule.WeekDay);
            Assert.Equal(FromTime, createdAvailabilityRule.FromTime);
            Assert.Equal(ToTime, createdAvailabilityRule.ToTime);
            Assert.Equal(Available, createdAvailabilityRule.Available);
        }

        [Theory]
        [InlineData(1)]
        public void DeleteTest(long ID)
        {
            MockAvailabilityRuleRepository mockAvailabilityRuleRepository = new MockAvailabilityRuleRepository();
            mockAvailabilityRuleRepository.MockDelete(ID);
            AvailabilityRuleService availabilityRuleService = new AvailabilityRuleService(mockAvailabilityRuleRepository.Object);
            Assert.True(availabilityRuleService.Delete(ID));
        }
    }
}
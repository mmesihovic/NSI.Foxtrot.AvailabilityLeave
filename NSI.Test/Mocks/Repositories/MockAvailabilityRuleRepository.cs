using System.Collections.Generic;
using Moq;
using NSI.DAL.Repository.Interfaces;
using NSI.DAL.Entities;
using System;

namespace NSI.Test.Mocks.Repositories
{
    public class MockAvailabilityRuleRepository: Mock<IAvailabilityRuleRepository>
    {

        public MockAvailabilityRuleRepository MockGetAll(long ID, long ResourceID, string WeekDay, 
            DateTime FromTime, DateTime ToTime, bool Available)
        {
            List<AvailabilityRule> availabilityRules = new List<AvailabilityRule>();
            availabilityRules.Add(new AvailabilityRule {
                ID = ID,
                ResourceID = ResourceID,
                WeekDay = WeekDay,
                FromTime = FromTime,
                ToTime = ToTime,
                Available = Available
            });
            
            Setup( x => x.GetAll()).Returns(availabilityRules);
            return this;
        }

        public MockAvailabilityRuleRepository MockGetAllByResourceId(long ID, long ResourceID, string WeekDay, 
            DateTime FromTime, DateTime ToTime, bool Available)
        {
            List<AvailabilityRule> availabilityRules = new List<AvailabilityRule>();
            availabilityRules.Add(new AvailabilityRule {
                ID = ID,
                ResourceID = ResourceID,
                WeekDay = WeekDay,
                FromTime = FromTime,
                ToTime = ToTime,
                Available = Available
            });

            Setup(x => x.GetAllByResourceId(It.Is<long>(_id => _id == ResourceID))).Returns(availabilityRules);

            return this;
        }

        public MockAvailabilityRuleRepository MockGetById(long ID, long ResourceID, string WeekDay, 
            DateTime FromTime, DateTime ToTime, bool Available)
        {
             Setup( x => x.GetById( It.Is<long>( _id => _id == ID)))
                .Returns(new AvailabilityRule {
                ID = ID,
                ResourceID = ResourceID,
                WeekDay = WeekDay,
                FromTime = FromTime,
                ToTime = ToTime,
                Available = Available
            });
            return this;
        }

        public MockAvailabilityRuleRepository MockCreate(){
            Setup( x => x.Create(It.IsAny<AvailabilityRule>()))
                .Returns<AvailabilityRule>(availabilityRules => availabilityRules);
            return this;
        }

        public MockAvailabilityRuleRepository MockGetByResourceIdNull(){
            Setup( x => x.GetAllByResourceId(It.IsAny<long>())).Returns<AvailabilityRule>( null );
            return this;
        }
        public MockAvailabilityRuleRepository MockDelete(long ID){
            Setup( x => x.Delete(It.Is<long>( id => id == ID ))).Returns(true);
            return this;
        }
    }
}
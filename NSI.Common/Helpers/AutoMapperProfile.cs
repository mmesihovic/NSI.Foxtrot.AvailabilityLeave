using AutoMapper;
using System;
using NSI.DAL.Entities;
using NSI.DataContracts.Models.Requests;
using NSI.DataContracts.Models.Responses;
using System.Globalization;

namespace NSI.Common.Helpers
{
    public class AutoMapperProfile: Profile
    {
        public AutoMapperProfile()
        {
           
                
                CreateMap<Event, EventResponse>();
                CreateMap<EventRequest, Event>();
                CreateMap<EventType, EventTypeResponse>();
                CreateMap<EventTypeRequest, EventType>();
                // TODO: Add sufrix rule
                CreateMap<ScheduleRequestPost, ResourceSchedule>()
                  .ForMember(dst => dst.FromDate, opt => opt.MapFrom(src => src.From))
                  .ForMember(dst => dst.UntilDate , opt => opt.MapFrom(src => src.Until));
                CreateMap<ResourceSchedule, ScheduleResponse>()
                  .ForMember(dst => dst.From, opt => opt.MapFrom(src => src.FromDate))
                  .ForMember(dst => dst.Until, opt => opt.MapFrom(src => src.UntilDate));

                CreateMap<LeaveBalance, LeaveBalanceResponse>();
                CreateMap<LeaveBalanceRequest, LeaveBalance>();
                CreateMap<LeaveTransaction, LeaveTransactionResponse>()
                    .ForMember(dst => dst.LeaveBalanceId, opt => opt.MapFrom(src => src.LeaveBalance.ID));

                CreateMap<AvailabilityRuleRequest, AvailabilityRule>()
                    .ForMember(dst => dst.ToTime, opt => opt.MapFrom(src => DateTime.ParseExact(src.ToTime, "HH:mm", CultureInfo.CurrentCulture)))
                    .ForMember(dst => dst.FromTime, opt => opt.MapFrom(src => DateTime.ParseExact(src.FromTime, "HH:mm", CultureInfo.CurrentCulture)));
                
                CreateMap<AvailabilityRule, AvailabilityRuleResponse>()
                    .ForMember(dst => dst.ToTime, opt => opt.MapFrom(src => src.ToTime.ToString("HH:mm")))
                    .ForMember(dst => dst.FromTime, opt => opt.MapFrom(src => src.FromTime.ToString("HH:mm")));
                
        }
    }
}

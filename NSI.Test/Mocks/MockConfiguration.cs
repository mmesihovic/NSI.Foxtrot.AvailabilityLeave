using Moq;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;

namespace NSI.Test.Mocks {

    public static class MockConfiguration  {

        public static IConfiguration MockLeaveBalanceConfiguration(long Days, long MonthlyGain, long YearlyGain){
            Dictionary<string, string> settings = new Dictionary<string, string>();
            settings.Add("LeaveBalance:Days", Days.ToString());
            settings.Add("LeaveBalance:MonthlyGain", MonthlyGain.ToString());
            settings.Add("LeaveBalance:YearlyGain", YearlyGain.ToString());
            
            IConfiguration configuration = new ConfigurationBuilder()
                                            .AddInMemoryCollection(settings)
                                            .Build();
            return configuration;
        }
        public static IConfiguration MockEmpthy(){
            IConfiguration configuration = new ConfigurationBuilder().Build();
            return configuration;
        }
    }

}
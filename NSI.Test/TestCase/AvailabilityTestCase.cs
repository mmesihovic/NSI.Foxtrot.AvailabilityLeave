using System;
using System.Collections.Generic;

namespace NSI.Test.TestCase
{
    public class AvailabilityTestCase
    {
        public static readonly List<object[]> GetData = new List<object[]>
        {
            new Object[]{ 1, 2, "Monday", new DateTime(2020,1,20), new DateTime(2020, 1, 22), false},
            new Object[]{ 2, 1, "Wednesday", new DateTime(2020,2,10), new DateTime(2020,2, 12), true}
        };

        public static IEnumerable<object[]> Data {
            get { return GetData;}
        }
    }
}
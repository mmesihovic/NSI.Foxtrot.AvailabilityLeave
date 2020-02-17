﻿using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Common.Exceptions
{
    public class NsiConfigurationException :NsiBaseException
    {
        public NsiConfigurationException(string message, SeverityEnum severity = SeverityEnum.Error)
           : base(message, severity)
        {

        }
        public NsiConfigurationException(string message, Exception inner, SeverityEnum severity)
            : base(message, inner, severity)
        {

        }
    }
}

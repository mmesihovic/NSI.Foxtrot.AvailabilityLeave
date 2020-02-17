﻿using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Common.Exceptions
{
    public class NsiNotAuthorizedException : NsiBaseException
    {
        public NsiNotAuthorizedException(string message, SeverityEnum severity = SeverityEnum.Error)
            : base(message, severity)
        {

        }
        public NsiNotAuthorizedException(string message, Exception inner, SeverityEnum severity)
            : base(message, inner, severity)
        {

        }
    }
}

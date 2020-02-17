﻿using NSI.Common.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace NSI.Common.Exceptions
{
    public class NsiArgumentException :NsiBaseException
    {
        public NsiArgumentException(string message, SeverityEnum severity = SeverityEnum.Error)
            : base(message, severity)
        {

        }
        public NsiArgumentException(string message, Exception inner, SeverityEnum severity)
            : base(message, inner, severity)
        {

        }
    }
}

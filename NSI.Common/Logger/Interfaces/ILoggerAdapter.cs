using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using System;

namespace NSI.Common.Logger.Interfaces
{
    public interface ILoggerAdapter
    {
        void LogException<T>(Exception ex, T request, SeverityEnum severity = SeverityEnum.Error);
        void LogException<T>(Exception ex, SeverityEnum severity = SeverityEnum.Error);
        void LogException<T>(NsiBaseException ex, T request);
        void LogException<T>(NsiBaseException ex);
    }
}

using System;
using NLog;
using NSI.Common.Enumerations;
using NSI.Common.Exceptions;
using NSI.Common.Logger.Interfaces;

namespace NSI.Common.Logger
{
    public class NLogAdapter : ILoggerAdapter
    {
        private static readonly NLog.Logger _logger = LogManager.GetCurrentClassLogger();

        public void LogException<T>(Exception ex, T request, SeverityEnum severity = SeverityEnum.Error)
        {
            _logger.Log(ConvertToNLogLevel(severity), ex, ex.Message, null);
        }

        public void LogException<T>(Exception ex, SeverityEnum severity = SeverityEnum.Error)
        {
            _logger.Log(ConvertToNLogLevel(severity), ex, ex.Message, null);
        }

        public void LogException<T>(NsiBaseException ex, T request)
        {
            _logger.Log(ConvertToNLogLevel(ex.Severity), ex, ex.Message, request);
        }

        public void LogException<T>(NsiBaseException ex)
        {
            _logger.Log(ConvertToNLogLevel(ex.Severity), ex, ex.Message, null);
        }

        private LogLevel ConvertToNLogLevel(SeverityEnum severity)
        {
            switch (severity)
            {
                case SeverityEnum.Info:
                    return LogLevel.Info;
                case SeverityEnum.Fatal:
                    return LogLevel.Info;
                case SeverityEnum.Warning:
                    return LogLevel.Warn;
                case SeverityEnum.Debug:
                    return LogLevel.Debug;
                default:
                    return LogLevel.Error;
            }
        }
    }
}

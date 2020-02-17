using NSI.Common.Exceptions;
using NSI.Common.Logger.Interfaces;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using NSI.DataContracts;
using NSI.Common.Resources;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft;
using Newtonsoft.Json;

namespace NSI.Common.Handlers
{
    public class NsiExceptionFilter : ExceptionFilterAttribute, IExceptionFilter
    {
        private readonly ILoggerAdapter _logger;

        public NsiExceptionFilter(ILoggerAdapter logger)
        {
            _logger = logger;
        }

        public override void OnException(ExceptionContext exceptionContext)
        {
            var customException = exceptionContext.Exception as NsiBaseException;
            var exceptionType = customException.GetType();
            var severity = Common.Enumerations.SeverityEnum.Error;


            switch (exceptionType) 
            {
                case Type _ when exceptionType == typeof(NsiArgumentException):
                    CreateResponseOnException(exceptionContext, HttpStatusCode.BadRequest, ExceptionMessages.ArgumentException);
                    break;

                case Type _ when exceptionType == typeof(NsiArgumentNullException):
                    CreateResponseOnException(exceptionContext, HttpStatusCode.BadRequest, ExceptionMessages.ArgumentNullException);
                    break;

                case Type _ when exceptionType == typeof(NsiConfigurationException):
                    CreateResponseOnException(exceptionContext, HttpStatusCode.InternalServerError, ExceptionMessages.ConfigurationException);
                    break;

                case Type _ when exceptionType == typeof(NsiNotAuthorizedException):
                    CreateResponseOnException(exceptionContext, HttpStatusCode.Unauthorized, ExceptionMessages.UnauthorizedException);
                    break;

                case Type _ when exceptionType == typeof(NsiNotFoundException):
                    CreateResponseOnException(exceptionContext, HttpStatusCode.NotFound, ExceptionMessages.ResourceNotFound);
                    break;
            }

            _logger.LogException(exceptionContext.Exception, exceptionContext, severity);

        }

        private void CreateResponseOnException(ExceptionContext context, HttpStatusCode statusCode, string message)
        {
            context.Result = new ContentResult()
            {
                Content = message,
                StatusCode = (int?)statusCode,
                ContentType = "text/plain"
            };
        }
    }
}

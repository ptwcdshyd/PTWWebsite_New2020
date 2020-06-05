using LoggerService;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PTWWebsite2.CustomMiddleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILoggerManager _logger;

        private readonly IHttpContextAccessor _contextAccessor;
        public ExceptionMiddleware(RequestDelegate next, ILoggerManager logger, IHttpContextAccessor contextAccessor)
        {
            _logger = logger;
            _next = next;
            _contextAccessor = contextAccessor;
        }

        public async Task InvokeAsync(HttpContext httpContext)
        {
            try
            {

                await _next(httpContext);
            }
            catch (HttpStatusCodeException ex)
            {
                _logger.LogError($"Error Occurred: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Something went wrong: {ex}");
                await HandleExceptionAsync(httpContext, ex);
            }
        }

        private Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


            if (context.User.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                int loginUserId = Convert.ToInt32(context.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                _logger.LogInfo($"Get Logged in UserId: {loginUserId.ToString()}");
                _logger.LogInfo($"Going to log on DB");
                // ErrorDetails errdetails = new ErrorDetails();
                // errdetails.Insert(exception, loginUserId);
                _logger.LogInfo($"Successfully Logged on DB");
            }
            return _next(context);
        }

        private Task HandleExceptionAsync(HttpContext context, HttpStatusCodeException exception)
        {
            string result = null;
            context.Response.ContentType = "application/json";
            if (context.User.FindFirst(ClaimTypes.NameIdentifier) != null)
            {
                int loginUserId = Convert.ToInt32(context.User.FindFirst(ClaimTypes.NameIdentifier).Value);
                _logger.LogInfo($"HttpStatusCodeException -> Get Logged in UserId: {loginUserId.ToString()}");
                _logger.LogInfo($"Going to log on DB");
                //ErrorDetails errdetails = new ErrorDetails();
                //errdetails.Insert(exception, loginUserId);
                _logger.LogInfo($"Successfully Logged on DB");
            }

            if (exception is HttpStatusCodeException)
            {
                // result = new ErrorDetails() { Message = exception.Message, StatusCode = (int)exception.StatusCode }.ToString();
                context.Response.StatusCode = (int)exception.StatusCode;
            }
            else
            {
                // result = new ErrorDetails() { Message = "Runtime Error", StatusCode = (int)HttpStatusCode.BadRequest }.ToString();
                context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
            }
            //return context.Response.WriteAsync(result);

            return _next(context);
        }

        private Task CORS(HttpContext context)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = 404;
            return _next(context);
        }

    }
}

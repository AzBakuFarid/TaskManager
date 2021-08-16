using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TaskManager.Services.Exceptions;

namespace TaskManager.Web.Filters
{
    public class UiExceptionFilter : IExceptionFilter
    {
        private readonly IModelMetadataProvider _modelMetadataProvider;

        public UiExceptionFilter(IModelMetadataProvider modelMetadataProvider)
        {
            this._modelMetadataProvider = modelMetadataProvider;
        }
        public void OnException(ExceptionContext context)
        {
            context.ExceptionHandled = true;
            var exception = context.Exception.InnerException ?? context.Exception;
            var response = context.HttpContext.Response;
            var result = new ViewResult();
            if (exception is NotFoundException)
            {
                response.StatusCode = (int)HttpStatusCode.NotFound;
                result.ViewName = "NotFound";
                result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState)
                {
                    { "ErrorMessage", exception.Message }
                };
            }
            else if (exception is OperationNotAllowedException)
            {
                response.StatusCode = (int)HttpStatusCode.Forbidden;
                result.ViewName = "AccessDenied";
                result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState)
                {
                    { "ErrorMessage", exception.Message }
                };
            }
            else
            {
                response.StatusCode = (int)HttpStatusCode.InternalServerError;
                result.ViewName = "Error";
                result.ViewData = new ViewDataDictionary(_modelMetadataProvider, context.ModelState)
                {
                    { "ErrorMessage", null }
                };
            }
            context.Result = result;

        }
    }
}

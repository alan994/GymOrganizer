using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;

namespace Web.Utils
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IModelMetadataProvider modelMetadataProvider;
        private readonly ILogger<ExceptionFilter> logger;

        public ExceptionFilter(IHostingEnvironment hostingEnvironment, IModelMetadataProvider modelMetadataProvider, ILogger<ExceptionFilter> logger)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.modelMetadataProvider = modelMetadataProvider;
            this.logger = logger;
        }

        public override void OnException(ExceptionContext context)
        {
            this.logger.LogError($"Error while calling {context.ActionDescriptor.DisplayName}. Exception message {context.Exception.Message}");
            context.Result = new BadRequestResult();
        }
    }
}
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.Extensions.Logging;
using System.IO;
using System.Text;
using Logger;
using Data.Db;
using System;

namespace Web.Utils
{
    public class ExceptionFilter : ExceptionFilterAttribute
    {
        private readonly IHostingEnvironment hostingEnvironment;
        private readonly IModelMetadataProvider modelMetadataProvider;
        private readonly ILogger<ExceptionFilter> logger;
        private readonly GymOrganizerContext db;

        public ExceptionFilter(IHostingEnvironment hostingEnvironment, IModelMetadataProvider modelMetadataProvider, ILogger<ExceptionFilter> logger, GymOrganizerContext db)
        {
            this.hostingEnvironment = hostingEnvironment;
            this.modelMetadataProvider = modelMetadataProvider;
            this.logger = logger;
            this.db = db;
        }

        public override void OnException(ExceptionContext context)
        {
            var url = context.HttpContext.Request.Path.ToString();
            var queryString = context.HttpContext.Request.Query.ToString();
            var body = string.Empty;

            if (context.HttpContext.Request.Method.ToLower().Trim() != "get")
            {
                using (StreamReader reader = new StreamReader(context.HttpContext.Request.Body, Encoding.UTF8))
                {
                    body = reader.ReadToEnd();
                }
            }

            var tenantIdFromToken = TokenHelper.ExtractTenantFromToken(context.HttpContext.User, this.db);
            var userIdFromToken = TokenHelper.ExtractUserFromToken(context.HttpContext.User, this.db);

            string tenantId = null;
            string userId = null;
            if(tenantIdFromToken != Guid.Empty)
            {
                tenantId = tenantIdFromToken.ToString();
            }

            if (userIdFromToken != Guid.Empty)
            {
                userId = userIdFromToken.ToString();
            }

            this.logger.LogCustomError($"Error occurred. Url: {url}, QueryString: {queryString}, body: {body}", context.Exception, null, tenantId, userId);
            this.logger.LogError($"Error while calling {context.ActionDescriptor.DisplayName}. Exception message {context.Exception.Message}");
            context.Result = new BadRequestResult();
        }
    }
}
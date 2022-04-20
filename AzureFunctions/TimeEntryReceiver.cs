using App.Common.Abstraction;
using App.Common.DTO;
using App.Common.Factory;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Azure.WebJobs.Host;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace AzureFunctionApp
{
    public static class TimeEntryReceiver
    {
        private static readonly IRequestValidator _requestValidator = AppFactory.CreateRequestValidator();
        private static readonly Dictionary<string, string> _configParameters = new Dictionary<string, string>
        {
            ["url"] = Environment.GetEnvironmentVariable("dynamicsUrl"),
            ["userName"] = Environment.GetEnvironmentVariable("dynamicsUserName"),
            ["password"] = Environment.GetEnvironmentVariable("dynamicsPassword")
        };
        private static readonly ITimeEntryService _timeEntryService = AppFactory.CreateTimeEntryService(_configParameters);



        [FunctionName("TimeEntryReceiver")]
        public static async Task<HttpResponseMessage> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "POST", Route = null)] HttpRequestMessage req, 
            TraceWriter log)
        {
            log.Info("Request received");

            try
            {
                var timeEntryDTO = await req.Content.ReadAsAsync<TimeEntryDTO>();

                var failedValidations = _requestValidator.Validate(timeEntryDTO);
                if (failedValidations.Count > 0)
                {
                    return req.CreateResponse(HttpStatusCode.BadRequest, failedValidations);
                }

                var result = _timeEntryService.Save(timeEntryDTO);

                return req.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (Exception ex)
            {
                log.Error($"ERROR OCCURED: {ex.Message}");

                return req.CreateResponse(HttpStatusCode.InternalServerError, ex.Message);
            }
        }

    }
}

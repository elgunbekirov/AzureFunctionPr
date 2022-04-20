using App.Common.Abstraction;
using App.Common.Repository;
using App.Common.Service;
using App.Common.Validator;
using Microsoft.Crm.Sdk.Messages;
using Microsoft.Xrm.Sdk;
using Microsoft.Xrm.Tooling.Connector;
using System;
using System.Collections.Generic;

namespace App.Common.Factory
{
    public static class AppFactory
    {
        public static IRequestValidator CreateRequestValidator()
        {
            return new RequestValidator();
        }
        public static ITimeEntryRepository CreateTimeEntryRepository(Dictionary<string, string> configParameters, string repositoryName = "DYNAMICS")
        {
            if (repositoryName == "DYNAMICS")
            {
                var organizationServiceProxy = CreateOrganizationServiceProxy(configParameters["url"], configParameters["userName"], configParameters["password"]);

                return new DynamicsTimeEntryRepository(organizationServiceProxy);
            }

            throw new System.Exception($"{repositoryName} repository not implemented");
        }

        public static ITimeEntryService CreateTimeEntryService(Dictionary<string, string> configParameters, string serviceName = "DYNAMICS")
        {
            if (serviceName == "DYNAMICS")
            {
                var timeEntryRepository = CreateTimeEntryRepository(configParameters, "DYNAMICS");

                return new DynamicsTimeEntryService(timeEntryRepository);
            }

            throw new System.Exception($"{serviceName} service not implemented");
        }

        private static IOrganizationService CreateOrganizationServiceProxy(string url, string userName, string password)
        {
            string connectionString = $@"Url = {url};AuthType=OAuth;
                                         UserName = {userName};Password = {password}; 
                                         AppId = 51f81489-12ee-4a9e-aaae-a2591f45987d;
                                         RedirectUri = app://58145B91-0C36-4500-8554-080854F2AC97;
                                         LoginPrompt=Auto;RequireNewInstance = True";

            var crmServiceClient = new CrmServiceClient(connectionString);

            var organizationServiceProxy = crmServiceClient.OrganizationWebProxyClient != null ? crmServiceClient.OrganizationWebProxyClient : (IOrganizationService)crmServiceClient.OrganizationServiceProxy;

            if (organizationServiceProxy == null)
                throw new Exception("Connection failed.");

            var userid = ((WhoAmIResponse)organizationServiceProxy.Execute(new WhoAmIRequest())).UserId;

            if (userid == Guid.Empty)
                throw new Exception("Connection was not established.");

            return organizationServiceProxy;
        }
    }
}

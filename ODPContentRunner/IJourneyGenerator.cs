using ODP.Services.Models;
using System;
using System.Collections.Generic;

namespace ODPContentRunner
{
    public interface IJourneyGenerator
    {
        List<ODPGeneric> GenerateAssociationCMSScript(Customer customer, DateTime startDateTime);
        List<ODPGeneric> GenerateCMSScript(Customer customer, DateTime startDateTime);
        List<ODPGeneric> GenerateCommerceMultiDayScript(Customer customer, DateTime startDateTime);
        List<ODPGeneric> GenerateCommerceScript(Customer customer, DateTime startDateTime);
    }
}
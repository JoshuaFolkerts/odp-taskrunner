using ODP.Services.Models;
using System;
using System.Collections.Generic;

namespace ODPContentRunner
{
    public interface IRandomGenerator
    {
        List<ODPGeneric> ExecuteCloudAndCloundContent(DateTime dateTime, List<Product> products, List<Customer> customers, int count = 100);

        List<ODPGeneric> ExecuteCloudContent(DateTime dateTime, List<Product> products, List<Customer> customers, int count = 30);

        List<ODPGeneric> GenerateRandomEvents();

        List<ODPGeneric> GenerateRandomFormSubmit();

        List<ODPGeneric> GenerateRandomPageView();
    }
}
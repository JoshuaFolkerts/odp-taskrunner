﻿using ODP.Services.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ODP.Services
{
    public interface IContentGeneratorService
    {
        List<Customer> GenerateCustomers(int count);
    }
}
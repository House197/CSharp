using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api2.Helpers
{
    public class QueryObject
    {
        public string? String { get; set; } = null; // Deberia ser Symbol, pero se colocó String como nombre por accidente.
        public string? CompanyName { get; set; } = null;
    }
}
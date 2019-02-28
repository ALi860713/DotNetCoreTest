using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NetCoreTest.Models
{
    public class APIResult
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public List<APIData> Payload { get; set; }
    }
}

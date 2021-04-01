using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieManagement.Api.Models
{
    public class ErrorModel
    {
        public string ErrorMessage { get; set; }
        public string InnerException { get; set; }
    }
}

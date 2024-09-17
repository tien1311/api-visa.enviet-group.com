using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EV.Model.Models.Interfaces
{
    public interface IReturnObject
    {
        HttpStatusCode status { get; set; } 
        string message { get; set; }
        object result { get; set; }
    }
}

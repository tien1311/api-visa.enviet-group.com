using EV.Model.Models.Interfaces;
using Newtonsoft.Json;
using System.Net;

namespace EV.Model.Models.Shared
{
    public class ReturnObject : IReturnObject
    {
        public HttpStatusCode status { get; set; } = HttpStatusCode.OK;
        public string message { get; set; } = "Success";
        public object result { get; set; } = null;

        public Type Parse<Type>()
        {
            return JsonConvert.DeserializeObject<Type>(result.ToString());
        }
    }
    public class ReturnObjectAppota : IReturnObject
    {
        public HttpStatusCode status { get; set; } = HttpStatusCode.OK;
        public string message { get; set; } = "Success";
        public object result { get; set; } = null;
        public int ErrorCode { get; set; }
        public Type Parse<Type>()
        {
            return JsonConvert.DeserializeObject<Type>(result.ToString());
        }
      
    }
}

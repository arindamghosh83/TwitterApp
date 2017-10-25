using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace MyTwitterApp.Extensions
{
    public class JsonNetResult : ActionResult
    {
        public object Data { get; set; }
        public override void ExecuteResult(ControllerContext context)
        {
            var response = context.HttpContext.Response;
            response.ContentType = "application/json";
            response.Write(JsonConvert.SerializeObject(Data,new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }));

        }
    }
}
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace WebAPI_Save_Files.Controllers
{
    public class ValuesController : ApiController
    {
        static string path = @"C:\Users\faranam\Desktop\OutputFiles";

        // POST api/values
        [HttpPost]
        public string Post()
        {
            
            foreach (var item in Request.Content.Headers)
            {
                //ActionContext.ControllerContext.RequestContext;
                var filePath = $"{path}\\{item}";

                if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                if (File.Exists(filePath))
                {
                    return "File Already Exists!";
                }

                else
                {
                    File.Create(filePath);
                    Request.GetRequestContext();
                    File.WriteAllText(filePath, Request.Content.Headers.GetValues("").First());
                    return "File has been Created!";
                }
            }
            return "";
        }
    }
}

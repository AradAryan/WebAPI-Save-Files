using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI_Save_Files.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public string Post(string value)
        {
            if (Request.Headers.Contains("arad"))
            {
                StreamWriter sw = new StreamWriter(@"C:\Users\faranam\Desktop\Context.txt");
                sw.Write(value);
                sw.Close();
                return "Jobs Done!";
            }
            else
                return "File is not Valid";

            /*var checksum = Request.Headers;

             if (checksum.Contains("Custom"))
             {
                 string token = checksum.GetValues("Custom").First();
             }
             */
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}

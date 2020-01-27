using System;
using System.IO;
using System.Net;
using System.Web;
using System.Linq;
using System.Net.Http;
using System.Web.Http;

namespace WebAPI_Save_Files.Controllers
{
    public class ValuesController : ApiController
    {
        #region Fields
        /// <summary>
        /// Output Path
        /// </summary>
        static string path =
            @"C:\Users\faranam\Desktop\OutputFiles";
        #endregion

        #region POST
        /// <summary>
        /// POST Function
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public HttpResponseMessage Post()
        {
            string root =
                HttpContext.Current.Server.MapPath("~/App_Data");

            var provider =
                new MultipartFormDataStreamProvider(root);

            bool error = false;
            try
            {
                var contents =
                    Request.Content.ReadAsMultipartAsync(provider).Result;

                foreach (var item in contents.Contents.ToArray())
                {
                    var filePath =
                        $"{path}\\{ item.Headers.First().Value.First()}";

                    if (!Directory.Exists(path))
                    {
                        Directory.CreateDirectory(path);
                    }
                    if (File.Exists(filePath))
                    {
                        error = true;
                        continue;
                    }
                    else
                    {
                        using (File.Create(filePath))
                        {
                            ///Release Pointer
                        }
                        File.WriteAllBytes(filePath,
                            item.ReadAsByteArrayAsync().Result);
                    }
                }
                if (error)
                    return Request.CreateErrorResponse
                        (HttpStatusCode.Forbidden, "Wrong Checksum");
                else
                    return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                return Request.CreateErrorResponse
                    (HttpStatusCode.InternalServerError, e);
            }
        }
        #endregion 
    }
}


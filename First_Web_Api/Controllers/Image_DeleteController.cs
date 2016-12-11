using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using First_Web_Api.Models;
using System.IO;
using System.Text;

namespace First_Web_Api.Controllers
{
    public class Image_DeleteController : ApiController
    {
        // GET: api/Image_Delete
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Image_Delete/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Image_Delete
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Image_Delete/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Image_Delete/5
        public void Delete(int id)
        {
        }
    }
}

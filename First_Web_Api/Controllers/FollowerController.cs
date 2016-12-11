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
    public class FollowerController : ApiController
    {
        MysqlConnent MyConnent = new MysqlConnent();
        // GET: api/Follower
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Follower/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Follower
        public string Post([FromBody]Follower value)
        {
            try
            {
                MyConnent.MySqlWrite("INSERT INTO 关注表() VALUES('" + value.FollowerAccount + "','" + value.WasFollowederAccount + "')");
                return "true";
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
                return "false";
            }
        }

        // PUT: api/Follower/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Follower/5
        public void Delete(int id)
        {
        }
    }
}

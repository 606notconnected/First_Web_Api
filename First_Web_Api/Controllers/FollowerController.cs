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

        // POST: api/Follower
        /// <summary>
        /// 关注
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
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

    }
}

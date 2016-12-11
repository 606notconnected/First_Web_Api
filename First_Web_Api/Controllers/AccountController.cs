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
    public class AccountController : ApiController
    {
        MysqlConnent MyConnent = new MysqlConnent();
        bool IsSuccess = true;

        // GET: api/Account
        public string Get()
        {
            return IsSuccess.ToString();
        }

        // GET: api/Account/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Account
        public string PostAccount([FromBody]Sign value)
        {
            try
            {
                MyConnent.MySqlWrite("INSERT INTO 账号表() VALUES('" + value.account + "','" + value.password + "')");
                return value.account + "," +value.password;
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
                return e.ToString();
            }
            
        }

   


        // PUT: api/Account/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/Account/5
        public void Delete(int id)
        {
        }
    }
}

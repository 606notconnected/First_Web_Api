using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using First_Web_Api.Models;

namespace First_Web_Api.Controllers
{
    public class RoadController : ApiController
    {

        MysqlConnent myConnent = new MysqlConnent();
 
        // GET: api/Road/5
        /// <summary>
        /// 通过RoadID获取图片名
        /// </summary>
        /// <param name="RoadID"></param>
        /// <returns></returns>
        public ReturnImageNameByRoadID GetImageName(string RoadID)
        {
            ReturnImageNameByRoadID tmpModel = new ReturnImageNameByRoadID();
            List<string> tmpList = new List<string>();
            try
            {
                tmpList = myConnent.MySqlRead("SELECT * FROM 图片表 WHERE RoadID = '"+ RoadID + "'  ORDER BY DateTime DESC", "ImageName");
                Debug.WriteLine(tmpList);
                tmpModel.returnImageName = tmpList;
                return tmpModel;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
                tmpList.Add("error");
                tmpModel.returnImageName = tmpList;
                return tmpModel;
            }
        }
        /// <summary>
        /// 通过帐号获取行程信息
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public ReturnRoad Get(string account)
        {
            ReturnRoad tmpReturnRoad = new ReturnRoad();
            List<Dictionary<string, string>> tmpList = new List<Dictionary<string, string>>();
            try
            {

                Dictionary<string, string> returnDictionary = new Dictionary<string, string>();
                returnDictionary.Add("RoadID", null);
                returnDictionary.Add("RoadName", null);
                returnDictionary.Add("Introduction", null);
                tmpList = myConnent.MySqlReadDictionary("SELECT * FROM 路径表 WHERE account ='" + account + "'", returnDictionary);
                tmpReturnRoad.returnMessage = tmpList;
                return tmpReturnRoad;
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
                tmpReturnRoad.returnMessage = tmpList;
                return tmpReturnRoad;
            }

        }

        // POST: api/Road
        /// <summary>
        /// 新增行程
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public string Post([FromBody]Road value)
        {
            try
            {
                string tmpID = myConnent.NewID();
                if (value.introduction == null)
                    value.introduction = "无";
                myConnent.MySqlWrite("INSERT INTO 路径表() VALUES('" + tmpID + "','" + value.roadName + "','" + value.account + "','" + value.introduction + "','" + DateTime.Now.ToString("yyyyMMddHHmmss") + "','" + DateTime.Now.ToString("yyyyMMddHHmmss") + "')");
                return tmpID; 
            }
            catch(Exception e)
            {
                return "error";
            }
        }

 
    }
}

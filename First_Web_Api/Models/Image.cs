using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First_Web_Api.Models
{
    /// <summary>
    /// 上传图片Model
    /// </summary>
    public class ImageModel
    {
        /// <summary>
        /// 图片名
        /// </summary>
        public string imageName { get; set; }
        /// <summary>
        /// 拍照日期
        /// </summary>
        public string dateTime { get; set; }

        /// <summary>
        /// 经度
        /// </summary>
        public string longitude { get; set; }
        /// <summary>
        /// 纬度
        /// </summary>
        public string latitude { get; set; }

        /// <summary>
        /// 简介
        /// </summary>
        public string introduction { get; set; }

        /// <summary>
        /// 对应路径ID
        /// </summary>
        public string roadID { get; set; }
    }

    /// <summary>
    /// 队图片进行假删除操作
    /// </summary>
    public class DeleteImageModel
    {
        public string imageName { get; set; }

        public string isDelete { get; set; }
    }


    /// <summary>
    /// 图片上传结果
    /// </summary>
    public class ResultModel
    {
        /// <summary>
        /// 返回结果 0: 失败，1: 成功。
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 操作信息，成功将返回空。
        /// </summary>
        public string Message { get; set; }

    }


    public class ReturnImageNameModel
    {
        public string result { get; set; }

        public List<string> imageName { get; set; }
    }

    public class ReturnImageMessageModel
    {
        public string imageName { get; set; }

        public string dateTime { get; set; }

        public string longitude { get; set; }

        public string latitude { get; set; }

        public string introduction{get; set;}
    }
}
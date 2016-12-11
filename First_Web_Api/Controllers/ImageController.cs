﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Web.Mvc;
using First_Web_Api.Models;
using System.Web.Hosting;
using System.Text;


namespace First_Web_Api.Controllers
{
    public class ImageController : ApiController
    {
        
        /// <summary>
        /// 返回图片
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public HttpResponseMessage Get(string name)
        {
            string root = HttpContext.Current.Server.MapPath("~/Image/"+ name+".jpg");
            //从图片中读取byte
            var imgByte = File.ReadAllBytes(root);
            //从图片中读取流
            var imgStream = new MemoryStream(File.ReadAllBytes(root));
            var resp = new HttpResponseMessage(HttpStatusCode.OK)
            {
                Content = new ByteArrayContent(imgByte)
                //或者
                //Content = new StreamContent(stream)
            };
            resp.Content.Headers.ContentType = new MediaTypeHeaderValue("image/jpg");
            return resp;
        }


        
        /// <summary>
        /// 接收图片
        /// </summary>
        /// <returns></returns>
        public async Task<string> Post()
        {
            string imageName;
            MysqlConnent myConnent = new MysqlConnent();

            if (!Request.Content.IsMimeMultipartContent())
            {
                throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
            string root = HttpContext.Current.Server.MapPath("~/Image");//指定要将文件存入的服务器物理位置
            Debug.WriteLine(root);
            if(!Directory.Exists(root))
            {
                Directory.CreateDirectory(root);
            }
            var provider = new MultipartFormDataStreamProvider(root);
            try
            {
                // Read the form data.
                await Request.Content.ReadAsMultipartAsync(provider);

                List<string> fileName = new List<string>();

                // This illustrates how to get the file names.
                foreach (MultipartFileData file in provider.FileData)
                {//接收文件
                    Trace.WriteLine(file.Headers.ContentDisposition.FileName);//获取上传文件实际的文件名
                    Trace.WriteLine("Server file path: " + file.LocalFileName);//获取上传文件在服务上默认的文件名
                    Save(file.LocalFileName, 769, 1280);
                    fileName.Add(file.LocalFileName);
                }//TODO:这样做直接就将文件存到了指定目录下，暂时不知道如何实现只接收文件数据流但并不保存至服务器的目录下，由开发自行指定如何存储，比如通过服务存到图片服务器
                foreach (var key in provider.FormData.AllKeys)
                {//接收FormData
                    dic.Add(key, provider.FormData[key]);
                }
                imageName = fileName[0].Substring(fileName[0].Length - 45, 45);
                Debug.WriteLine(imageName);
                string account = dic["Account"];
                myConnent.MySqlWrite("INSERT INTO 图片表() VALUES('" + imageName + "','" + account + "','" + DateTime.Now.ToString("yyyyMMddHHmmss")+ "','" + "0" + "','" + "0" + "','" + "暂无" + "','" + "0" + "','" + "暂无" + "')");
               // myConnent.MySqlHasRows("SELECT * FROM 路径表() ")


            }
            catch
            {
                throw;
            }
          
            return imageName;
        }


        



        /* public async Task<HttpResponseMessage> Post()  
         {

             Debug.WriteLine("QAQ");
             // Check whether the POST operation is MultiPart?  
             if (!Request.Content.IsMimeMultipartContent())  
             {
                 Debug.WriteLine(200);
                 throw new HttpResponseException(HttpStatusCode.UnsupportedMediaType);  
             }  

             // Prepare CustomMultipartFormDataStreamProvider in which our multipart form  
             // data will be loaded.  
             string fileSaveLocation = HttpContext.Current.Server.MapPath("~/App_Data");  
             CustomMultipartFormDataStreamProvider provider = new CustomMultipartFormDataStreamProvider(fileSaveLocation);  
             List<string> files = new List<string>();  

             try  
             {  
                 // Read all contents of multipart message into CustomMultipartFormDataStreamProvider.  
                 await Request.Content.ReadAsMultipartAsync(provider);  

                 foreach (MultipartFileData file in provider.FileData)  
                 {  
                     files.Add(Path.GetFileName(file.LocalFileName));
                     Debug.WriteLine(2333);
                 }  

                 // Send OK Response along with saved file names to the client.  
                 return Request.CreateResponse(HttpStatusCode.OK, files);  

             }  
             catch (System.Exception e)  
             {
                 Debug.WriteLine(6666);
                 return Request.CreateErrorResponse(HttpStatusCode.InternalServerError, e);  
             }  
         }  */




        // POST: api/Image
        /*public ResultModel Post([FromBody]ImageModel value)
        {
            ResultModel result = new ResultModel();
            try
            {

                if (value.FileData != null && value.FileData.Length > 0)
                {
                    result.Result = "yes";

                    using (FileStream tmpfs = File.Create(@"C:/travel_pictrue/2333.jpg"))
                    {
                        tmpfs.Write(value.FileData, 0, value.FileData.Length);
                        tmpfs.Flush();
                        tmpfs.Close();
                    }
                }
                result.Result = "true";
                result.Message = "null";
                return result;

            }
            catch(Exception e)
            {
                result.Result = "false";
                result.Message = e.ToString();
                return result;
            }
       

        }
        */




   


        public void Save(string sourcefullname, int dispMaxWidth, int dispMaxHeight)
        {
            try
            {
                Bitmap mg = new Bitmap(sourcefullname);
                Size newSize = new Size(dispMaxWidth, dispMaxHeight);
                Bitmap bp = ResizeImage(mg, newSize);
                if (bp != null)
                    bp.Save(sourcefullname + ".jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            //Path.Combine(output, Path.GetFileName(sourcefullname))
        }
        Bitmap ResizeImage(Bitmap mg, Size newSize)
        {
            double ratio = 0d;
            double myThumbWidth = 0d;
            double myThumbHeight = 0d;
            int x = 0;
            int y = 0;

            Bitmap bp = null;
            try
            {
                if ((mg.Width / Convert.ToDouble(newSize.Width)) > (mg.Height /
                Convert.ToDouble(newSize.Height)))
                    ratio = Convert.ToDouble(mg.Width) / Convert.ToDouble(newSize.Width);
                else
                    ratio = Convert.ToDouble(mg.Height) / Convert.ToDouble(newSize.Height);
                myThumbHeight = Math.Ceiling(mg.Height / ratio);
                myThumbWidth = Math.Ceiling(mg.Width / ratio);

                Size thumbSize = new Size((int)newSize.Width, (int)newSize.Height);
                bp = new Bitmap(newSize.Width, newSize.Height);
                x = (newSize.Width - thumbSize.Width) / 2;
                y = (newSize.Height - thumbSize.Height);
                System.Drawing.Graphics g = Graphics.FromImage(bp);
                g.SmoothingMode = SmoothingMode.HighQuality;
                g.InterpolationMode = InterpolationMode.HighQualityBicubic;
                g.PixelOffsetMode = PixelOffsetMode.HighQuality;
                Rectangle rect = new Rectangle(x, y, thumbSize.Width, thumbSize.Height);
                g.DrawImage(mg, rect, 0, 0, mg.Width, mg.Height, GraphicsUnit.Pixel);
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.ToString());
            }

            return bp;

        }
    }


}

    public class CustomMultipartFormDataStreamProvider : MultipartFormDataStreamProvider
    {
        public CustomMultipartFormDataStreamProvider(string path)
          : base(path)
        { }

        public override string GetLocalFileName(System.Net.Http.Headers.HttpContentHeaders headers)
        {
            string fileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            return fileName + "_" + headers.ContentDisposition.FileName.Replace("\"", string.Empty);//base.GetLocalFileName(headers);
        }
    }

    public class CompressPic
    {
       


}

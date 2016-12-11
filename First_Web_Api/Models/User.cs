using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace First_Web_Api.Models
{
    public class Sign
    {
        public string account { get; set; }

        public string password { get; set; }

        public string username { get; set; }

    }

    public class Login
    {

        public string account { get; set; }

        public string password { get; set; }

    }

    public class ChangePassword
    {

        public string account { get; set; }

        public string oldPassword { get; set; }

        public string newPassword { get; set; }

    }

    public class ForgetPassword
    {

        public string account { get; set; }

        public string newPassword { get; set; }

    }

    public class User
    {
        public string account { get; set; }

        public string userName { get; set; }

        public string sex { get; set; }

        public string introduction { get; set; }

        public string email { get; set; }
    }

}
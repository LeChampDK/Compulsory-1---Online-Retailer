using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModel.Messages.LoginComponent
{
    public class LoginRequest
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}

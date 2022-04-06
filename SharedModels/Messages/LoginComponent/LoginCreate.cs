using System;
using System.Collections.Generic;
using System.Text;

namespace SharedModel.Messages.LoginComponent
{
    public class LoginCreate
    {
        public string email { get; set; }
        public string password { get; set; }
    }
}

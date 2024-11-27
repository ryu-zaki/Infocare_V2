using Org.BouncyCastle.Asn1.X509;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infocare_Project
{
    public class LoginEmpty
    {
        public string Username { get; set; }
        public string Password { get; set; }


        public LoginEmpty() { }
        public LoginEmpty(string username, string password)
        {
            Username = username;
            Password = password;
        }
    }

    internal class FieldsEmptyLogin
    {
       public void EmptyLogin(LoginEmpty nullLogin) 
        {                   
            HomeForm home = new HomeForm();
            home.Show();
        }
    }
}

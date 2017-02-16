using System;
using System.Collections.Generic;
using System.Linq;
using PizzaMore.Data;
using PizzaMore.Utility;

namespace SignIn
{
    public class SignIn
    {
        public static IDictionary<string, string> RequestParams;
        public static Header Header = new Header();
         
        static void Main()
        {
            if (WebUtil.IsPost())
            {
                LogIn();
            }

            ShowPage();

        }

        private static void LogIn()
        {
            RequestParams = WebUtil.RetrievePostParameters();
            string email = RequestParams["email"];
            string password = PasswordHasher.Hash(RequestParams["password"]);

            using (var context = new PizzaMoreContext())
            {
                var user = context.Users.SingleOrDefault(u => u.Email == email);
                if (password==user.Password)
                {
                    var session = new Session()
                    {
                        User = user
                    };
                    if (user!=null)
                    {
                        Header.AddCookie(new Cookie("sid",session.Id.ToString()));
                    }
                    context.Sessions.Add(session);
                    context.SaveChanges();
                }
            }
        }

        private static void ShowPage()
        {
            Header.Print();
            WebUtil.PrintFileContent(Constants.SignIn);
        }
    }
}

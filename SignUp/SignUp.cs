using System;
using System.Collections.Generic;
using PizzaMore.Utility;
using PizzaMore.Data;

namespace SignUp
{
    public class SignUp
    {
        public static IDictionary<string, string> RequestParams;
        public static Header Header = new Header();
         
        static void Main()
        {
            if (WebUtil.IsPost())
            {
                RegisterUser();
            }
            ShowPage();
        }

        private static void ShowPage()
        {
            Header.Print();
            WebUtil.PrintFileContent(Constants.SignUp);
        }

        private static void RegisterUser()
        {
            RequestParams=WebUtil.RetrievePostParameters();
            var email = RequestParams["email"];
            var password = RequestParams["password"];
            var user = new User()
            {
                Email = email,
                Password = PasswordHasher.Hash(password)
            };

            using (var context = new PizzaMoreContext())
            {
                context.Users.Add(user);
                context.SaveChanges();
            }

        }
    }
}

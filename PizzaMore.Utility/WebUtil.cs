using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using PizzaMore.Utility.Interfaces;
using PizzaMore.Data;

namespace PizzaMore.Utility
{
    public static class WebUtil
    {
        
        public static bool IsPost()
        {
            var environmentVar = Environment.GetEnvironmentVariable(Constants.RequestMethod);
            if (environmentVar!=null)
            {
                string requestMethod = environmentVar.ToLower();
                if (requestMethod == "post")
                {
                    return true;
                }
            }
            return false;
        }

        public static bool IsGet()
        {
            var environmentVar = Environment.GetEnvironmentVariable(Constants.RequestMethod);
            string requestMethod = environmentVar.ToLower();

            if (requestMethod=="get")
            {
                return true;
            }
            return false;
        }

        public static IDictionary<string, string> RetrievePostParameters()
        {
            string paramStrings = WebUtility.UrlDecode(Console.ReadLine());
            return RetrieveRequestParameters(paramStrings);
        }

        public static IDictionary<string, string> RetrieveGetParameters()
        {
            string paramStrings = WebUtility.UrlDecode(Environment.GetEnvironmentVariable(Constants.QueryString));
            return RetrieveRequestParameters(paramStrings);
        }

        private static IDictionary<string, string> RetrieveRequestParameters(string paramStrings)
        {
            Dictionary<string, string> resultParams = new Dictionary<string, string>();
            var parameters = paramStrings.Split('&');

            foreach (var param in parameters)
            {
                var pair = param.Split('=');
                var name = pair[0];
                var value = pair[1];
                resultParams.Add(name,value);
            }
            return resultParams;
        }

        public static ICookieCollection GetCookies()
        {
            string cookieString = Environment.GetEnvironmentVariable(Constants.HttoCookie);
            if (string.IsNullOrEmpty(cookieString))
            {
                return new CookieCollection();
            }

            var cookies = new CookieCollection();
            string[] cookieSaves = cookieString.Split(';');
            foreach (var cookieSave in cookieSaves)
            {
                string[] cookiePair = cookieSave.Split('=').Select(x => x.Trim()).ToArray();
                var cookie = new Cookie(cookiePair[0], cookiePair[1]);
                cookies.AddCookie(cookie);
            }
            return cookies;
        }

        public static Session GetSession()
        {
            var cookies = GetCookies();
            if (!cookies.ContainsKey(Constants.SessionIdKey))
            {
                return null;
            }
            var sessionCookie = cookies[Constants.SessionIdKey];
            var ctx = new PizzaMoreContext();

            var session = ctx.Sessions.FirstOrDefault(s => s.Id == (int.Parse(sessionCookie.Value)));
            return session;
        }

        public static void PrintFileContent(string path)
        {
            string content = File.ReadAllText(path);
            Console.WriteLine(content);
        }

        public static void PageNotAloowed()
        {
            PrintFileContent("../../htdocs/pm/game/index.html");
        }
    }
}

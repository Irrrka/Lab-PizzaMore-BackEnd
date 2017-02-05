using System;
using System.Collections.Generic;
using PizzaMore.Utility.Interfaces;

namespace PizzaMore.Utility
{
    public class CookieCollection : ICookieCollection
    {
        private Dictionary<string, Cookie> cookies;

        public CookieCollection()
        {
            this.cookies=new Dictionary<string, Cookie>();
        }
         
        public void AddCookie(Cookie cookie)
        {
            this.cookies.Add(cookie.Name,cookie);
        }

        public void RemoveCookie(string cookieName)
        {
            this.cookies.Remove(cookieName);
        }

        public bool ContainsKey(string key)
        {
            cookies.ContainsKey(key);
        }

        public int Count
        {
            get { return this.cookies.Count; }
        }

        public Cookie this[string key]
        {
            get { return this.cookies[key]; }
            set { this.cookies[key]=value; }
        }
    }
}

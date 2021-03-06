﻿using PizzaMore.Data;
using PizzaMore.Utility;
using System;
using System.Collections.Generic;
using System.Runtime.Remoting.Messaging;

namespace PizzaMore
{
    internal class Home
    {
        private static IDictionary<string, string> RequestParameters;
        private static Session Session;
        private static Utility.Header Header = new Utility.Header();
        private static string Language;

        static void Main()
        {
            AddDefaultLanguageCookie();

            if (WebUtil.IsGet())
            {
                RequestParameters = WebUtil.RetrieveGetParameters();
                TryLogOut();
                Language = WebUtil.GetCookies()["lang"].Value;
            }
            else if (WebUtil.IsPost())
            {
                RequestParameters = WebUtil.RetrievePostParameters();
                Header.AddCookie(new Cookie("lang", RequestParameters["language"]));
                Language = RequestParameters["language"];
            }

            ShowPage();
        }
        private static void AddDefaultLanguageCookie()
        {
            if (!WebUtil.GetCookies().ContainsKey("lang"))
            {
                Header.AddCookie(new Cookie("lang", "EN"));
                Language = "EN";
                ShowPage();
            }
        }
        private static void ShowPage()
        {
            ;
            if (Language.Equals("DE"))
            {
                ServeHtmlBg();
            }
            else
            {
                ServeHtmlEn();
            }
        }

        private static void ServeHtmlBg()
        {
            WebUtil.PrintFileContent("../../htdocs/pm/home-de.html");
        }

        private static void ServeHtmlEn()
        {
            WebUtil.PrintFileContent("../../htdocs/pm/home.html");
        }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace PizzaMore.Utility
{
    public class Header
    {
        public string Type { get;}
        public string Location { get; private set; }
        public ICollection<Cookie> Cookies { get; private set; }

        public Header()
        {
            this.Type = "Content-type: text/html";
            this.Cookies=new List<Cookie>();
        }

        public void AddLocation(string location)
        {
            this.Location = $"Location: {location}";
        }

        public void AddCookie(Cookie cookie)
        {
            this.Cookies.Add(cookie);
        }

        public void Print()
        {
            Console.WriteLine(this.ToString());
        }

        public override string ToString()
        {
            StringBuilder header = new StringBuilder();
            header.AppendLine(this.Type);
            if (this.Cookies.Count>0)
            {
                foreach (var cookie in this.Cookies)
                {
                    header.AppendLine($"Set-Cookie: {cookie.ToString()}");
                } 
            }
            if (this.Location!=null)
            {
                header.AppendLine(this.Location);
            }

            header.AppendLine();
            header.AppendLine();

            return header.ToString();
        }
    }
}

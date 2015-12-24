using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebCounter.BusinessLogicLayer.Providers.SocialNetwork.Vkontakte
{
    public class VKontakteAPIException : Exception
    {
        public string Type { get; set; }

        public VKontakteAPIException(string type, string msg)
            : base(msg)
        {
            Type = type;
        }

        public override string ToString()
        {
            return Type + ": " + base.ToString();
        }

        public VKontakteAPIException()
        { }
    }
}

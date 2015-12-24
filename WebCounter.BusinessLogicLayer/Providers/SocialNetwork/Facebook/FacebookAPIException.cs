using System;

namespace WebCounter.BusinessLogicLayer.Providers.SocialNetwork.Facebook
{
    public class FacebookAPIException : Exception
    {
        public string Type { get; set; }

        public FacebookAPIException(string type, string msg)
            : base(msg)
        {
            Type = type;
        }

        public override string ToString()
        {
            return Type + ": " + base.ToString();
        }

        public FacebookAPIException()
        { }
    }
}

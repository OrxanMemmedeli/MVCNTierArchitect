using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCNTierArchitect.Models
{
    public class GoogleConfigModel
    {
        public const string googleConfig = "GoogleConfig";
        public const string key = "6LemwQgfAAAAALoBZquvBeEmnc1XqqzrpC7WvXIJ";
        public const string secret = "6LemwQgfAAAAALYakVEO4GRvDEcfL_29sUA2AHv6";

        public string Key
        {
            get { return key; }
        }

        public string Secret
        {
            get { return secret; }
        }
    }
}
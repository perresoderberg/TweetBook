﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook.Api.Contract.V1
{
    public static class ApiRoutes
    {
        public const string Root = "api";
        public const string Version = "v1";
        public const string Base = Root + "/" + Version;
        public static class Tweets
        {
            public const string GetAll = Base + "/tweets";
            public const string Get = Base + "/tweets/{tweetId}";
            public const string Create = Base + "/tweets";
            public const string Update = Base + "/tweets/{tweetId}";
            public const string Delete = Base + "/tweets/{tweetId}";
        }

        public static class Identity
        {
            public const string Login = Base + "/identity/login";
            public const string Register = Base + "/identity/register";
        }
    }
}

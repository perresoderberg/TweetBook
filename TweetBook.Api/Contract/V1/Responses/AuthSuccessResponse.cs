using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook.Api.Contract.V1.Responses
{
    public class AuthSuccessResponse : IAuthResponse
    {
        public string Token { get; set; }

    }
}

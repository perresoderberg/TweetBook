using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TweetBook.Api.Contract.V1.Responses
{
    public class AuthFailedResponse : IAuthResponse
    {
        public IEnumerable<string> Errors { get; set; }

    }
}

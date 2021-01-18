using System;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace TweetBook.Extensions
{
    public static class Extentions
    {
        public static string GetUserId(this HttpContext context)
        {

            if (context.User == null)
                return "";

            return context.User.Claims.SingleOrDefault(x => x.Type == "id").Value;
        }
    }
}

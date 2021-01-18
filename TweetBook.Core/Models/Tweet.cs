using System;
//using Dapper.Contrib.Extensions;

namespace TweetBook.Core.DomainModels
{
    //[Table("Tweet")]
    public class Tweet
    {
        //[Key]
        //[Computed]
        public int TweetId { get; set; }



        public Guid GuidId { get; set; }

        public string Name { get; set; }


        public DateTime DateCreated { get; set; }

        public string UserId { get; set; }
    }
}

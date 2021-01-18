using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using TweetBook.Api.Contract.V1;
using TweetBook.Api.Contract.V1.Requests;
using TweetBook.Api.Contract.V1.Responses;
using TweetBook.Core.DomainModels;
using TweetBook.Core.Services;
using TweetBook.Extensions;

namespace TweetBook.Api.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TweetsController : Controller
    {
        private ITweetService _tweetService;
        public TweetsController(ITweetService tweetService)
        {
            _tweetService = tweetService;
        }

        [HttpGet(ApiRoutes.Tweets.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            var posts = await _tweetService.GetAllPostsAsync();
            return Ok(posts);
        }

        [HttpGet(ApiRoutes.Tweets.Get)]
        public async Task<IActionResult> Get([FromRoute] int tweetId)
        {
            var post = await _tweetService.GetPostByIdAsync(tweetId);
            if (post == null)
                return NotFound();

            return  Ok(post);
        }

        [HttpDelete(ApiRoutes.Tweets.Delete)]
        public async Task<IActionResult> Delete([FromRoute] int tweetId)
        {

            bool deleted = await _tweetService.DeletePostByIdAsync(tweetId);
            if (!deleted)
                return NotFound();

            return Ok();
        
        }

        [HttpPut(ApiRoutes.Tweets.Update)]
        public async Task<IActionResult> Update([FromRoute] int tweetId, [FromBody] TweetRequest updatePostRequest)
        {
            if (tweetId == 0)
                return NotFound();

            var UserOwnsThisTweet = await _tweetService.UserOwnsThisTweet(HttpContext.GetUserId(), tweetId);
            if (!UserOwnsThisTweet)
                return BadRequest(new { Error="You do not own this tweet" });

            var post = new Tweet() 
            {
                TweetId = tweetId, 
                Name = updatePostRequest.Name
            };

            var updated = await _tweetService.UpdatePostAsync(post);
            if (updated)
                return Ok();

            return NotFound();
        }

        [HttpPost(ApiRoutes.Tweets.Create)]
        public async Task<IActionResult> Create([FromBody] TweetRequest createPostRequest)
        {
            // Post = Domain Object / Entity
            // CreatePostRequest = Version request object
            // CreatePostResponse = Version response object

            var tweet = new Tweet()
            {
                Name = createPostRequest.Name,
                UserId = HttpContext.GetUserId()
            };

            int IdOfInsertedItem = await _tweetService.CreatePostAsync(tweet);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            var location = $"{baseUrl}/{ApiRoutes.Tweets.Get.Replace("{guidId}", tweet.GuidId.ToString())}";

            tweet.TweetId = IdOfInsertedItem;
            var response = new TweetResponse() { TweetId = IdOfInsertedItem, Name=tweet.Name };
            
            return Created(location, response);
        }
    }
}

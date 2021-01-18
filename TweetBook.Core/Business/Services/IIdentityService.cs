
using System.Threading.Tasks;
using TweetBook.Core.Models;

namespace TweetBook.Core.Business.Services
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string username, string password);
    }
}

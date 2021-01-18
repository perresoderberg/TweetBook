
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using TweetBook.Core.Data;
using TweetBook.Core.Services;

namespace TweetBook.Api.Installers
{
    public class DataInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration Configuration)
        {

            //services.AddDbContext<DataContext>(options =>
            //options.UseSqlServer(
            //    Configuration.GetConnectionString("DefaultConnection")));
            //        services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //            .AddEntityFrameworkStores<DataContext>();

            //services.AddDapperTypeMaps(Assembly.GetExecutingAssembly());

            services.AddTransient<ITweetService, TweetService>();
            services.AddTransient<ITweetRepository, TweetRepository>();

        }
    }
}

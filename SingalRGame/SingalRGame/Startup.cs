using Microsoft.Owin;
using Owin;

namespace SingalRGame
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            app.MapSignalR();
        }
    }
}
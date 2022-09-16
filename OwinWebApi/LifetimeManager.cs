using System;
using System.Threading.Tasks;

namespace OwinWebApi
{
    public class LifetimeManager
    {
        public delegate void EventHandler();
        public event EventHandler Terminate;

        public LifetimeManager() 
        {
            //Обьективно плохое решение, но единственную теоретическую альтернативу что я тут вижу -
            //это System.Web.MVC.ActionFilterAttribute.OnResultExecuted()
            Terminate += (async () =>
            {
                await Task.Delay(2000);
                Environment.Exit(0);
            });
        }

        public async Task OnTerminateProcess()
        {
            await Task.Run(() => Terminate?.Invoke());
        }
    }
}

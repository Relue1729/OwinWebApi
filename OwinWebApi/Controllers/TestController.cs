using OwinWebApi;
using OwinWebApi.Interfaces;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace OwinAPI.Controller
{
    public class TestController : ApiController
    {
        private readonly IRepository repository;
        private readonly LifetimeManager lifetimeManager;
        
        public TestController(IRepository repository, LifetimeManager lifetimeManager)
        {
            this.repository = repository;
            this.lifetimeManager = lifetimeManager;
        }

        /// <summary>
        /// Increments value at given index
        /// </summary>
        /// <param name="index">Index of value to increment</param>
        [HttpPost]
        public async Task<HttpResponseMessage> IncrementArray([FromBody] int index)
        {
            await repository.IncrementAtIndexAsync(index);
            var result = await repository.GetValue(index);
            return Request.CreateResponse(HttpStatusCode.OK, $"result at index {index} set to {result}");
        }

        /// <summary>
        /// Terminates the web app
        /// </summary>
        [HttpGet]
        public HttpResponseMessage Terminate()
        {
            _ = lifetimeManager.OnTerminateProcess();
            return Request.CreateResponse(HttpStatusCode.OK, $"The app will terminate shortly");
        }
    }
}
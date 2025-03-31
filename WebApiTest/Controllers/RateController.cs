using Microsoft.Owin.Hosting.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Description;
using System.Web.Http;
using Swashbuckle.Swagger.Annotations;

namespace WebApiTest.Controllers
{
    [RoutePrefix("travel/v1/quotes")]
    public class RateController:ApiController
    {

        [HttpPost]
        [Route("rate", Name = "CreateQuote")]
        [SwaggerOperation(Tags = new[] { "Rate" })]
        public async Task<HttpResponseMessage> CreateNewRate()
        {
            try
            {
                // Simulate some processing logic here
                // For example, validate input, interact with services, etc.

                return new HttpResponseMessage(System.Net.HttpStatusCode.OK)
                {
                    Content = new StringContent("Success")
                };
            }
            catch (Exception ex)
            {
                // Log the exception details for debugging
                // You can use a logging framework or simply write to console/output
                Console.WriteLine(ex.Message);

                // Return a 500 Internal Server Error response
                return new HttpResponseMessage(System.Net.HttpStatusCode.InternalServerError)
                {
                    Content = new StringContent("An error occurred.")
                };
            }
        }

        //[HttpPost]
        //[Route("", Name = "CreateQuote")]
        //public async Task<HttpResponseMessage> CreateNewRate()
        //{
        //    return new HttpResponseMessage(System.Net.HttpStatusCode.OK) { Content = new StringContent("Success") };
        //}
    }
}

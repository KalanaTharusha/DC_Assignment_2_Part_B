using Bank_Data_DLL;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using RestSharp;

namespace Bank_Web_Application.Controllers
{
    public class BankApiController : Controller
    {

        private String DataService = "http://localhost:5161/";
        private RestClient client;

        [HttpPost]
        public IActionResult Signup([FromBody] User reqBody)
        {
            client = new RestClient(DataService);
            RestRequest request = new RestRequest("api/users", Method.Post);
            request.AddBody(reqBody);
            RestResponse response = client.Execute(request);
            return Ok();
        }

        [HttpPost]
        public IActionResult Login([FromBody] User reqBody)
        {
            client = new RestClient(DataService);
            RestRequest request = new RestRequest("api/users/email/{e}", Method.Get);
            request.AddUrlSegment("e", reqBody.Email);
            RestResponse response = client.Execute(request);
            User user = JsonConvert.DeserializeObject<User>(response.Content);

            if (user != null)
            {
                if (user.Password.Equals(reqBody.Password)) 
                {
                    return new ObjectResult(user) { StatusCode = 200 };
                }
            }

            return new ObjectResult(user) { StatusCode = 404 };
        }

    }
}

using System;
using System.Linq.Expressions;
using ArganaWeedClietApi.Users;
using NLog.LayoutRenderers;

namespace ArganaWeedClietApi
{
    public class Program

    {
        // HttpClient lifecycle management best practices:
        // https://learn.microsoft.com/dotnet/fundamentals/networking/http/httpclient-guidelines#recommended-use
        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("http://localhost:5153/"),
        };

        static void Main(string[] args)
        {
            //Login();
            //GetUsers();
            GetAsync(sharedClient);

        }


        static async Task GetAsync(HttpClient httpClient)
        {
            try
            {
                using HttpResponseMessage response = await httpClient.GetAsync("api/users");



                var jsonResponse = await response.Content.ReadAsStringAsync();
                Console.WriteLine($"{jsonResponse}\n");
            }
            catch (Exception ex) { Console.WriteLine(ex.ToString()); }

            // Expected output:
            //   GET https://jsonplaceholder.typicode.com/api/Users HTTP/1.1
            //   {
            //     "userId": 1,
            //     "id": 3,
            //     "title": "fugiat veniam minus",
            //     "completed": false
            //   }
        }

        static async void Login()
        {
            Console.WriteLine("*******  0  *******");
            Uri uri = new Uri("http://localhost:5153/api/Auth/login");

            LoginRequest loginRequest = new LoginRequest();
            loginRequest.Password = "password";
            loginRequest.Email = "test@example.com";

            try {
                var o = new ClientRequestResponse<LoginRequest, LoginResponse>(loginRequest,"Auth/Login");
                Console.WriteLine("*******  1  *******");
                LoginResponse response = await o.Receive();
                Console.WriteLine("*******  2  *******");
                Console.WriteLine(response);
                Console.WriteLine("******  3 *******");
            }
            catch (Exception ex) { Console.WriteLine(ex.Message);
                Console.WriteLine("******  4 ******");
            }
        }



        static async void GetUsers()
        {
            Console.WriteLine("*******  0  *******");
            
            //Uri uri =new Uri("api/users");

            UsersRequest usersRequest = new UsersRequest();
            

            try
            {
                var o = new ClientRequestResponse<GenericRequest, UsersResponse>("api/users");
                Console.WriteLine("*******  1  *******");
                UsersResponse response = await o.ReceiveGet();
                Console.WriteLine("*******  2  *******");
                Console.WriteLine(response.users);
                Console.WriteLine("******  3 *******");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("******  4 ******");
            }
        }

        static Uri BuildAbsoluteUrl(string relUrl)
        {
            Uri _baseUri = new Uri("http://localhost:5153/");
            return new Uri(_baseUri, relUrl);
        }


    }

}

   

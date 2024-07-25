using System;
using System.Linq.Expressions;
using System.Net;
using ArganaWeedClietApi.Tests;
using ArganaWeedClietApi.Users;
using NLog.LayoutRenderers;

namespace ArganaWeedClietApi
{
    public class Program

    {
        private static HttpClient sharedClient = new()
        {
            BaseAddress = new Uri("https://api.restful-api.dev"),
        };

        public LoginResponse Login_(LoginRequest loginRequest) { 

            if (loginRequest != null)
            {

                lo
            }
        
        }

        static async Task Main(string[] args)
        {

             await  GetItemsAsync();

        }


        static async void Login()
        {
            Console.WriteLine("*******  0  *******");

            LoginRequest loginRequest = new LoginRequest();
            loginRequest.Password = "password";
            loginRequest.Email = "test@example.com";

            try
            {
                var o = new ClientRequestResponse<LoginRequest, LoginResponse>(loginRequest, "/Auth/Login");
                Console.WriteLine("*******  1  *******");
                LoginResponse response = await o.Receive();
                Console.WriteLine("*******  2  *******");
                Console.WriteLine(response);
                Console.WriteLine("******  3 *******");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.WriteLine("******  4 ******");
            }
        }

        private static async Task GetItemsAsync() {


            var o = new ClientRequestResponse<GenericRequest, ItemResponse>("/objects");
            ItemResponse itemResponse = await o.Receive();
            Console.WriteLine($"GET Response: {itemResponse}");

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



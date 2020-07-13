namespace CookTime.Services
{
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text;
    using System.Threading.Tasks;
    using Models;
    using Newtonsoft.Json;
    using Plugin.Connectivity;

    public class ApiService
    {
        public static async Task<Response> CheckConnection()
        {
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Please turn on your internet settings.",
                };
            }

            var isReachable = await CrossConnectivity.Current.IsRemoteReachable(
                "google.com");

            if (!isReachable)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Check you internet connection.",
                };
            }

            return new Response
            {
                IsSuccess = true,
                Message = "Ok",
            };
        }

        public static async Task<Response> GetUser<T>(
            string urlBase,
            string servicePrefix,
            string controller)
        {
            try
            {
                var client = new HttpClient();

                
                var url = string.Format("{0}{1}{2}",urlBase ,servicePrefix, controller);
                Console.WriteLine(url);
                var response = await client.GetAsync(url);
                var result = await client.GetStringAsync(url);
               

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Something was wrong",
                    };
                }

                var list = JsonConvert.DeserializeObject<User>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = list.Password,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "This email is not registered",
                };
            }
        }
    }
}

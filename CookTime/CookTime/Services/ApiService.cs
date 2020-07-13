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
            //Asks if the phone has the internet enabled
            if (!CrossConnectivity.Current.IsConnected)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = "Please turn on your internet settings.",
                };
            }

            //Makes a little test
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

            return new Response //if everything is ok, this is the response
            {
                IsSuccess = true,
                Message = "Ok",
            };
        }

        /*
         * This method allows to take one specific account information
         */
        public static async Task<Response> GetUser<T>(
            string urlBase,
            string servicePrefix,
            string controller)
        {
            try
            {
                //Creates the client and asks for the informaiton 
                var client = new HttpClient();             
                var url = string.Format("{0}{1}{2}",urlBase ,servicePrefix, controller);
                Console.WriteLine(url);
                var response = await client.GetAsync(url);
                var result = await client.GetStringAsync(url);
               
                //If the information was unsuccesfully taken, then it answers that something was wrong
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = "Something was wrong",
                    };
                }

                var list = JsonConvert.DeserializeObject<User>(result); //The object given is of type User
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = list.Password,
                };
            }
            
            catch (Exception ex)
            {
                //If the email doesn't exixts then the url doesn't exists thats why 
                //this answer is in the catch section
                return new Response
                {
                    IsSuccess = false,
                    Message = "This email is not registered",
                };
            }
        }
    }
}

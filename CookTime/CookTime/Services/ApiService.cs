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
                    Result = list,
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

        public static async Task<Response> GetList<T>(
            string urlBase,
            string servicePrefix,
            string controller)
        {
            try
            {
                var client = new HttpClient();
                client.BaseAddress = new Uri(urlBase);
                var url = string.Format("{0}{1}{2}",urlBase , servicePrefix, controller);
                var response = await client.GetAsync(url);
                var result = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = result,
                    };
                }

                var list = JsonConvert.DeserializeObject<List<Recipe>>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Ok",
                    Result = list,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }

        /*
         * This method alows to post recipes and user to the server
         */
        public static async Task<Response> Post<T>(
            string urlBase,
            string servicePrefix,
            string controller,
            T model)
        {
            try
            {
                //Creates a Json file based ob the object given 
                var request = JsonConvert.SerializeObject(model);

                //With this Json it creates a content needed for posting the user or recipe
                var content = new StringContent(
                    request,
                    Encoding.UTF8,
                    "application/json");

                //Creates a client and posts its information to the server in the specific url
                var client = new HttpClient();
                var url = string.Format("{0}{1}{2}", urlBase, servicePrefix, controller);
                var response = await client.PostAsync(url, content);

                //If something goes wrong then it gives the message
                if (!response.IsSuccessStatusCode)
                {
                    return new Response
                    {
                        IsSuccess = false,
                        Message = response.StatusCode.ToString(),
                    };
                }

                //Creates the json for the user and returns it in the response
                var result = await response.Content.ReadAsStringAsync();
                var newRecord = JsonConvert.DeserializeObject<T>(result);
                return new Response
                {
                    IsSuccess = true,
                    Message = "Record added OK",
                    Result = newRecord,
                };
            }
            catch (Exception ex)
            {
                return new Response
                {
                    IsSuccess = false,
                    Message = ex.Message,
                };
            }
        }
    }
}

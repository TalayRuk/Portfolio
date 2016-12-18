using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using RestSharp;
using RestSharp.Authenticators;

namespace VichitraPortfolio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Make a connection with the server where the API is located 
            var client = new RestClient("https://api.github.com/search/repositories?page=1&q=user:talayruk&sort=stars&order=desc");
            //Create the request, and add the physical path to the specific API controller an choose the HTTP method
            var request = new RestRequest("Client_ID/aa0091094cf01ddc2acebef180d37ef928bbe616/Repos", Method.GET);
            //Add parameters to our request. We have to set repository
            request.AddHeader("User-Agent", "talayruk");
            //To get metadata in search results, specify the text-match media type in Accept header
            request.AddHeader("Accept", "application/vnd.github.v3.text-match+json");
            request.AddParameter("Access_token", "81fb8907e791f232977c6c3ba867e1d1cd8b2c09");
            //request.AddParameter("q=user", "talayruk");
            //request.AddParameter("Sort", "stars");
            //request.AddParameter("Order", "desc");
            //request.AddParameter("Number", "3");

            //Give the  client appropriate credentials
            client.Authenticator = new HttpBasicAuthenticator("2d2756c3dd0a7e9a23c7", "aa0091094cf01ddc2acebef180d37ef928bbe616");
            //We initialize a new RestResponse variable named respons
            var response = new RestResponse();
            //The request ismade w/an asynchronous method, allows us to await asynchronous calls in a "synchronous" way. We set response = to the response from our request, which we make in the method 1) and then cast as the type REstresponse
             
            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();
           
            //Console.WriteLine()) to process the request The response has content property
            Console.WriteLine(response.Content);
            Console.ReadLine();
        }
        //1)
        public static Task<IRestResponse> GetResponseContentAsync(RestClient theClient, RestRequest theRequest)
        {
            var tcs = new TaskCompletionSource<IRestResponse>();
            theClient.ExecuteAsync(theRequest, response => {
                tcs.SetResult(response);
            });
            return tcs.Task;
        }
    }
}

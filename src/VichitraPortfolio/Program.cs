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
            var client = new RestClient("https://api.github.com/search/repositories");
            //Create the request, and add the physical path to the specific API controller an choose the HTTP method
            var request = new RestRequest("Client_ID/aa0091094cf01ddc2acebef180d37ef928bbe616/Repos", Method.GET);
            //Add parameters to our request. We have to set repository
            //request.AddHeader("User-Agent", "talayruk");
            //To get metadata in search results, specify the text-match media type in Accept header
            //request.AddHeader("Accept", "application/vnd.github.v3.text-match+json");
            //request.AddParameter("q=user", "talayruk");
            //request.AddParameter("Sort", "stars");
            //request.AddParameter("Order", "desc");

            //Give the  client appropriate credentials
            client.Authenticator = new HttpBasicAuthenticator("2d2756c3dd0a7e9a23c7", "aa0091094cf01ddc2acebef180d37ef928bbe616");
            //Execute the request to the client. ExecuteAsync needs callback(which we include here as a Console.WriteLine()) to process the request
            client.ExecuteAsync(request, response =>
            {
                Console.WriteLine(response);
            });
            Console.ReadLine();

        }
    }
}

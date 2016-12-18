using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using RestSharp;

namespace VichitraPortfolio
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Make a connection with the server where the API is located 
            var client = new RestClient("https://api.github.com");
            //Create the request, and add the physical path to the specific API controller an choose the HTTP method
            var request = new RestRequest("Accounts/", Method.GET);
            //Add parameters to our request. We have to set repository

            //Giv the  client appropriate credentials

        }
    }
}

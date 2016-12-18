using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using RestSharp.Authenticators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VichitraPortfolio.Models
{
    public class Repo 
    {
        public string Full_name { get; set; }
        public string Stargazers_count { get; set; }
        public string Html_url { get; set; }
        public string Description { get; set; }
        public string Language { get; set; }

        public static List<Repo> GetRepos()
        {
            //Make a connection with the server where the API is located 
            var client = new RestClient("https://api.github.com/search/repositories?page=1&q=user:talayruk&sort=stars:>0&order=desc");
            //Create the request, and add the physical path to the specific API controller an choose the HTTP method 
            //Can't add /Itmes.json to the account since there's no account just leave it empty string in order to get the json 
            var request = new RestRequest("", Method.GET);
            //Add parameters to our request. We have to set repository
            request.AddParameter("Access_token", "81fb8907e791f232977c6c3ba867e1d1cd8b2c09");
            request.AddHeader("User-Agent", "talayruk");
            //To get metadata in search results, specify the text-match media type in Accept header
            request.AddHeader("Accept", "application/vnd.github.v3.text-match+json");
            //Give the  client appropriate credentials & add /Itmes.json to the account string to get the response in JSON format **"items" is Json keys & its value is an array of JSON-formatted data about Repos
            client.Authenticator = new HttpBasicAuthenticator("/Itmes.json", "81fb8907e791f232977c6c3ba867e1d1cd8b2c09");
            //We initialize a new RestResponse variable named response

            var response = new RestResponse();
            //The request ismade w/an asynchronous method, allows us to await asynchronous calls in a "synchronous" way. We set response = to the response from our request, which we make in the method 1) and then cast as the type REstresponse

            Task.Run(async () =>
            {
                response = await GetResponseContentAsync(client, request) as RestResponse;
            }).Wait();

            //Console.WriteLine()) to process the request The response has content property
            //Trn the array stored as response.Content coverts the Json-formatted string response.Content into JObject (JObject comes from using NewtonSoft.Json.Linq library & is a .NET obj we can treat as JSON)
            //JSON key is "items" we can pull this array out as a JSON object by deserializing it. DeserializeObject went into the data for each repo and found those keys to create item objects for us
            //for this to work the property Full_name has to match the JSON key. This means that the Full_name property for our Repo class needs to be named "Full_name"
            JObject jsonResponse = JsonConvert.DeserializeObject<JObject>(response.Content);
            var repoList = JsonConvert.DeserializeObject<List<Repo>>(jsonResponse["items"].ToString());
            return repoList;
            //foreach (var repo in repoList)
            //{
            //    Console.WriteLine("Full_name: {0}", repo.Full_name);
            //    Console.WriteLine("Stargazers_count: {0}", repo.Stargazers_count);
            //    Console.WriteLine("Html_url: {0}", repo.Html_url);
            //    Console.WriteLine("Description: {0}", repo.Description);
            //    Console.WriteLine("Language: {0}", repo.Language);
            //}

            //Console.WriteLine(jsonResponse["items"]); **B/c items is the key where the data is stored, can't change it to somethingelse
            //Console.ReadLine();
        }
        //GetRepos should show all the repositories, don't need another showrepositories unlike sending message we have to have another form to send the message but since we will not be sending these repos anywhere !
        //public void ShowRespositories()
        //{
        //    var client = new RestClient("https://api.github.com/search/repositories?page=1&q=user:talayruk&sort=stars:>0&order=desc");
        //    var request = new RestRequest("", Method.POST);
        //    request.AddParameter("Full_name", Full_name);
        //    request.AddParameter("Stargazers_count", Stargazers_count);
        //    request.AddParameter("Html_url", Html_url);
        //    request.AddParameter("Description", Description);
        //    request.AddParameter("Language", Language);
        //    client.Authenticator = new HttpBasicAuthenticator("", "81fb8907e791f232977c6c3ba867e1d1cd8b2c09");
        //    client.ExecuteAsync(request, response => {
        //        Console.WriteLine(response.Content);
        //    });
        //}
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


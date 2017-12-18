using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.Serialization.Json;
using System.Threading.Tasks;

namespace WebAPIClient
{
    class Program
    {
        private static async Task ProcessRepositories()
        {
            var serializer = new DataContractJsonSerializer(typeof(List<repo>));

            var client = new HttpClient();
            // configured to accept the GitHub JSON responses
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/vnd.github.v3+json"));
            client.DefaultRequestHeaders.Add("User-Agent", ".NET Foundation Repository Reporter");

            var streamTask = client.GetStreamAsync("https://api.github.com/orgs/dotnet/repos");
            var repositories = serializer.ReadObject(await streamTask) as List<repo>;

            foreach (var repo in repositories)
                Console.WriteLine(repo.name);
        }

        static void Main(string[] args)
        {
            ProcessRepositories().Wait();
        }
    }
}

using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using BooksCollector.Core;
using Newtonsoft.Json;

namespace BooksCollector.Client
{
    public class SubmitService
    {
        public static async Task<Result> SubmitAsync(Book book)
        {
            var client = new HttpClient
            {
                BaseAddress = new Uri("http://localhost:33300")
            };
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            var response = await client.PostAsJsonAsync("/api/Books", book);
            response.EnsureSuccessStatusCode(); // throws an exception on error

            var result = JsonConvert.DeserializeObject<Result>(
                await response.Content.ReadAsStringAsync());

            return result;
        }



    }
}
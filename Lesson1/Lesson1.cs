using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Net.Http;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ASP_NET_CORE_WEB_API.Lesson1
{
    public class Lesson1
        {
            private static readonly HttpClient client = new HttpClient();

            private static readonly List<Task<Article>> myList = new List<Task<Article>>();
        
            public static async Task Main(string[] args)
            {
                for (int i = 4; i < 14; i++)
                {
                    myList.Add(ProcessRepositories(i));
                }
            
                await Task.WhenAll(myList);

                using var writer = File.CreateText("../../Lesson1/result.txt");
                myList.ForEach(el =>
                {
                    writer.WriteLine(el.Result.UserId);
                    writer.WriteLine(el.Result.Id);
                    writer.WriteLine(el.Result.Title);
                    writer.WriteLine(el.Result.Body);
                    writer.Write(writer.NewLine);
                });
            }

            private static async Task<Article> ProcessRepositories(int i)
            {
                var stringTask = client.GetStringAsync("https://jsonplaceholder.typicode.com/posts/?id="+i);
                var msg = await stringTask;
                var articles =JsonSerializer.Deserialize<List<Article>>(msg);
                return articles.First();
            }
        }

        public class Article
        {
            [JsonPropertyName("userId")]
            public int UserId { get; set; }
            [JsonPropertyName("id")]
            public int Id { get; set; }
            [JsonPropertyName("title")]
            public string Title { get; set; }
            [JsonPropertyName("body")]
            public string Body { get; set; }
        }

}
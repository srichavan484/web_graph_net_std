using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Text.Json;
using GraphQL.Client.Http;
using GraphQL.Client.Serializer.Newtonsoft;
using GraphQL;
using System.Threading.Tasks;
using System.IO;
using System.Net;
using System.Text;
using System.Net.Http.Json;


namespace SwayamEQ
{
    public partial class GraphQL_client_test1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Write("Hello");

            // To use NewtonsoftJsonSerializer, add a reference to NuGet package GraphQL.Client.Serializer.Newtonsoft
            var graphQLClient = new GraphQLHttpClient("https://spacex-production.up.railway.app/", new NewtonsoftJsonSerializer());
            var heroRequest = new GraphQLRequest
            {
                Query = @"
                        query ExampleQuery {  company {    ceo  }
                                         }"
            };

          // InvokeWebClient();
          RegisterAsyncTask(new PageAsyncTask(PageLoadAsync));

            //InvokeGraphQLAPI().GetAwaiter().GetResult();

           // InvokeGraphQLAPI();

            //var myTask = StartMyTask(); // call your method which will return control once it hits await
                                        // now you can continue executing code here
           // string result1 = myTask.Result;


        }
        public async Task PageLoadAsync()
        {
            //perform your actions here, including calling async methods and awaiting their results


         txtResult.Text= await  InvokeGraphQLAPI();

           // Task<string> downloadTask = new WebClient().DownloadStringTaskAsync("http://www.example.com");
           

           // TextBox1.Title = "We can do some actions while waiting for the task to complete";
          // Response.Write(downloadTask.Result);
           // Response.Write(await downloadTask);

            //TextBox2.Title = await downloadTask;
        }

        public void InvokeWebClient()
        {
            try
            {
                string url = "https://spacex-production.up.railway.app/";
                //string data = "{\"service\":\"absence.list\", \"company_id\":3}";

                string data = @"query ExampleQuery {  company {    ceo  } }";

                WebRequest myReq = WebRequest.Create(url);
                myReq.Method = "POST";
                myReq.ContentLength = data.Length;
                
                //myReq.ContentType = "application/json; charset=UTF-8";
               myReq.ContentType = "application/graphql; charset=UTF-8";

                string usernamePassword = "YOUR API TOKEN HERE" + ":" + "x";

                UTF8Encoding enc = new UTF8Encoding();

                // myReq.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(enc.GetBytes(usernamePassword)));

                //requestMessage.Content = JsonContent.Create(new { Name = "John Doe", Age = 33 });

                using (Stream ds = myReq.GetRequestStream())
                {
                    //ds.Write(data,data.Length);
                    ds.Write(enc.GetBytes(data), 0, data.Length);
                }


                WebResponse wr = myReq.GetResponse();
                Stream receiveStream = wr.GetResponseStream();
                StreamReader reader = new StreamReader(receiveStream, Encoding.UTF8);
                string content = reader.ReadToEnd();
                Response.Write(content);
            }
            catch (Exception ex) {
                Response.Write(ex.Message); 
            }
        }





        public async Task<string> StartMyTask()
        {
            string sURL = "https://spacex-production.up.railway.app/";

            var graphQLClient = new GraphQLHttpClient(sURL, new NewtonsoftJsonSerializer());

            var heroRequest = new GraphQLRequest
            {
                Query = @"
                        query ExampleQuery {  company {    ceo  }
                                         }"
            };

            var graphQLResponse = await graphQLClient.SendQueryAsync<data>(heroRequest);
            Console.WriteLine("raw response:");
            Console.WriteLine(JsonSerializer.Serialize(graphQLResponse, new JsonSerializerOptions { WriteIndented = true }));

            Console.WriteLine();
            Console.WriteLine($"Name: {graphQLResponse.Data.company}");

            // code to execute once foo is done
            return "";
        }

        public void Foo()
        {

        }

        public static async Task<string> InvokeGraphQLAPI()
        {
            try
            {
                string sURL = "https://spacex-production.up.railway.app/";

                var graphQLClient = new GraphQLHttpClient(sURL, new NewtonsoftJsonSerializer());

                var heroRequest = new GraphQLRequest
                {
                    Query = @"
                        query ExampleQuery {  company {    ceo  }
                                         }"
                };

                var graphQLResponse = await graphQLClient.SendQueryAsync<data>(heroRequest);
                Console.WriteLine("raw response:");
                Console.WriteLine(JsonSerializer.Serialize(graphQLResponse, new JsonSerializerOptions { WriteIndented = true }));
                
                Console.WriteLine();
                Console.WriteLine($"Name: {graphQLResponse.Data.company}");
                //var films = string.Join(", ", graphQLResponse.Data.ceo.FilmConnection.Films.Select(f => f.Title));
                //Console.WriteLine($"Films: {films}");
                return graphQLResponse.Data.company.ceo;

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
               // Response.Write("Hello");
            }
            return "";


        }

    }



    public class data
    {
        public company company { get; set; }
    }


    public class company
    {
        public string ceo { get; set; }
    }

}
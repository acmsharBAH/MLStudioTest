// This code requires the Nuget package Microsoft.AspNet.WebApi.Client to be installed.
// Instructions for doing this in Visual Studio:
// Tools -> Nuget Package Manager -> Package Manager Console
// Install-Package Microsoft.AspNet.WebApi.Client

using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace CallRequestResponseService
{
    public class ScoreData
    {
        public Dictionary<string, string> FeatureVector { get; set; }
        public Dictionary<string, string> GlobalParameters { get; set; }
    }

    public class ScoreRequest
    {
        public string Id { get; set; }
        public ScoreData Instance { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            //InvokeRequestResponseService().Wait();
            //fake comment
            SQLTest();
        }

        static void SQLTest()
        {
            Console.WriteLine("1111111111111111");
            SqlConnection myConnection = new SqlConnection("Server=tcp:q69lftfix2.database.windows.net,1433;Database=bahsigapps-sql;User ID=azureadmin@q69lftfix2;Password=Password123!@#;Trusted_Connection=False;Encrypt=True;Connection Timeout=30;");
                
                
                //"user id=azureadmin@q69lftfix2;" +
                //                       "password=Password123!@#;"+
                //                       "Server=tcp:q69lftfix2.database.windows.net,1433;" +
                //                       "Trusted_Connection=false;" +
                //                       "database=bahsigapps-sql; " +
                //                       "Encrypt=True;"+
                //                       "connection timeout=15");
            try
            {
                myConnection.Open();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
//SqlCommand cmd = new SqlCommand("CREATE TABLE Game1("
//                                + "[ID] [int] IDENTITY(1,1) NOT NULL,"
//                                + "[Offense] [nvarchar](20) NOT NULL,"
//                                + "[DriveNum] [int] NOT NULL,"
//                                + "[PlayNum] [int] NOT NULL,"
//                                + "[Quarter] [int] NOT NULL,"
//                                + "[Minute] [int] NOT NULL,"
//                                + "[QTRSection] [int] NOT NULL,"
//                                + "[PtDiff] [int] NOT NULL,"
//                                + "[TIMO] [int] NOT NULL,"
//                                + "[TIMD] [int] NOT NULL,"
//                                + "[Down] [int] NOT NULL,"
//                                + "[YTG_BIN] [nvarchar](20) NOT NULL,"
//                                + "[Zone] [int] NOT NULL)", myConnection);
//                    cmd.ExecuteNonQuery();
//                    cmd.Connection.Close();

            Console.WriteLine("22222");
            try
            {
                myConnection.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
            Console.WriteLine("Press return to continue");
            Console.ReadLine();
        }


        static async Task InvokeRequestResponseService()
        {
            using (var client = new HttpClient())
            {
                ScoreData scoreData = new ScoreData()
                {
                    FeatureVector = new Dictionary<string, string>() 
                    {
                        { "OFF", "NE" },
                        { "DSEQ", "3" },
                        { "QTR", "1" },
                        { "MIN", "5" },
                        { "QTRSECTION", "2" },
                        { "PTDIFF", "7" },
                        { "TIMO", "3" },
                        { "TIMD", "3" },
                        { "DWN", "3" },
                        { "YTG_BIN", "Long" },
                        { "ZONE", "1" },
                    },
                    GlobalParameters =
                        new Dictionary<string, string>()
                        {
                        }
                };

                ScoreRequest scoreRequest = new ScoreRequest()
                {
                    Id = "score00001",
                    Instance = scoreData
                };

                const string apiKey = "xTQkImuyYMnLckJb3KV05uAOW9wUv0vKAiEFRgBjOWUoXGhJ7Kvilp3k6aSE2PEAPu3yuQxZl5MH/eEhTy/QtA=="; // Replace this with the API key for the web service
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", apiKey);

                client.BaseAddress = new Uri("https://ussouthcentral.services.azureml.net/workspaces/7e69e85c761a4003b4ba9efe1952b716/services/bc16c08f0e7c438e8b1d7378bb3f0a28/score");
                HttpResponseMessage response = await client.PostAsJsonAsync("", scoreRequest);
                if (response.IsSuccessStatusCode)
                {
                    string result = await response.Content.ReadAsStringAsync();
                    Console.WriteLine("Result: {0}", result);
                }
                else
                {
                    Console.WriteLine("Failed with status code: {0}", response.StatusCode);
                }
                Console.WriteLine("Press return to continue");
                Console.ReadLine();
            }
        }
    }
}
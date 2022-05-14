using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleWebApp
{
    internal class Program
    {
        static void Main(string[] args)
        {
            
            Console.WriteLine("Enter id to find bundleID, or x to quit.");
            string id = Console.ReadLine();
            GetBundleId(id);
        }

        private static void GetBundleId(string id)
        {
            string localFilePath = @"C:\Users\steph\OneDrive\Desktop\file.txt";

            HttpClient myWebClient = new HttpClient();

            while (id != "x")
            {
                try
                {

                    string remoteUri = "https://itunes.apple.com/lookup?id=" + id;
                    var GetTask = myWebClient.GetAsync(remoteUri);//make the call to itunes api

                    //save the file onto my desktop
                    using (var fs = new FileStream(localFilePath, FileMode.CreateNew))
                    {
                        var ResponseTask = GetTask.Result.Content.CopyToAsync(fs);
                        ResponseTask.Wait(14);
                    }

                    JObject o1 = JObject.Parse(File.ReadAllText(localFilePath));

                    Console.WriteLine(o1["results"][0]["bundleId"]); //returns the bundleId field as a string to the console
                    File.Delete(localFilePath); //now remove the file from the desktop
                }
                catch (Exception e)
                {
                    Console.WriteLine("Error(s):  \n " + e);
                }

                id = Console.ReadLine(); //detect key press to either continue application or quit
            }

        }

    }
}

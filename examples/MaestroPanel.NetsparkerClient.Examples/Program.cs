using MaestroPanel.NetsparkerClient.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MaestroPanel.NetsparkerClient.Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            var client = new NetsparkerClient(
                         new HttpRequest("https://www.netsparkercloud.com/api/1.0/"));

            //Netsparker Authenticate
            client.Authenticate("your-access-token-from-netsparker");

            //Start New Scan
            var newScanResult = client.Scan()
                                      .New(new NewScanTaskApiModel
                                      {
                                          TargetUri = "http://yourdomain.com",
                                          FormAuthUsername = "username"
                                      });
            Console.WriteLine("Scan Id:{0}", newScanResult.Data[0].Id);


            //Check Existing Scan Status
            var statusResult = client.Scan()
                              .Status(newScanResult.Data[0].Id);
            Console.WriteLine("State : {0}", statusResult.Data.State);

            //Cancel Existing Scan
            var cancelResult = client.Scan()
                                     .Cancel(newScanResult.Data[0].Id);
            Console.WriteLine("HttpStatusCode : {0}", cancelResult.Status);

            Console.ReadKey();
        }
    }
}

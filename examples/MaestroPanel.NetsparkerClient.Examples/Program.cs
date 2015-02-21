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
            var client = new NetsparkerRestClient(
                         new HttpRequest("https://www.netsparkercloud.com/api/1.0/"));

            //Netsparker Authenticate
            client.Authenticate("your-access-token-from-netsparker");

            ExecuteResult<VerifyOwnershipResult> vResult = client.WebSite().Verify(new VerifyApiModel
            {
                VerificationMethod = VerificationMethod.File,
                VerificationSecret = "website id",
                WebsiteUrl = "http://maestropanel.com"
            });

            Console.WriteLine(vResult.Content);
            Console.WriteLine(vResult.ErrorMessage);
            Console.WriteLine(vResult.Status);

            Console.ReadKey();

            //Delete Existing WebSite
            var deleteWebSiteResult = client.WebSite()
                                            .Delete(new DeleteWebsiteApiModel
                                                      {
                                                          RootUrl = "http://demo.maestrodemo.com/"
                                                      });
            //New WebSite
            var addWebSiteResult = client.WebSite()
                                         .New(new NewWebsiteApiModel
                                         {
                                             FormAuthentication = new FormAuthentication
                                             {
                                                 LoginFormUrl = "http://demo.maestropanel.org:9715",
                                                 Personas = new List<FormAuthenticationPerson> 
                                                 {
                                                      new FormAuthenticationPerson
                                                      {
                                                           IsDefault = true,
                                                           Username = "admin",
                                                           Password = "1"
                                                      }
                                                 }
                                             },
                                             Groups = new List<string> { "Default" },
                                             Name = "added by emre",
                                             RootUrl = "http://demo.maestropanel.org:9715"
                                         });

            //Start New Scan
            var newScanResult = client.Scan()
                                      .New(new NewScanTaskApiModel
                                      {
                                          TargetUri = "http://yourdomain.com",
                                          FormAuthUsername = "username"
                                      });
            Console.WriteLine("Scan Id:{0}", newScanResult.Data[0].Id);

            //Scan List
            var scanListResult = client.Scan()
                                       .List();
            Console.WriteLine("Scan Count : {0}", scanListResult.Data.List.Count);


            //Check Existing Scan Status
            var statusResult = client.Scan()
                              .Status(newScanResult.Data[0].Id);
            Console.WriteLine("State : {0}", statusResult.Data.State);


            //Completed Scan ReTest 
            var retestResult = client.Scan()
                                     .Retest(new BaseScanApiModel
                                     {
                                         BaseScanId = newScanResult.Data[0].Id
                                     });
            Console.WriteLine(retestResult.Data.State);

            //Cancel Existing Scan
            var cancelResult = client.Scan()
                                     .Cancel(newScanResult.Data[0].Id);
            Console.WriteLine("HttpStatusCode : {0}", cancelResult.Status);

            Console.ReadKey();
        }
    }
}

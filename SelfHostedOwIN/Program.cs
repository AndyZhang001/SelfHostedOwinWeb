using Microsoft.Owin.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace SelfHostedOwinWeb
{    
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                //For non-admin user, need to register url by using netsh. 
                //For example(cmd):
                //    netsh http add urlacl url=http://+:7331/ user=Everyone
                string managementPort = "7331";
                StartOptions options = new StartOptions();               
                
                //Register for all ips
                options.Urls.Add(Program.GetUrl("+", managementPort));                
                using (WebApp.Start<Startup>(options))
                {
                    Console.WriteLine("Inited. Waiting for request.");                                        
                    Console.Read();
                }                
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            Console.Read();
        }

        private static string GetUrl(string host, string managementPort)
        {
            string url = "http://" + host + ":" + managementPort;
            Console.WriteLine(string.Format("Management url {0} is registered.", url));
            return url;
        }
    }
}

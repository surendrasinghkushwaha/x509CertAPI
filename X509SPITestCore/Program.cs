using System;
using System.IO;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.EnvironmentVariables;
namespace X509SPITestCore
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").AddEnvironmentVariables().Build();
            var localPathForCertificate = configuration.GetSection("appSetting")["LocalPathForCertificate"];
            HttpWebRequest req = (HttpWebRequest)WebRequest.Create("https://mytest.com/api/values/1");
            req.ClientCertificates.Add(GetClientCertFromLocalPath(localPathForCertificate));
            HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
            using (var readStream = new StreamReader(resp.GetResponseStream()))
            {
                var resultString = readStream.ReadToEnd();
                Console.WriteLine(resultString);
            }

            Console.ReadLine();
        }

        static X509Certificate GetClientCertFromLocalPath(string certificateFilePat)
        {
            X509Certificate2 x509Certificate2 = null;
            try
            {
                x509Certificate2 = new X509Certificate2(Path.Combine(certificateFilePat, "mytestCA.pfx"), "1");
                if (x509Certificate2 != null)
                {
                    return x509Certificate2;
                }
                else
                {
                    throw new Exception("Unable to locate the file certificate.");
                }

            }
            catch
            {
                throw;
            }
        }
    }
}

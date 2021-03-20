using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.IO;

namespace X509APITest
{
    class Program
    {
        static void Main(string[] args)
        {
            PostCall();
        }

        public static void PostCall()
        {
            var handler = new WebRequestHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            var useLocalPathForCertificate = ConfigurationSettings.AppSettings["UseLocalPathForCertificate"];
            if (useLocalPathForCertificate == "true")
            {
                var localPathForCertificate = ConfigurationSettings.AppSettings["LocalPathForCertificate"];

                handler.ClientCertificates.Add(GetClientCertFromLocalPath(localPathForCertificate));
            }
            else
            {
                handler.ClientCertificates.Add(GetInstalledClientCert());
            }


            handler.UseProxy = false;

            var client = new HttpClient(handler);
            var result = client.GetAsync("https://mytest.com/api/values/1").GetAwaiter().GetResult();
            var resultString = result.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            Console.WriteLine(resultString);
            Console.ReadKey();
        }

        private static X509Certificate GetInstalledClientCert()
        {
            X509Store userCaStore = new X509Store(StoreName.My, StoreLocation.CurrentUser);
            try
            {
                userCaStore.Open(OpenFlags.ReadOnly);
                X509Certificate2Collection certificatesInStore = userCaStore.Certificates;
                X509Certificate2Collection findResult = certificatesInStore.Find(X509FindType.FindBySubjectName, "mytest.com", true);
                X509Certificate2 clientCertificate = null;
                if (findResult.Count == 1)
                {
                    clientCertificate = findResult[0];
                }
                else
                {
                    throw new Exception("Unable to locate the correct client certificate.");
                }
                return clientCertificate;
            }
            catch
            {
                throw;
            }
            finally
            {
                userCaStore.Close();
            }
        }
        private static X509Certificate GetClientCertFromLocalPath(string certificateFilePath)
        {
            X509Certificate2 x509Certificate2 = null;
            try
            {
                x509Certificate2 = new X509Certificate2(Path.Combine(certificateFilePath, "mytestCA.pfx"), "1");
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

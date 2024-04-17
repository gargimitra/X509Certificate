using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace CertificateOperations
{
    internal class Program
    {
        static void Main(string[] args)
        {
            byte[] certificateFromLeafBlob = Convert.FromBase64String("<pfx blob>");
            RetrieveIntermediateCertificates(certificateFromLeafBlob);
        }

        private static void RetrieveIntermediateCertificates(byte[] certificateFromLeafBlob)
        {
            var certCollection = new X509Certificate2Collection();
            certCollection.Import(certificateFromLeafBlob, "<pfx password>", X509KeyStorageFlags.Exportable | X509KeyStorageFlags.PersistKeySet);
            foreach (X509Certificate2 x509Cert in certCollection)
            {
                var thumbprint = x509Cert.Thumbprint;
                var certBlob = x509Cert.GetRawCertData();
                var subjectName = x509Cert.Subject;
                var issuerName = x509Cert.Issuer;
                var expirationTime = x509Cert.NotAfter;
                var isPfx = x509Cert.HasPrivateKey;
                var isRoot = string.Equals(x509Cert.Issuer, x509Cert.Subject, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}

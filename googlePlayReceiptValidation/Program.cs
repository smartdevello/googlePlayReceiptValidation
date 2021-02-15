using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Google.Apis.AndroidPublisher.v3;
using Google.Apis.AndroidPublisher.v3.Data;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Newtonsoft.Json;

namespace googlePlayReceiptValidation
{
    class Program
    {
        static AndroidPublisherService service;
        static GoogleCredential credential;
        static string[] Scopes = { AndroidPublisherService.Scope.Androidpublisher};

        static void Main(string[] args)
        {
            var program = new Program();
            program.getCredentials();
            string packageName = "app.houseofquiz.bobblquiz";
            string productId = "app.bobblquiz.buy.100";
            string purchaseToken = "hamcfigdpllhdmhpbgdcmlcd.AO-J1Ox0h-N_oF-pEu_9a1Qw7TepNVe7Oz61eXolvvE1y8rKGkes9yu55ovJghBlMKPxEJIS4sbF5kLlHMmxFxDolwqHLvj-wp1FJMDPlmj7SsSfCV9a1oY";
            program.getPurchasesproducts(packageName, productId, purchaseToken);
        }
        public void getPurchasesproducts(string packageName, string productId, string purchaseToken)
        {
            var request = service.Purchases.Products.Get(packageName, productId, purchaseToken);
            try
            {
                ProductPurchase productPurchase = request.Execute();
                
                Console.WriteLine("kind {0}", productPurchase.Kind);
                Console.WriteLine("consumptionState {0}", productPurchase.ConsumptionState);
                Console.WriteLine("orderId {0}", productPurchase.OrderId);
                Console.WriteLine("PurchaseType {0}", productPurchase.PurchaseType);
                Console.WriteLine("PurchaseToken {0}", productPurchase.PurchaseToken);
                Console.WriteLine("ProductId {0}", productPurchase.ProductId);
                Console.WriteLine("quantity {0}", productPurchase.Quantity);
                Console.WriteLine("obfuscatedExternalAccountId {0}", productPurchase.ObfuscatedExternalAccountId);
                Console.WriteLine("obfuscatedExternalProfileId {0}", productPurchase.ObfuscatedExternalProfileId);
                Console.WriteLine("regionCode {0}", productPurchase.RegionCode);
            }
            catch (Exception ex)
            {                
                Console.WriteLine(ex.ToString());
            }
        }
        public void getCredentials()
        {
            
            using(var stream = new FileStream("google-serviceaccount.json", FileMode.Open, FileAccess.Read))
            {
                credential = GoogleCredential.FromStream(stream).CreateScoped("https://www.googleapis.com/auth/androidpublisher");

                service = new Google.Apis.AndroidPublisher.v3.AndroidPublisherService(new BaseClientService.Initializer()
                {
                    HttpClientInitializer = credential
                });
                
            }


        }
    }
}

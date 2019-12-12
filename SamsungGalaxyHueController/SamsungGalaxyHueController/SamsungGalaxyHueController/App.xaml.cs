using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

namespace SamsungGalaxyHueController
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();
            ServicePointManager.ServerCertificateValidationCallback += (sender, cert, chain, sslPolicyErrors) => true;

            var page = new MainPage();
            NavigationPage.SetHasNavigationBar(page, false);
            MainPage = new NavigationPage(page);
        }

        protected override void OnStart()
        {
            /*
            try
            {
                WebClient webClient = new WebClient();

                ConnectionItem currentConnection = ConnectionManager.CurrentConnection;
                Log.Info(Program.LOG_TAG, "Connection(" + currentConnection.Type + ", " + currentConnection.State + ")");
                LabelText = "Connection(" + currentConnection.Type + ", " + currentConnection.State + ")\n";

                if (currentConnection.Type == ConnectionType.Disconnected)
                {
                    Log.Info(Program.LOG_TAG, "There's no available data connectivity!!");
                    DownloadInfo = "There's no available data connectivity for downloading.";
                    return;
                }
                else if (currentConnection.Type == ConnectionType.Ethernet)
                {
                    // For Tizen Emulator, it is not necessary to set up web proxy.
                    // It's for Samsung Galaxy Watch which is paired with the mobile phone.
                    if (string.Compare(Tizen_Emulator, ModelName) != 0)
                    {
                        // Use web proxy
                        string proxyAddr = ConnectionManager.GetProxy(AddressFamily.IPv4);
                        WebProxy myproxy = new WebProxy(proxyAddr, true);
                        // Set proxy information to be used by WebClient
                        webClient.Proxy = myproxy;
                    }
                }

                webClient.DownloadFileCompleted += WebClient_DownloadFileCompleted;
                webClient.DownloadProgressChanged += WebClient_DownloadProgressChanged;

                string pathToNewFile = Path.Combine(DownloadsFolder, Path.GetFileName("https://archive.org/download/BigBuckBunny_328/BigBuckBunny_512kb.mp4"));
                // Download a file asynchronously
                webClient.DownloadFileAsync(new Uri("https://archive.org/download/BigBuckBunny_328/BigBuckBunny_512kb.mp4"), pathToNewFile);
            }
            catch (Exception ex)
            {
                Log.Error(Program.LOG_TAG, "[DownloadContent] Error: " + ex.Message);
            }
        }
    */
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

using Xamarin.Forms;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Xaml;

using Tizen;
using Tizen.Network.Connection;
using Tizen.System;

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
            // Use web proxy
            string proxyAddr = ConnectionManager.GetProxy(AddressFamily.IPv4);
            WebProxy myproxy = new WebProxy(proxyAddr, true);
            WebClient client = new WebClient();
            client.Proxy = myproxy;
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

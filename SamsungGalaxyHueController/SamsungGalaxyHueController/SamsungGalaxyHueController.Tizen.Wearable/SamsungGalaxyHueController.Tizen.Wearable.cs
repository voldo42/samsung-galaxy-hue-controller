using System;

namespace SamsungGalaxyHueController
{
    class Program : global::Xamarin.Forms.Platform.Tizen.FormsApplication
    {
        protected override void OnCreate()
        {
            base.OnCreate();

            LoadApplication(new App());
        }

        static void Main(string[] args)
        {
            var app = new Program();
            global::Xamarin.Forms.Forms.Init(app);
            app.Run(args);
        }
    }
}

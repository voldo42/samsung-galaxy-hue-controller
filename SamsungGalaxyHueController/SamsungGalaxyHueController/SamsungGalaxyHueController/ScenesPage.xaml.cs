using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SamsungGalaxyHueController
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ScenesPage : CirclePage
    {
        public ScenesPage(Group group)
        {
            this.BindingContext = HueHelper.GetScenes(group.id);

            //var menuListView = new CircleListView()
            //{
            //    ItemsSource = 
            //};

            //this.Content = menuListView;
            //menuListView.ItemTapped += OnItemTapped;
        }

        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {

        }
    }
}

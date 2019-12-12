
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Tizen.Wearable.CircularUI.Forms;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SamsungGalaxyHueController
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainPage : CarouselPage
    {
        public ObservableCollection<Group> Pages { get; set; }

        public MainPage()
        {
            InitializeComponent();

            var groups = HueHelper.GetGroups();

            foreach (var group in groups)
            {
                try
                {
                    var groupLabel = new Label
                    {
                        Text = group.name,
                        VerticalOptions = LayoutOptions.CenterAndExpand,
                        HorizontalOptions = LayoutOptions.CenterAndExpand
                    };

                    var lightSwitch = new Switch
                    {
                        IsToggled = group.state.any_on
                    };
                    lightSwitch.Toggled += OnToggled;
                                    
                    Children.Add(new CirclePage
                    {
                        Content = new StackLayout
                        {
                            VerticalOptions = LayoutOptions.StartAndExpand,
                            Padding = 50,
                            Children =
                            {
                                groupLabel,
                                new Label(),
                                lightSwitch
                            }
                        }
                    });

                }
                catch (Exception e)
                {
                    Alert("Oh oh!", e.Message, "OK");
                }
            }
        }

        async void Alert(string title, string message, string button)
        {
            await DisplayAlert(title, message, message);
        }

        async void OnToggled(object sender, ToggledEventArgs e)
        {
            try
            {
                await DisplayAlert("Woohoo", $"Toggle! {e.Value} {sender}", "OK");
            }
            catch
            {
                await DisplayAlert("Error", $"Toggle failed :(", "OK");
            }
        }

        async void OnItemTapped(object sender, ItemTappedEventArgs e)
        {
            try
            {
                var page = new ScenesPage((Group)e.Item);

                NavigationPage.SetHasNavigationBar(page, false);
                await Navigation.PushAsync(page);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Failed to load page. {ex.Message}", "OK");
            }
        }
    }
}
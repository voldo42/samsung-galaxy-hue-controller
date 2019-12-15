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

            List<Group> groups = null;
            try
            {
                groups = HueHelper.GetGroups();
            }
            catch (Exception ex)
            {
                Alert("Error", $"Failed to load groups. {ex.Message}", "OK");
            }

            CreateAllPage();
            CreateGroupsPages(groups);
            SelectedItem = Children[1];
        }

        private void CreateGroupsPages(List<Group> groups)
        {
            foreach (var group in groups.OrderBy(g => g.type).ThenBy(g => g.name))
            {
                var groupLabel = new Label
                {
                    Text = group.name,
                    FontSize = 12,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };

                var groupTypeLabel = new Label
                {
                    Text = $"{group.type}",
                    FontSize = 8,
                    VerticalOptions = LayoutOptions.CenterAndExpand,
                    HorizontalOptions = LayoutOptions.CenterAndExpand
                };

                var lightSwitch = new Switch
                {
                    IsToggled = group.state.any_on,
                    BindingContext = group
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
                                groupTypeLabel,
                                new Label(),
                                lightSwitch
                            }
                    }
                });
            }
        }

        private void CreateAllPage()
        {
            var allLabel = new Label
            {
                Text = "All",
                FontSize = 12,
                VerticalOptions = LayoutOptions.CenterAndExpand,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            };

            var allButton = new Button
            {
                Text = "Off"
            };
            allButton.Clicked += AllButton_Clicked;

            Children.Add(new CirclePage
            {
                Content = new StackLayout
                {
                    VerticalOptions = LayoutOptions.StartAndExpand,
                    Padding = 50,
                    Children =
                        {
                            allLabel,
                            new Label(),
                            allButton
                        }
                }
            });
        }

        private void AllButton_Clicked(object sender, EventArgs e)
        {
            HueHelper.KillAll();
        }

        async void Alert(string title, string message, string cancel)
        {
            await DisplayAlert(title, message, cancel);
        }

        async void OnToggled(object sender, ToggledEventArgs e)
        {
            try
            {
                var selectedGroup = (Switch)sender;
                var group = (Group)selectedGroup.BindingContext;

                HueHelper.SetGroupAction(group.id, e.Value);
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", $"Toggle failed. {ex.Message}", "OK");
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
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace SamsungGalaxyHueController
{
    public interface ITogglableItem
    {
        string GroupName { get; }
        bool IsChecked { get; set; }
    }

    public class GroupedSwitch : Switch
    {
        public static readonly BindableProperty IsGroupingEnabledProperty =
            BindableProperty.Create(
                "IsGroupingEnabled", typeof(bool), typeof(GroupedSwitch),
                defaultValue: default(bool));

        public bool IsGroupingEnabled
        {
            get { return (bool)GetValue(IsGroupingEnabledProperty); }
            set { SetValue(IsGroupingEnabledProperty, value); }
        }

        public static readonly BindableProperty GroupContextProperty =
            BindableProperty.Create(
                        "GroupContext", typeof(IEnumerable), typeof(GroupedSwitch),
                        defaultValue: default(IEnumerable));

        public IEnumerable GroupContext
        {
            get { return (IEnumerable)GetValue(GroupContextProperty); }
            set { SetValue(GroupContextProperty, value); }
        }

        protected override void OnPropertyChanged(string propertyName = null)
        {
            base.OnPropertyChanged(propertyName);

            if (propertyName != nameof(IsToggled))
                return;

            if (IsToggled != true || GroupContext == null)
                return;

            var currentItem = BindingContext as ITogglableItem;
            if (currentItem == null)
                return;

            if (IsGroupingEnabled)
            {
                var groupList = GroupContext as IEnumerable<IGrouping<string, ITogglableItem>>;
                var currentGroup = groupList.FirstOrDefault(x => x.Key == currentItem.GroupName);
                if (currentGroup != null)
                    foreach (var item in currentGroup)
                    {
                        if (item != currentItem)
                            item.IsChecked = false;
                    }
            }
            else
            {
                var simpleList = GroupContext as IEnumerable<ITogglableItem>;
                if (simpleList != null)
                    foreach (var item in simpleList)
                    {
                        if (item != currentItem)
                            item.IsChecked = false;
                    }

            }
        }
    }
}

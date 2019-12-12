using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace SamsungGalaxyHueController.Models
{
    class GroupList : ObservableCollection<Group>
    {
        public GroupList() : base()
        {
            Add(new Group(1, "Lauren"));
            Add(new Group(2, "Jim"));
        }
    }
}

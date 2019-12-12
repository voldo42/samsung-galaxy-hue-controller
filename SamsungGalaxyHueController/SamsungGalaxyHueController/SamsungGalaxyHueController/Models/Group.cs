using System;
using System.Collections.Generic;
using System.Text;

namespace SamsungGalaxyHueController
{
    public class Group
    {
        public Group() { }
        public Group(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public int id { get; set; }
        public string name { get; set; }
        public string[] lights { get; set; }
        public string type { get; set; }
        public GroupState state { get; set; }
        public bool recycle { get; set; }
        public string _class { get; set; }
        public Action action { get; set; }
        public List<Scene> scenes { get; set; }

        public override string ToString()
        {
            return name;
        }
    }

    public class GroupState
    {
        public bool all_on { get; set; }
        public bool any_on { get; set; }
    }

    public class Action
    {
        public bool on { get; set; }
        public int bri { get; set; }
        public int hue { get; set; }
        public int sat { get; set; }
        public string effect { get; set; }
        public float[] xy { get; set; }
        public int ct { get; set; }
        public string alert { get; set; }
        public string colormode { get; set; }
    }

}

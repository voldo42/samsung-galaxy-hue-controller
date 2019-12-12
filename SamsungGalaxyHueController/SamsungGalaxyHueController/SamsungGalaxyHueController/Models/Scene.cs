using System;
using System.Collections.Generic;
using System.Text;

namespace SamsungGalaxyHueController
{
    public class Scene
    {
        public int id { get; set; }
        public string name { get; set; }
        public string type { get; set; }
        public int group { get; set; }
        public string[] lights { get; set; }
        public string owner { get; set; }
        public bool recycle { get; set; }
        public bool locked { get; set; }
        public string picture { get; set; }
        public DateTime lastupdated { get; set; }
        public int version { get; set; }

        public override string ToString()
        {
            return name;
        }
    }
}

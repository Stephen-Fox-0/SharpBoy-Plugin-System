using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoyPluginSystem
{
    public class PluginBlankSetting : IPluginSetting
    {
        string name;

        public string Name => name;

        public PluginBlankSetting(string name) { this.name = name; }
    }
}

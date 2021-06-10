using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoyPluginSystem
{
    public class PluginCheckSetting : ExtraCategoryItem, IPluginSetting
    {
        string name;
        bool _value;

        public string Name => name;

        public bool Value
        {
            get => _value;
            set => _value = value;
        }

        public PluginCheckSetting( string name, bool defaultValue )
        {
            this.name = name;
            this._value = defaultValue;
        }
    }
}

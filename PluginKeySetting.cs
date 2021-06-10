using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoyPluginSystem
{
    public class PluginKeySetting : ExtraCategoryItem, IPluginSetting
    {
        string name;

        Keys _key;

        public Keys Keys
        {
            get => _key;
            set => _key = value;
        }

        public string Name => name;

        public PluginKeySetting( string name, Keys defaultValue)
        {
            this.name = name;
            this._key = defaultValue;
        }
    }
}

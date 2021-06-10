using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoyPluginSystem
{
    public class PluginListSetting : ExtraCategoryItem, IPluginSetting
    {
        string name;

        /// <summary>
        /// Gets the name of this key.
        /// </summary>
        public string Name => name;

        /// <summary>
        /// Gets the items.
        /// </summary>
        public List<string> Items { get { return _items; } }

        /// <summary>
        /// Gets or sets the selected item.
        /// </summary>
        public string SelectedItem;

        private List<string> _items;

        public PluginListSetting( string name )
        {
            _items = new List<string>();
            this.name = name;
        }
    }
}

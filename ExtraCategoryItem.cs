using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoyPluginSystem
{
    public class ExtraCategoryItem :IPluginSetting
    {
        /// <summary>
        /// Gets the name of this category.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets the description.
        /// </summary>
        public string Description { get; set; }


        public event EventHandler Clicked;


        public virtual void OnClicked()
        {
            Clicked?.Invoke( this, EventArgs.Empty );
        }
    }
}

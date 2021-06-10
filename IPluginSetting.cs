using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoyPluginSystem
{
    public interface IPluginSetting
    {
        /// <summary>
        /// Gets the name of this setting.
        /// </summary>
        string Name { get; }
    }
}

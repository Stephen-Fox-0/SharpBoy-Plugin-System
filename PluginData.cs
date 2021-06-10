using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoyPluginSystem
{
    public sealed class PluginData
    {
        IPlugin _plugin;
        string _path;

        /// <summary>
        /// Gets the plugin.
        /// </summary>
        public IPlugin Plugin => _plugin;

        /// <summary>
        /// Gets the plugin path.
        /// </summary>
        public string Path => _path;

        /// <summary>
        /// Initialize a new instance of <see cref="PluginData"/>
        /// </summary>
        /// <param name="path">The path to witch the plugin is located.</param>
        /// <param name="plugin">The plugin instance.</param>
        public PluginData(string path, IPlugin plugin)
        {
            _path = path;
            _plugin = plugin;
        }
    }
}


using SharpBoyEngine.Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoyPluginSystem
{
    public interface IPluginHost
    {

        /// <summary>
        /// Gets the accepted file types. this is used, when we load a rom from the file system.
        /// </summary>
        ///<remarks>Seperate the filetypes like .gb|.nes</remarks>
        string AcceptedFileTypes { get; }

        /// <summary>
        /// Loads this plugin.
        /// </summary>
        /// <param name="screen">The screen this host is child to.</param>
        void Load( Scene screen );

        /// <summary>
        /// Unloads this plugin.
        /// </summary>
        /// <param name="screen">The screen this host is child to.</param>
        void Unload( Scene screen );

        /// <summary>
        /// Called when a request to this plugins debug info.
        /// </summary>
        /// <returns></returns>
        string OnRequestDebugInfo();
    }
}

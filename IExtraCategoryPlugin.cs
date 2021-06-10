
using SharpBoyEngine.Screen;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoyPluginSystem
{
    public interface IExtraCategoryPlugin
    {
        Scene ReturnScreen { get; set; }

        /// <summary>
        /// Gets the name of this category.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The items of our category.
        /// </summary>
        List<ExtraCategoryItem> Items { get; }
    }
}

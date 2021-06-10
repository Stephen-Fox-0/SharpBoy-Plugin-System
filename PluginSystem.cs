
using Microsoft.Xna.Framework;
using SharpBoyEngine.Screen.Services;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoyPluginSystem
{
    public sealed class PluginSystem :IService
    {
        public IExtraCategoryPlugin[] CategoryPlugins { get; internal set; }

        /// <summary>
        /// Gets the plugins that have been loaded.
        /// </summary>
        public PluginData[]  Plugins { get; internal set; }

        public string Name => "PluginSystem Service";

        /// <summary>
        /// Load all the plugins.
        /// </summary>
        /// <param name="directory">The directory of the plugins.</param>
        public void Load(string directory)
        {
            var Plugins = new List<PluginData>();

            //Load the DLLs from the Plugins directory
            if (Directory.Exists( directory ))
            {
                string[] files = Directory.GetFiles( directory );
                List<string> dlls = new List<string>();
                foreach (string file in files)
                {
                    if (file.EndsWith( ".dll" ))
                    {
                        Assembly.LoadFrom( file );
                        dlls.Add( file );
                    }
                }

                Type interfaceType = typeof( IPlugin );

                //Fetch all types that implement the interface IPlugin and are a class
                Type[] types = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany( a => a.GetTypes() )
                    .Where( p => interfaceType.IsAssignableFrom( p ) && p.IsClass )
                    .ToArray();

                var lastViewDuplicate = "";
                for(int i = 0; i < types.Length;i++)
                {
                    var type = types[i];

                    //Create a new instance of all found types
                    var plugin = new PluginData( dlls[i], (IPlugin)Activator.CreateInstance( type ) );

                    if (plugin.Plugin.Name != lastViewDuplicate)
                    {
                        Plugins.Add( plugin );
                        lastViewDuplicate = plugin.Plugin.Name;
                    }
                    else
                    {
                        Plugins.Remove( plugin );
                    }
                }
            }
           
            this.Plugins = Plugins.ToArray();
            LoadCategory( directory );
        }


        public string GetPluginTypePath<T>() where T : IPlugin
        {
            string path = "";
            foreach (var i in Plugins)
                if (i is T)
                {
                    path = i.Path;
                }
            return path;
        }

        void LoadCategory( string directory )
        {
            var g = new List<IExtraCategoryPlugin>();

            //Load the DLLs from the Plugins directory
            if (Directory.Exists( directory ))
            {
                string[] files = Directory.GetFiles( directory );
                List<string> dlls = new List<string>();
                foreach (string file in files)
                {
                    if (file.EndsWith( ".dll" ))
                    {
                        Assembly.LoadFrom( file );
                        dlls.Add( file );
                    }
                }

                Type interfaceType = typeof( IExtraCategoryPlugin );

                //Fetch all types that implement the interface IPlugin and are a class
                Type[] types = AppDomain.CurrentDomain.GetAssemblies()
                    .SelectMany( a => a.GetTypes() )
                    .Where( p => interfaceType.IsAssignableFrom( p ) && p.IsClass )
                    .ToArray();

                var lastViewDuplicate = "";
                for (int i = 0; i < types.Length; i++)
                {
                    var type = types[i];

                    //Create a new instance of all found types
                    var plugin = (IExtraCategoryPlugin)Activator.CreateInstance( type );


                    g.Add( plugin );
                    lastViewDuplicate = plugin.Name;
                }
            }

            this.CategoryPlugins = g.ToArray();
        }

        public void Update(GameTime gameTime)
        {
        }
    }
}

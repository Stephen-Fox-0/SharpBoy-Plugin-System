using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoyPluginSystem
{
    public class PluginErrorEventArgs
    {
        string message;
        IPlugin sender;

        public string Message => message;

        public IPlugin Sender
        {
            get => sender;
        }

        public PluginErrorEventArgs(string message, IPlugin sender)
        {
            this.message = message;
            this.sender = sender;
        }
    }

    public delegate void PluginErrorEventHandler(PluginErrorEventArgs e);
}

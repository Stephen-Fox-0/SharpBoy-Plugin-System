using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using SharpBoyEngine.Screen.Graphics;
using SharpBoyEngine.Screen.Input;
using SharpBoyEngine.Screen.System;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SharpBoyPluginSystem
{
    public interface IPlugin
    {
        /// <summary>
        /// Call this event when there will be a error.
        /// </summary>
        event PluginErrorEventHandler Error;

        /// <summary>
        /// Gets weather this plugin is only a popup screen, (default: false);
        /// </summary>
        bool Popup { get; }

        /// <summary>
        /// Gets or sets weather this plugin is on the main menu.
        /// </summary>
        bool IsMainMenu { get; }

        /// <summary>
        /// Gets the settings.
        /// </summary>
        List<IPluginSetting> Settings { get; }

        /// <summary>
        /// Gets the name of this plugin.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Gets the description of this plugin.
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Gets the host of this plugin.
        /// </summary>
        IPluginHost Host { get; }

        /// <summary>
        /// Initialize this plugin.
        /// </summary>
        /// <param name="fileName">The filename that is used to give the plugin the name of the file to use.</param>
        void Initialize( string fileName, bool audioEnabled, float audioVolume, string savePath );

        /// <summary>
        /// Loads all of this <see cref="IPlugin"/>s content.
        /// </summary>
        /// <param name="system">The active content manager.</param>
        void LoadContent( SceneSystem system ); 

        /// <summary>
        /// Draws the plugins content on screen.
        /// </summary>
        /// <param name="spriteBatch">The spriteBatch.</param>
        /// <param name="gameTime">The gametime.</param>
        void Draw( SpriteBatch spriteBatch, GameTime gameTime, SceneBounds drawBounds );

        /// <summary>
        /// Draws the plugin contents on screen, in a fixed time. (witch gets updated every 1000 miliseconds).
        /// </summary>
        /// <param name="spriteBatch"></param>
        /// <param name="gameTime"></param>
        /// <param name="drawBounds"></param>
        void FixedDraw(SpriteBatch spriteBatch, GameTime gameTime, SceneBounds drawBounds);


        /// <summary>
        /// Update the screen 
        /// </summary>
        /// <param name="gameTime">The gametime.</param>
        /// <param name="input">The input.</param>
        void Update( GameTime gameTime );

        /// <summary>
        /// Updates the screen in a fixed time. (witch gets updated every 1000 miliseconds).
        /// </summary>
        /// <param name="gameTime"></param>
        /// <param name="input"></param>
        void FixedUpdate(GameTime gameTime);

        /// <summary>
        /// Handles all emulation controls.
        /// </summary>
        /// <param name="input"></param>
        void HandleInput(InputSystem input);

        /// <summary>
        /// The method is called when exiting.
        /// </summary>
        void Exit();

        /// <summary>
        /// Loads the settings into our plugin.
        /// </summary>
        /// <param name="settingStream"></param>
        void LoadSettings( PluginSettingStream settingStream );

        /// <summary>
        /// Save our settings.
        /// </summary>
        /// <param name="settingStream"></param>
        void SaveSettings( PluginSettingStream settingStream );

        /// <summary>
        /// Gets a value indercating weather this plugin has been paused or unpaused.
        /// </summary>
        /// <param name="isPaused">Gets the value weather this plugins is paused.</param>
        void Paused( bool isPaused );

        /// <summary>
        /// Set a cheat into memory.
        /// </summary>
        /// <param name="address">The address.</param>
        /// <param name="value">The value.</param>
        void SetCheat( byte address, byte value );

        /// <summary>
        /// This method is called when a screenshot was requested.
        /// </summary>
        /// <param name="texture"></param>
        void ScreenShotRequested(out Texture2D texture);
    }
}

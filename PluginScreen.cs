

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using SharpBoyEngine.Screen;
using SharpBoyEngine.Screen.Components.Notification;
using SharpBoyEngine.Screen.Input;
using SharpBoyEngine.Screen.System;
using System;
using System.IO;

namespace SharpBoyPluginSystem
{
    public class PluginScreen : Scene
    {
        public bool ShowFps { get; set; }

        Scene returnScene;
        public static PluginScreen Instance;
        bool _paused = false;
        IPlugin plugin;
        string fileName;
        SharpBoyEngine.Screen.Components.FPS.FpsComponent fps;

        int fixedUpdateTime = 0;
        int fixedUpdateTimer = 1000;

        int fixedDrawTime = 0;
        int fixedDrawTimer = 1000;

        public Keys ScreenshotKey { get; set; }
        public string ScreenshotSaveDirectory { get; set; }

        string savePath;
        public string SavePath
        {
            get => savePath;
            set => savePath = value;
        }

        public Scene ReturnScene
        {
            get => returnScene;
            set
            {
                returnScene = value;
            }
        }

        public delegate void PluginScreenEventHandler<T,T2>( T2 sender, T e );

        /// <summary>
        /// Gets or sets the pause key.
        /// </summary>
        public Keys PauseKey
        {
            get;
            set;
        }

        /// <summary>
        /// Occurs when this screen has been paused.
        /// </summary>

        public event PluginScreenEventHandler<bool, IPlugin> PauseChanged;

        /// <summary>
        /// Gets or set weather this screen is paused.
        /// </summary>
        public new bool Paused
        {
            get { return _paused; }
            set
            {
                _paused = value;
                PauseChanged?.Invoke( plugin, value );

                plugin.Paused( value );
            }
        }

        /// <summary>
        /// Gets the file name.
        /// </summary>
        public string FileName => fileName;

        public bool AudioEnabled { get;  }
        public float AudioVolume { get; }

        /// <summary>
        /// Gets the plugin.
        /// </summary>
        public IPlugin Plugin => plugin;

        /// <summary>
        /// Initialize a new instance of <see cref="PluginScreen"/>
        /// </summary>
        /// <param name="name">The name of this screen.</param>
        /// <param name="system">The screen system of this screen.</param>
        public PluginScreen( string fileName, IPlugin plugin, SceneSystem system, bool audioEnabled, float audioVolme )
        {
            this.fileName = fileName;
            this.plugin = plugin;
            this.AudioEnabled = audioEnabled;
            this.AudioVolume = audioVolme;
            Instance = this;
            plugin.Error += Plugin_Error;
        }

        private void Plugin_Error(PluginErrorEventArgs e)
        {
            var text = $"Error Occured '{e.Message}'";
            ScreenManager.GetComponent<NotificationSystem>().Show(text, Icon.Error);
            ScreenManager.AddScreen(returnScene);
            ScreenManager.RemoveScreen(this);
            plugin.Exit();
            plugin.Host.Unload(this);
        }

        public override void Activate(bool instancePreserved)
        {
            base.Activate(instancePreserved);

            plugin.Host.Load(this);

            plugin.LoadContent(ScreenManager);
            plugin.Initialize(fileName, AudioEnabled, AudioVolume, savePath);

            fps = new SharpBoyEngine.Screen.Components.FPS.FpsComponent(ScreenManager.Game, SharpBoyEngine.Screen.Components.FPS.FpsComponentView.Simplified);
        }

        public override void HandleInput(GameTime gameTime, InputSystem input)
        {
            base.HandleInput(gameTime, input);

            if (!Paused)
            {
                plugin.HandleInput(input);
            }

            if (input.ContainsType<KeyboardController>())
            {
                if (input.GetController<KeyboardController>().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape, true))
                    ExitScreen();

                if (input.GetController<KeyboardController>().IsKeyDown(PauseKey, true))
                    Paused = !Paused;

                if(input.GetController<KeyboardController>().IsKeyDown(ScreenshotKey, true))
                {
                    int count = Directory.GetFiles(ScreenshotSaveDirectory).Length;
                    count += 1;
                    var path = Path.Combine(ScreenshotSaveDirectory, $"screenshot{count}.png");
                    Texture2D texture;
                    plugin.ScreenShotRequested(out texture);

                    if(texture != null)
                    {
                        texture.SaveAsPng(new FileStream(path, FileMode.Create, FileAccess.Write), texture.Width, texture.Height);
                        ScreenManager.GetComponent<NotificationSystem>().Show("Screenshot saved " + path);
                    }
                }
            }
        }


        public override void Update(GameTime gameTime, bool otherScreenHasFocus, bool coveredByOtherScreen)
        {
            base.Update(gameTime, otherScreenHasFocus, coveredByOtherScreen);

            if(!Paused)
            {
                plugin.Update(gameTime);
            }

            fixedUpdateTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if (fixedUpdateTime >= fixedUpdateTimer)
            {
                plugin.FixedUpdate(gameTime);
                fixedUpdateTime = 0;
            }
        }
        public override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);

            plugin.Draw(ScreenManager.SpriteBatch, gameTime, ScreenManager.GraphicsDeviceManager.Bounds);

            fixedDrawTime += (int)gameTime.ElapsedGameTime.TotalMilliseconds;

            if(fixedDrawTime>= fixedDrawTimer)
            {
                plugin.FixedDraw(ScreenManager.SpriteBatch, gameTime, ScreenManager.GraphicsDeviceManager.Bounds);
                fixedDrawTime = 0;
            }

            if (ShowFps)
                fps.Draw(ScreenManager.SpriteBatch, gameTime);
        }


        public override void ExitScreen()
        {
            base.ExitScreen();


            plugin.Host.Unload( this );
            plugin.Exit();

            ScreenManager.AddScreen( returnScene );
            ScreenManager.RemoveScreen( this );
        }
    }
}

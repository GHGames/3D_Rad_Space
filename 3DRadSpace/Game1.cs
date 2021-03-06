﻿using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Content;
using System.Windows.Forms;
using System.Net;
using System.IO;
using System.Diagnostics;
using System;

namespace _3DRadSpace
{
    public class Game1 : Game
    {
        static Vector3 CameraPos = new Vector3(10, 1, 3);
        Matrix world = Matrix.CreateTranslation(new Vector3(0, 0, 0));
        Matrix view = Matrix.CreateLookAt(CameraPos, new Vector3(0, 0, 0), Vector3.UnitY);
        Matrix projection = Matrix.CreatePerspectiveFieldOfView(MathHelper.ToRadians(45), 800f / 480f, 0.1f, 500f); //Last parameter is maximum view distance
        GraphicsDeviceManager graphics;
        Texture2D GUI1;
        SpriteBatch spriteBatch;
        Model Axis;
        SpriteFont Font;
        Model[] GameObjects = new Model[300];
        string[] ObjectsData = new string[300];
        Texture2D CurrentEdit; //ignore the warning, will be used later.
        Color skycolor = new Color(100, 100, 255); 
        static public bool Focus = true;
        public static NotifyIcon notifyIcon = new NotifyIcon()
        {
            Icon = System.Drawing.Icon.ExtractAssociatedIcon("Icon.ico"),
            Visible = true,
            Text = "3DRadSpace",
            Tag = "3DRadSpace",
        };
        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }
        protected override void Initialize()
        {
            UpdateV(false);
            // TODO: Add your initialization logic here
            base.IsMouseVisible = true;
            base.Window.Position = new Point(0, 0);
            graphics.PreferredBackBufferHeight = Screen.PrimaryScreen.Bounds.Height - 60;
            graphics.PreferredBackBufferWidth = Screen.PrimaryScreen.Bounds.Width - 12;
            graphics.ApplyChanges();
            Window.Title = "3DRadSpace v1.0 Pre-Alpha release -Editor-";
            notifyIcon.BalloonTipClicked += NotifyIcon_BalloonTipClicked;
            base.Initialize();
            Form GameForm = Form.FromHandle(Window.Handle) as Form; //intptr xd
            GameForm.FormClosing += OnGameEditorClosing;
            for(int i = 0;i<300;i++)
            {
                ObjectsData[i] = null;
            }
        }
        private void OnGameEditorClosing(object sender, System.EventArgs e)
        {
            string[] data = File.ReadAllLines(@"settings.data");
            if (data[1] == "True")
            {
                DialogResult warn1 = MessageBox.Show("Project is not saved. Save the project?", "Warning", MessageBoxButtons.YesNo, MessageBoxIcon.Warning);
                if(warn1 == DialogResult.Yes)
                {
                    SaveFileDialog saveFile = new SaveFileDialog()
                    {
                        Filter = "3DRadSpace Project | *.3drsp | Text File | *.txt",
                        InitialDirectory = @"/Projects/",
                        Title = "Save a 3DRadSpace project...",
                        OverwritePrompt = true
                    };
                    saveFile.ShowDialog();
                }
            }
        }
        private void NotifyIcon_BalloonTipClicked(object sender, System.EventArgs e)
        {
           ///Things to do later here.
        }
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Axis = Content.Load<Model>("Axis");
            GUI1 = Content.Load<Texture2D>("Button");
            Font = Content.Load<SpriteFont>("Font");
            // TODO: use this.Content to load your game content here
        }
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }
        protected override void Update(GameTime gameTime)
        {

            if (GamePad.GetState(PlayerIndex.One).Buttons.Back == Microsoft.Xna.Framework.Input.ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
                Exit();
            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            if (keyboard.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Up)) CameraPos.X += 1;
            if (keyboard.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Down)) CameraPos.X -= 1;
            if (keyboard.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Left)) CameraPos.Z -= 1;
            if (keyboard.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Right)) CameraPos.Z += 1;
            if (keyboard.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.E)) CameraPos.Y += 1;
            if (keyboard.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Q)) CameraPos.Y -= 1;
            if (_3DRadSpace_EditorClass.DotInSquare(new Vector2(mouse.X, mouse.Y), new Vector2(3, 2), new Vector2(30, 18)))
            {
                if (mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    if (Focus == true)
                    {
                        Focus = false;
                        FileMenuStrip strip1 = new FileMenuStrip();
                        strip1.ShowDialog();
                    }
                }
            }
            if (_3DRadSpace_EditorClass.DotInSquare(new Vector2(mouse.X, mouse.Y), new Vector2(40, 2), new Vector2(80, 18)))
            {
                if (mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    if (Focus == true)
                    {
                        Focus = false;
                        EditMenuStrip strip2 = new EditMenuStrip();
                        strip2.ShowDialog();
                        string obj = File.ReadAllText(@"lastobj.data");
                        if (obj.Split(' ')[0] != null)
                        {
                            for (int i = 0; i < 300; i++)
                            {
                                if(ObjectsData[i] == null)
                                {
                                    ObjectsData[i] = obj;
                                    break;
                                }
                            }
                        }
                    }
                }
            }
            if (_3DRadSpace_EditorClass.DotInSquare(new Vector2(mouse.X, mouse.Y), new Vector2(90, 2), new Vector2(140, 18)))
            {
                if (mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    if (Focus == true)
                    {
                        Focus = false;
                        OptionsMenuStrip strip3 = new OptionsMenuStrip();
                        strip3.ShowDialog();
                    }
                   
                }
            }
            if (_3DRadSpace_EditorClass.DotInSquare(new Vector2(mouse.X, mouse.Y), new Vector2(150, 2), new Vector2(170, 18)))
            {
                if (mouse.LeftButton == Microsoft.Xna.Framework.Input.ButtonState.Pressed)
                {
                    if (Focus == true)
                    {
                        Focus = false;
                        HelpMenuStrip strip4 = new HelpMenuStrip();
                        strip4.ShowDialog();
                    }
                }
            }
            
            base.Update(gameTime);
        }
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(skycolor);
            DrawModel(Axis, world,view, projection);
            view = Matrix.CreateLookAt(CameraPos, new Vector3(0, 0, 0), Vector3.UnitY);
            for(int i=0; i < 300;i++)
            {
                if (ObjectsData[i] != null)
                    CreateObject(ObjectsData[i].Split(' '));
            }
            spriteBatch.Begin(); //rlly
            spriteBatch.Draw(GUI1, new Rectangle(0, 0, Screen.PrimaryScreen.Bounds.Width, 25),Color.White);
            spriteBatch.DrawString(Font, "File",new Vector2(3, 2), Color.White);
            spriteBatch.DrawString(Font, "Edit", new Vector2(40, 2), Color.White);
            spriteBatch.DrawString(Font, "Options", new Vector2(80, 2), Color.White);
            spriteBatch.DrawString(Font, "Help", new Vector2(145, 2), Color.White);
            for(int i=0;i < 300;i++)
            {
                if(ObjectsData[i] != null)
                {
                    
                }
            }
            spriteBatch.End();
            spriteBatch.GraphicsDevice.RasterizerState = RasterizerState.CullCounterClockwise;
            spriteBatch.GraphicsDevice.BlendState = BlendState.Opaque;
            spriteBatch.GraphicsDevice.DepthStencilState = DepthStencilState.Default;
            base.Draw(gameTime);
        }
        /// <summary>
        /// Finds a update for 3DRadSpace
        /// </summary>
        public static void UpdateV(bool forced)
        {
            string[] Setting = File.ReadAllLines(@"settings.data");
            if (Setting[0] == "True" || forced == true)
            {
                WebClient vchecker = new WebClient();
                vchecker.DownloadFile("https://drive.google.com/uc?export=download&id=0B9yRO5eZEvTjeHhPa05OZDRxUmM", @"update.data");
                string Update = File.ReadAllText(@"update.data");
                string[] LastVersion = Update.Split(' ');
                if (LastVersion[0] != "1" || LastVersion[1] != "0" || LastVersion[2] != "0")
                {
                    DialogResult UpdateQ = MessageBox.Show("A update is avalable for 3DRadSpace. Version" + LastVersion[0] + " " + LastVersion[1] + " " + LastVersion[2] + " \r\n Do you want to download it?", "Information", MessageBoxButtons.YesNo, MessageBoxIcon.Information);
                    if (UpdateQ == DialogResult.Yes)
                    {
                        vchecker.DownloadFile("https://drive.google.com/uc?export=download&id=0B9yRO5eZEvTjSVhCZndjSGRUcVE", @"3DRadSpace_Installer.exe");
                        Process.Start(@"3DRadSpace_Installer.exe");
                        MessageBox.Show("To apply the update, manually restart the application.", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("You can update 3DRadSpace from the official website", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                }
                else
                {
                    MessageBox.Show("No new updates", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
        }
        void DrawModel(Model model, Matrix world, Matrix view, Matrix projection)
        {
            foreach (ModelMesh mesh in model.Meshes)
            {
                foreach (BasicEffect effect in mesh.Effects)
                {
                    effect.World = world;
                    effect.View = view;
                    effect.Projection = projection;
                }
                mesh.Draw();
            }
        }
       public void CreateObject(string[] ObjectData)
        {
            File.AppendAllText(@"log.txt", "Trying to create object " + ObjectData.ToString());
            for (int i = 0; i < 300; i++)
            {
                if (ObjectsData[i] == null)
                {
                    if (ObjectData[0] == "Camera")
                    {
                        Matrix objpos = Matrix.CreateTranslation(new Vector3(Convert.ToSingle(ObjectData[3]), Convert.ToSingle(ObjectData[4]), Convert.ToSingle(ObjectData[5])));
                        Matrix rotation = Matrix.CreateFromYawPitchRoll(Deg2Rad(Convert.ToSingle(ObjectData[6])), Deg2Rad(Convert.ToSingle(ObjectData[7])), Deg2Rad(Convert.ToSingle(ObjectData[8])));
                        objpos *= rotation;
                        GameObjects[i] = Content.Load<Model>("Camera");
                        DrawModel(GameObjects[i], objpos, view, projection);
                        File.AppendAllText(@"log.txt", "Loaded Camera Object ID \r\n" + i.ToString());
                        break;
                    }
                    if(ObjectData[0] == "SkyColor")
                    {
                        skycolor.R = Convert.ToByte(Convert.ToInt16(ObjectData[3]));
                        skycolor.G = Convert.ToByte(Convert.ToInt16(ObjectData[4]));
                        skycolor.B = Convert.ToByte(Convert.ToInt16(ObjectData[5]));
                        break;
                    }
                }
            }
        }
        float Deg2Rad(float rad)
        {
            return rad * 3.141592f / 180;
        }
    }
}

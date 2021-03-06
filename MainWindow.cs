using System;
using System.Resources;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Collections.Generic;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;
using System.Linq;

namespace Magus
{
    public class MainWindow
    {
        private GameWindow window = null;
        private OpenTK.RectangleF CurrentView = new OpenTK.RectangleF(0, 0, 800, 600);
        private double time = 0.0f;
        private int ibo_elements;
        private Dictionary<string, int> textures = new Dictionary<string, int>();
        private List<Sprite> sprites = new List<Sprite>();
        private Matrix4 ortho;
        private int currentShader = 0;
        private List<ShaderProgram> shaders = new List<ShaderProgram>();
        private bool updated = false;
        private float avgfps = 60;
        private Random r = new Random();
        private bool multishadermode = false;

        public MainWindow(GameWindow _window)           
        {
            this.window = _window;
            this.window.Title += ": OpenGL";

            this.window.UpdateFrame +=  OnUpdateFrame;
            this.window.KeyDown += OnkeyDown;
            this.window.Resize +=  OnResize;
            this.window.Load += OnLoad;
            this.window.RenderFrame += OnRenderFrame;        
            this.window.MouseUp +=OnMouseUp;
            this.window.KeyUp +=OnKeyUp;   
           
        }

        public void Run(double UpdateRate){
            this.window.Run(UpdateRate);
        }

        private void OnkeyDown(object sender, KeyboardKeyEventArgs e ){
             // Quit if requested
            if(e.Key == Key.Escape){
                this.window.Exit();
            }
            // Move view based on key input
            float moveSpeed = 50.0f * ((e.Key == Key.ShiftLeft || e.Key ==Key.ShiftRight) ? 3.0f : 1.0f); // Hold shift to move 3 times faster!

            // Up-down movement
            if (e.Key == Key.Up)
            {
                CurrentView.Y += moveSpeed * (float) time;
            }
            else if (e.Key == Key.Down)
            {
                CurrentView.Y -= moveSpeed * (float) time;
            }

            // Left-right movement
            if (e.Key == Key.Left)
            {
                CurrentView.X -= moveSpeed * (float) time;
            }
            else if (e.Key == Key.Right)
            {
                CurrentView.X += moveSpeed * (float) time;
            }
        }
    
        private void OnLoad(object sender,EventArgs e){

            GL.ClearColor(OpenTK.Graphics.Color4.CornflowerBlue);
            GL.Viewport(0, 0, this.window.Width, this.window.Height);  

            // Load textures from files
            textures.Add("opentksquare", loadImage(".\\Resources\\world.mgs0.png"));
            textures.Add("opentksquare2", loadImage(".\\Resources\\magus.art1.png"));
            textures.Add("opentksquare3", loadImage(".\\Resources\\magus.art2.png"));

             Sprite s = new Sprite(textures.ElementAt(0).Value, 1000, 1000);
            s.Position = new Vector2(0, 0);
            //s.Size =new OpenTK.SizeF(280, 280);
            s.Size =new OpenTK.SizeF(4800, 6720);
            s.Rotation = 0;

            sprites.Add(s);
      
            // Load shaders
            shaders.Add(new ShaderProgram(".\\Shaders\\sprite.vert", ".\\Shaders\\sprite.frag", true)); // Normal sprite
            shaders.Add(new ShaderProgram(".\\Shaders\\white.vert", ".\\Shaders\\white.frag", true)); // Just draws the whole sprite white
            shaders.Add(new ShaderProgram(".\\Shaders\\onecolor.vert", ".\\Shaders\\onecolor.frag", true)); // Uses the color in the upper-left corner of the sprite, but with the correct alpha
            GL.UseProgram(shaders[currentShader].ProgramID);

            GL.GenBuffers(1, out ibo_elements);

            // Enable blending based on the texture alpha
            GL.Enable(EnableCap.Blend);
            GL.BlendFunc(BlendingFactorSrc.SrcAlpha, BlendingFactorDest.OneMinusSrcAlpha);
        }     

        private  void OnRenderFrame(object seneder, FrameEventArgs e)
        {
            if (updated)
            {
                GL.Viewport(0, 0, this.window.Width, this.window.Height);
                GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

                int offset = 0;

                GL.UseProgram(shaders[currentShader].ProgramID);
                shaders[currentShader].EnableVertexAttribArrays();
                foreach (Sprite s in sprites)
                {
                    if (s.IsVisible(this.window.ClientSize.Width,this.window.ClientSize.Height,CurrentView.X,CurrentView.Y))
                    {
                        if (multishadermode)
                        {
                            GL.UseProgram(shaders[(s.TextureID - 1) % shaders.Count].ProgramID);
                        }

                        GL.BindTexture(TextureTarget.Texture2D, s.TextureID);

                        GL.UniformMatrix4(shaders[currentShader].GetUniform("mvp"), false, ref s.ModelViewProjectionMatrix);
                        GL.Uniform1(shaders[currentShader].GetAttribute("mytexture"), s.TextureID);
                        GL.DrawElements(BeginMode.Triangles, 6, DrawElementsType.UnsignedInt, offset * sizeof(uint));
                        offset += 6;
                    }
                }

                shaders[currentShader].DisableVertexAttribArrays();

                GL.Flush();
                this.window.SwapBuffers();
            }
        }

        private  void OnUpdateFrame(object sender,FrameEventArgs e)
        {
            time = e.Time;
            // Update positions
           /* Parallel.ForEach(sprites, delegate(Sprite s)
            {
                s.Position += new Vector2((float)(e.Time * s.Scale.X * Math.Cos(s.Rotation)), (float)(e.Time * s.Scale.Y * Math.Sin(s.Rotation)));
            });     */      

            // Update graphics
            List<Vector2> verts = new List<Vector2>();
            List<Vector2> texcoords = new List<Vector2>();
            List<int> inds = new List<int>();

            int vertcount = 0;
            int viscount = 0;

            // Get data for visible sprites
            foreach (Sprite s in sprites)
            {
                if (s.IsVisible(this.window.ClientSize.Width,this.window.ClientSize.Height,CurrentView.X,CurrentView.Y))
                {
                    verts.AddRange(Sprite.GetVertices());
                    texcoords.AddRange(s.GetTexCoords());
                    inds.AddRange(s.GetIndices(vertcount));
                    vertcount += 4;
                    viscount++;

                    s.CalculateModelMatrix(this.window.ClientSize.Width,this.window.ClientSize.Height,CurrentView.X,CurrentView.Y);
                    s.ModelViewProjectionMatrix = s.ModelMatrix * ortho;
                }
            }

            // Buffer vertex coordinates
            GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[currentShader].GetBuffer("v_coord"));
            GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr) (verts.Count * Vector2.SizeInBytes), verts.ToArray(), BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(shaders[currentShader].GetAttribute("v_coord"), 2, VertexAttribPointerType.Float, false, 0, 0);

            // Buffer texture coords
            GL.BindBuffer(BufferTarget.ArrayBuffer, shaders[currentShader].GetBuffer("v_texcoord"));
            GL.BufferData<Vector2>(BufferTarget.ArrayBuffer, (IntPtr) (texcoords.Count * Vector2.SizeInBytes), texcoords.ToArray(), BufferUsageHint.StaticDraw);
            GL.VertexAttribPointer(shaders[currentShader].GetAttribute("v_texcoord"), 2, VertexAttribPointerType.Float, true, 0, 0);
            
            GL.BindBuffer(BufferTarget.ArrayBuffer, 0);

            // Buffer indices
            GL.BindBuffer(BufferTarget.ElementArrayBuffer, ibo_elements);
            GL.BufferData(BufferTarget.ElementArrayBuffer, (IntPtr) (inds.Count * sizeof(int)), inds.ToArray(), BufferUsageHint.StaticDraw);

            updated = true;

            // Display average FPS and sprite statistics in title bar
            avgfps = (avgfps + (1.0f / (float) e.Time)) / 2.0f;
            this.window.Title = String.Format("Magus ({0} sprites, {1} drawn, FPS:{2:0.00})", sprites.Count, viscount, avgfps);
        }
        private  void OnMouseUp(object sender, MouseButtonEventArgs e)
        {
            // Selection example
            // First, find coordinates of mouse in global space
            Vector2 clickPoint = new Vector2(e.X, e.Y);
            clickPoint.X += CurrentView.X;
            clickPoint.Y = this.window.ClientSize.Height - clickPoint.Y + CurrentView.Y;

            // Find target Sprite
            Sprite clickedSprite = null;
            foreach (Sprite s in sprites)
            {
                // We can only click on visible Sprites
                if (s.IsVisible(this.window.ClientSize.Width,this.window.ClientSize.Height,CurrentView.X,CurrentView.Y))
                {
                    if (s.IsInside(clickPoint))
                    {
                        // We store the last sprite found to get the topmost one (they're searched in the same order they're drawn)
                        clickedSprite = s;
                    }
                }
            }

            // Change the texture on the clicked Sprite
            if (clickedSprite != null)
            {
                if (clickedSprite.TextureID == textures["opentksquare"])
                {
                    clickedSprite.TextureID = textures["opentksquare2"];
                }
                else if (clickedSprite.TextureID == textures["opentksquare2"])
                {
                    clickedSprite.TextureID = textures["opentksquare3"];
                }
                else
                {
                    clickedSprite.TextureID = textures["opentksquare"];
                }
            }
        }
        private void OnKeyUp(object sender,KeyboardKeyEventArgs e)
        {

            // Change shader
            if (e.Key == Key.V && !multishadermode)
            {
                currentShader = (currentShader + 1) % shaders.Count;
                GL.UseProgram(shaders[currentShader].ProgramID);
            }

            // Enable shader based on texture ID
            if (e.Key == Key.M)
            {
                // Toggle the value
                multishadermode ^= true;
            }
        }
        private  void OnResize(object sender,EventArgs e)
        {
            ortho = Matrix4.CreateOrthographic(this.window.ClientSize.Width, this.window.ClientSize.Height, -1.0f, 2.0f);
            CurrentView.Size = new OpenTK.SizeF(this.window.ClientSize.Width, this.window.ClientSize.Height);
        }


        #region "MEthods"

        /// <summary>
        /// Creates a new sprite with a random texture and transform
        /// </summary>
        private void addSprite()
        {
            // Assign random texture
            Sprite s = new Sprite(textures.ElementAt(r.Next(0, textures.Count)).Value, 50, 50);

            // Transform sprite randomly
            s.Position = new Vector2(r.Next(-8000, 8000), r.Next(-6000, 6000));
            float scale = 300.0f * (float) r.NextDouble() + 0.5f;
            s.Size = new OpenTK.SizeF(scale, scale);
            s.Rotation = (float) r.NextDouble() * 2.0f * 3.141f;

            sprites.Add(s);
        }

        /// <summary>
        /// Loads a texture from a Bitmap
        /// </summary>
        /// <param name="image">Bitmap to make a texture from</param>
        /// <returns>ID of texture, or -1 if there is an error</returns>
        private int loadImage(System.Drawing.Bitmap image)
        {
            int texID = GL.GenTexture();

            GL.BindTexture(TextureTarget.Texture2D, texID);
            System.Drawing.Imaging.BitmapData  data = image.LockBits(new System.Drawing.Rectangle(0, 0, image.Width, image.Height),
                System.Drawing.Imaging.ImageLockMode.ReadOnly, System.Drawing.Imaging.PixelFormat.Format32bppArgb);

            GL.TexImage2D(TextureTarget.Texture2D, 0, PixelInternalFormat.Rgba, data.Width, data.Height, 0, OpenTK.Graphics.OpenGL4.PixelFormat.Bgra, PixelType.UnsignedByte, data.Scan0);

            image.UnlockBits(data);

            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.LinearMipmapLinear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);

            return texID;
        }

        /// <summary>
        /// Overload to make a texture from a filename
        /// </summary>
        /// <param name="filename">File to make a texture from</param>
        /// <returns>ID of texture, or -1 if there is an error</returns>
        private int loadImage(string filename)
        {
            try
            {
                System.Drawing.Image file =  System.Drawing.Image.FromFile(filename);
                return loadImage(new System.Drawing.Bitmap(file));
            }
            catch (FileNotFoundException e)
            {
                return -1;
            }
        }


        #endregion
        
    }
}
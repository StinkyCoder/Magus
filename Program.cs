using System;
using OpenTK;
using OpenTK.Graphics.OpenGL4;
using OpenTK.Input;

namespace Magus
{
    class Program
    {
        static void Main(string[] args)
        {
            
#if DEBUG
            Console.WriteLine("Start Magus!");
#endif

            MainWindow mainWindow = new MainWindow(new GameWindow(1280, // initial width
                                                                720, // initial height
                                                                OpenTK.Graphics.GraphicsMode.Default,
                                                                "Magus",  // initial title
                                                                GameWindowFlags.Default,
                                                                DisplayDevice.Default,
                                                                4, // OpenGL major version
                                                                0, // OpenGL minor version
                                                                OpenTK.Graphics.GraphicsContextFlags.ForwardCompatible));
            mainWindow.Run(30.0);
            
#if DEBUG
            Console.WriteLine( "Exiting main loop...");
#endif
        }
    }
}

using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;

namespace OpenTK_console_sample01
{
    class SimpleWindow : GameWindow
    {
        private float rotationAngle = 0.0f;

        public SimpleWindow() : base(800, 600)
        {
            KeyDown += Keyboard_KeyDown;
            Console.WriteLine("W-move up");
            Console.WriteLine("S-move down");
            Console.WriteLine("A-move left");
            Console.WriteLine("D-move right");
            Console.WriteLine("Left click-rotate counter clockwise");
            Console.WriteLine("Right click-rotate clockwise");
            Console.WriteLine("Scroll up-zoom in");
            Console.WriteLine("Scroll down-zoom out");
        }

        void Keyboard_KeyDown(object sender, KeyboardKeyEventArgs e)
        {
            if (e.Key == Key.Escape)
                this.Exit();

            if (e.Key == Key.F11)
                if (this.WindowState == WindowState.Fullscreen)
                    this.WindowState = WindowState.Normal;
                else
                    this.WindowState = WindowState.Fullscreen;

            if (e.Key == Key.W)
                GL.Translate(0.0, 0.1, 0);

            if (e.Key == Key.S)
                GL.Translate(0.0, -0.1, 0);

            if (e.Key == Key.D)
                GL.Translate(0.1, 0, 0);

            if (e.Key == Key.A)
                GL.Translate(-0.1, 0, 0);

        }

        private void DrawRhombus()
        {
            GL.Begin(PrimitiveType.Polygon);

            GL.Color3(Color.Linen);
            GL.Vertex2(0.0f, 1.0f);
            GL.Color3(Color.Magenta);
            GL.Vertex2(-1.0f, 0.0f);
            GL.Color3(Color.MediumBlue);
            GL.Vertex2(0.0f, -1.0f);
            GL.Color3(Color.MediumPurple);
            GL.Vertex2(1.0f, 0.0f);

            GL.End();
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color.MidnightBlue);
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);

            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadIdentity();
            GL.Ortho(-1.0, 1.0, -1.0, 1.0, 0.0, 4.0);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {

            base.OnUpdateFrame(e);


            MouseState mouse = OpenTK.Input.Mouse.GetState();

            if (mouse[OpenTK.Input.MouseButton.Right])
                GL.Rotate(-3.0f, 0.0f, 0.0f, 1.0f);

            if (mouse[OpenTK.Input.MouseButton.Left])
                GL.Rotate(3.0f, 0.0f, 0.0f, 1.0f);

            if (mouse.Wheel == +1)
                GL.Scale(1.1, 1.1, 1.1);

            if (mouse.Wheel == -1)
                GL.Scale(0.9, 0.9, 0.9);
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {

            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit);

            DrawRhombus();

            this.SwapBuffers();
        }

        [STAThread]
        static void Main(string[] args)
        {

            using (SimpleWindow example = new SimpleWindow())
            {
                example.Run(30.0, 0.0);
            }
        }
    }
}

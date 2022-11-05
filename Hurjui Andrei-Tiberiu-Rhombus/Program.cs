
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using System;
using System.Drawing;

namespace Rhombus
{
    internal class Window3D : GameWindow
    {
        bool showCube = true;
        KeyboardState lastKeyPress;
        private int transStep = 0;
        private int radStep = 0;
        private int attStep = 0;
        Cube cube = new Cube();
        Vector3 posEye = new Vector3(0, 0, 30);
        private Camera _camera;

        private bool _firstMove = true;

        private Vector2 _lastPos;


        private bool newStatus = false;

        private Window3D() : base(1050, 1050, new GraphicsMode(32, 24, 0, 8)) { }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            GL.ClearColor(Color.MidnightBlue);
            GL.Enable(EnableCap.DepthTest);
            GL.Hint(HintTarget.PolygonSmoothHint, HintMode.Nicest);

            _camera = new Camera(new Vector3(0, 0, 20), 1);
            CursorVisible = false;
            CursorGrabbed = true;

        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            GL.Viewport(0, 0, Width, Height);

            double aspect_ratio = Width / (double)Height;

            GL.Translate(new Vector3(0, 0, 2.5f));
            Matrix4 perspective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, (float)aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perspective);

            Matrix4 lookat = Matrix4.LookAt(posEye.X, posEye.Y, posEye.Z, 0, 0, 0, 0, 1, 0);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);
            GL.Translate(new Vector3(0, 0, 2.5f));

            showCube = true;
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);

            KeyboardState keyboard = Keyboard.GetState();
            MouseState mouse = Mouse.GetState();
            var input = Keyboard.GetState();


            // Se utilizeaza mecanismul de control input oferit de OpenTK (include perifcerice multiple, inclusiv
            // pentru gaminig - gamepads, joysticks, etc.).
            if (keyboard[Key.Escape])
            {
                Exit();
                return;
            }

            if (keyboard[Key.P] && !keyboard.Equals(lastKeyPress))
            {
                // Ascundere comandată, prin apăsarea unei taste - cu verificare de remanență! Timpul de reacție
                // uman << calculator.
                if (showCube)
                {
                    showCube = false;
                }
                else
                {
                    showCube = true;
                }
            }
            if (keyboard[Key.R] && !keyboard.Equals(lastKeyPress))
            {
                if (newStatus)
                {
                    newStatus = false;
                }
                else
                {
                    newStatus = true;
                }
            }

            if (keyboard[Key.Enter] && keyboard[Key.C])
            {
                cube.colors.Clear();
                cube.SetColors();
            }

            if (keyboard[Key.J])
            {
                cube.transparency(-1);
            }

            if (keyboard[Key.K])
            {
                cube.transparency(1);
            }



            lastKeyPress = keyboard;
            const float cameraSpeed = 10.5f;
            const float sensitivity = 0.2f;
            if (input.IsKeyDown(Key.W))
            {
                _camera.Position += _camera.Front * cameraSpeed * (float)e.Time; // Forward
            }

            if (input.IsKeyDown(Key.S))
            {
                _camera.Position -= _camera.Front * cameraSpeed * (float)e.Time; // Backwards
            }
            if (input.IsKeyDown(Key.A))
            {
                _camera.Position -= _camera.Right * cameraSpeed * (float)e.Time; // Left
            }
            if (input.IsKeyDown(Key.D))
            {
                _camera.Position += _camera.Right * cameraSpeed * (float)e.Time; // Right
            }
            if (input.IsKeyDown(Key.Space))
            {
                _camera.Position += _camera.Up * cameraSpeed * (float)e.Time; // Up
            }
            if (input.IsKeyDown(Key.ShiftLeft))
            {
                _camera.Position -= _camera.Up * cameraSpeed * (float)e.Time; // Down
            }

            if (_firstMove) // This bool variable is initially set to true.
            {
                _lastPos = new Vector2(mouse.X, mouse.Y);
                _firstMove = false;
            }
            else
            {
                // Calculate the offset of the mouse position
                var deltaX = mouse.X - _lastPos.X;
                var deltaY = mouse.Y - _lastPos.Y;
                _lastPos = new Vector2(mouse.X, mouse.Y);

                // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity; // Reversed since y-coordinates range from bottom to top
            }
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            if (newStatus)
            {
                cube.DrawCube();
            }

            if (showCube == true)
            {
                GL.PushMatrix();
                GL.Translate(transStep, attStep, radStep);

                cube.DrawCube();
                GL.PopMatrix();
            }

            var value = _camera.GetViewMatrix();
            GL.LoadMatrix(ref value);


            SwapBuffers();
        }


        [STAThread]
        static void Main(string[] args)
        {

            using (Window3D example = new Window3D())
            {
                example.Run(30.0, 0.0);
            }

        }
    }
}

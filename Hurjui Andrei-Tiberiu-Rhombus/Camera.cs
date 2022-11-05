using MathFloat;
using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Rhombus
{
    public class Camera
    {
        //Vectori care indica directia din interiorul camerei spre exterior
        //pentru a defini modul de rotatie
        private Vector3 _front = -Vector3.UnitZ;

        private Vector3 _up = Vector3.UnitY;

        private Vector3 _right = Vector3.UnitX;

        // Rotatie in jurul axei X (in radiani)
        private float _pitch;

        // Rotatie in jurul axei Y (in radiani)
        private float _yaw = -MathHelper.PiOver2; 

        // The field of view of the camera (radians)
        private float _fov = MathHelper.PiOver2;

        public Camera(Vector3 position, float aspectRatio)
        {
            Position = position;
            AspectRatio = aspectRatio;
        }

        // Pozitia camerei
        public Vector3 Position { get; set; }

        // Aspect-ratio-ul view-portului, folosit pentru projection matrix
        public float AspectRatio { private get; set; }

        public Vector3 Front => _front;

        public Vector3 Up => _up;

        public Vector3 Right => _right;

        // Conversie directa din grade in radiani, pentru a imbunatati performanta
        public float Pitch
        {
            get => MathHelper.RadiansToDegrees(_pitch);
            set
            {
                //Setam pitch-ul intre -89 si 89 pentru a preveni camera sa ajunga cu susul in jos.
                var angle = MathHelper.Clamp(value, -89f, 89f);
                _pitch = MathHelper.DegreesToRadians(angle);
                UpdateVectors();
            }
        }

        // Conversie directa din grade in radiani, pentru a imbunatati performanta
        public float Yaw
        {
            get => MathHelper.RadiansToDegrees(_yaw);
            set
            {
                _yaw = MathHelper.DegreesToRadians(value);
                UpdateVectors();
            }
        }

        //Unghiul vertical de vedere al camerei
        public float Fov
        {
            get => MathHelper.RadiansToDegrees(_fov);
            set
            {
                var angle = MathHelper.Clamp(value, 1f, 90f);
                _fov = MathHelper.DegreesToRadians(angle);
            }
        }

        //Returneaza View Matrix
        public Matrix4 GetViewMatrix()
        {
            return Matrix4.LookAt(Position, Position + _front, _up);
        }

        //Returneaza Projection Matrix
        public Matrix4 GetProjectionMatrix()
        {
            return Matrix4.CreatePerspectiveFieldOfView(_fov, AspectRatio, 0.01f, 100f);
        }

        //Actualizarea vertex-urilor de directie
        private void UpdateVectors()
        {
            //Calcularea matricei frontale
            _front.X = MathF.Cos(_pitch) * MathF.Cos(_yaw);
            _front.Y = MathF.Sin(_pitch);
            _front.Z = MathF.Cos(_pitch) * MathF.Sin(_yaw);

            //Normalizarea vectorului, altfel am obtine rezultate bizare
            _front = Vector3.Normalize(_front);

            //Calcularea vectorului dreapta si sus folosind produsul incricisat.
            _right = Vector3.Normalize(Vector3.Cross(_front, Vector3.UnitY));
            _up = Vector3.Normalize(Vector3.Cross(_right, _front));
        }
    }
}

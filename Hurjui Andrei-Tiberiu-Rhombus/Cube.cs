using AdministratorFisierNS;
using OpenTK.Graphics.OpenGL;
using System;
using System.Collections.Generic;
using System.Drawing;

namespace Rhombus
{
    public class Cube
    {
        public List<Color> colors = new List<Color>();
        private readonly List<List<float>> coordonate = new List<List<float>>();
        Admin admin = new Admin();
     

        public Cube()
        {
            coordonate = admin.GetCoordonate();

            SetColors();
        }

        public void transparency(int a)
        {
           

        }

        public void SetColors()
        {
            colors.Clear();
            Random rnd = new Random();
            //Gemerarea a 24 de culori random pentru fiecare vertex
            for (int i = 0; i < 24; i++)
                colors.Add(Color.FromArgb(rnd.Next(256), rnd.Next(256), rnd.Next(256)));



            for (int i = 0; i < colors.Count; i++)
            {

                Console.WriteLine("Vertex[" + i + "]:" + colors[i].ToString() + ", " + colors[i].ToString() + ", " + colors[i].ToString());
            }

            Console.WriteLine("\n");
        }


        //Desenarea cubului
        public void DrawCube()
        {

             GL.Clear(ClearBufferMask.ColorBufferBit);
          
           

            GL.Begin(PrimitiveType.Polygon);


            GL.Color3(colors[0]);
            GL.Vertex3(coordonate[0][0], coordonate[0][1], coordonate[0][2]);
            GL.Color3(colors[1]);
            GL.Vertex3(coordonate[1][0], coordonate[1][1], coordonate[1][2]);
            GL.Color3(colors[2]);
            GL.Vertex3(coordonate[2][0], coordonate[2][1], coordonate[2][2]);
            GL.Color3(colors[3]);
            GL.Vertex3(coordonate[3][0], coordonate[3][1], coordonate[3][2]);
            GL.End();
            

            GL.Begin(PrimitiveType.Polygon);
            GL.Color3(colors[4]);
            GL.Vertex3(coordonate[4][0], coordonate[4][1], coordonate[4][2]);
            GL.Color3(colors[5]);
            GL.Vertex3(coordonate[5][0], coordonate[5][1], coordonate[5][2]);
            GL.Color3(colors[6]);
            GL.Vertex3(coordonate[6][0], coordonate[6][1], coordonate[6][2]);
            GL.Color3(colors[7]);
            GL.Vertex3(coordonate[7][0], coordonate[7][1], coordonate[7][2]);
            GL.End();


            GL.Begin(PrimitiveType.Polygon);
            GL.Color3(colors[8]);
            GL.Vertex3(coordonate[8][0], coordonate[8][1], coordonate[8][2]);
            GL.Color3(colors[9]);
            GL.Vertex3(coordonate[9][0], coordonate[9][1], coordonate[9][2]);
            GL.Color3(colors[10]);
            GL.Vertex3(coordonate[10][0], coordonate[10][1], coordonate[10][2]);
            GL.Color3(colors[11]);
            GL.Vertex3(coordonate[11][0], coordonate[11][1], coordonate[11][2]);
            GL.End();


            GL.Begin(PrimitiveType.Polygon);
            GL.Color3(colors[12]);
            GL.Vertex3(coordonate[12][0], coordonate[12][1], coordonate[12][2]);
            GL.Color3(colors[13]);
            GL.Vertex3(coordonate[13][0], coordonate[13][1], coordonate[13][2]);
            GL.Color3(colors[14]);
            GL.Vertex3(coordonate[14][0], coordonate[14][1], coordonate[14][2]);
            GL.Color3(colors[15]);
            GL.Vertex3(coordonate[15][0], coordonate[15][1], coordonate[15][2]);
            GL.End();


            GL.Begin(PrimitiveType.Polygon);
            GL.Color3(colors[16]);
            GL.Vertex3(coordonate[16][0], coordonate[16][1], coordonate[16][2]);
            GL.Color3(colors[17]);
            GL.Vertex3(coordonate[17][0], coordonate[17][1], coordonate[17][2]);
            GL.Color3(colors[18]);
            GL.Vertex3(coordonate[18][0], coordonate[18][1], coordonate[18][2]);
            GL.Color3(colors[19]);
            GL.Vertex3(coordonate[19][0], coordonate[19][1], coordonate[19][2]);
            GL.End();



            GL.Begin(PrimitiveType.Polygon);

            GL.Color3(colors[23]);
            GL.Vertex3(coordonate[20][0], coordonate[20][1], coordonate[20][2]);
            GL.Color3(colors[20]);
            GL.Vertex3(coordonate[21][0], coordonate[21][1], coordonate[21][2]);
            GL.Color3(colors[21]);
            GL.Vertex3(coordonate[22][0], coordonate[22][1], coordonate[22][2]);
            GL.Color3(colors[22]);
            GL.Vertex3(coordonate[23][0], coordonate[23][1], coordonate[23][2]);

            GL.End();

        }
    }
}

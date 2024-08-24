using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Input;
using OpenTK.Graphics.OpenGL;
using System.Timers;
using Microsoft.VisualBasic;
namespace Tarea2
{
    public class Cara
    {
        public Relativo vertice1;
        public Relativo vertice2;
        public Relativo vertice3;
        public Relativo vertice4;

        public Cara(Relativo A, Relativo B, Relativo C, Relativo D)
        {
            vertice1 = A;
            vertice2 = B;
            vertice3 = C;
            vertice4 = D;
        }

        public void Dibujar(int R, int G, int B)
        {
            GL.Begin(PrimitiveType.Polygon);
            GL.Color3(Color.FromArgb(R, G, B));
            GL.Vertex3(vertice1.X, vertice1.Y, vertice1.Z);
            GL.Vertex3(vertice2.X, vertice2.Y, vertice2.Z);
            GL.Vertex3(vertice3.X, vertice3.Y, vertice3.Z);
            GL.Vertex3(vertice4.X, vertice4.Y, vertice4.Z);
            GL.End();
        }
    }
}

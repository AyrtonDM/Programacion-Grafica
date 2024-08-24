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
    public class Relativo
    {
        public float[] newPoint;
        public float X;
        public float Y;
        public float Z; 
        
        public Relativo(float[] nuevo, float x, float y, float z)
        {   

            newPoint = nuevo;
            X = x;
            Y = y;
            Z = z;
        }

        public void Actualizar()
        {
            X = newPoint[0] + X;  
            Y = newPoint[1] + Y;
            Z = newPoint[2] + Z;
        }

    }
}

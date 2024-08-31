using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea3
{
    public class Punto
    {
        public float X;
        public float Y;
        public float Z;

        public Punto()
        {
            X = Y = Z = 0.0f;
        }

        public Punto(float x, float y, float z)
        {
            X = x;
            Y = y;
            Z = z;
        }

        public void Escalar(float valor)
        {
            X = valor*X;
            Y = valor*Y;
            Z = valor*Z;
        }

        public void Escalar(float x,float y, float z)
        {
            X = x * X;
            Y = y * Y;
            Z = z * Z;
        }
    }
}

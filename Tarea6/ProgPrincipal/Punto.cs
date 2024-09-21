using OpenTK;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace ProgPrincipal
{
    [Serializable]
    public class Punto
    {
        private float X;
        private float Y;
        private float Z;

        public float x { get { return X; } set { X = value; } }
        public float y { get { return Y; } set { Y = value; } }
        public float z { get { return Z; } set { Z = value; } }

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

        //public void RotarEnX(float angulo)
        //{
        //    float rad = angulo * (float)Math.PI / 180.0f;
        //    float nuevoY = (y * (float)Math.Cos(rad)) - (z * (float)Math.Sin(rad));
        //    float nuevoZ = (y * (float)Math.Sin(rad)) + (z * (float)Math.Cos(rad));
        //    y = nuevoY;
        //    z = nuevoZ;
        //}

        //public void RotarEnY(float angulo)
        //{
        //    float rad = angulo * (float)Math.PI / 180.0f;
        //    float nuevoX = (x * (float)Math.Cos(rad)) + (z * (float)Math.Sin(rad));
        //    float nuevoZ = -(x * (float)Math.Sin(rad)) + (z * (float)Math.Cos(rad));
        //    x = nuevoX;
        //    z = nuevoZ;
        //}

        //public void RotarEnZ(float angulo)
        //{
        //    float rad = angulo * (float)Math.PI / 180.0f;
        //    float nuevoX = (x * (float)Math.Cos(rad)) - (y * (float)Math.Sin(rad));
        //    float nuevoY = (x * (float)Math.Sin(rad)) + (y * (float)Math.Cos(rad));
        //    x = nuevoX;
        //    y = nuevoY;
        //}


        //public void RotarEnX(float angulo, Punto centro)
        //{
        //    float rad = MathHelper.DegreesToRadians(angulo);
        //    float dy = Y - centro.y;
        //    float dz = Z - centro.z;
        //    Y = centro.y + (float)((dy * Math.Cos(rad)) - (dz * Math.Sin(rad)));
        //    Z = centro.z + (float)((dy * Math.Sin(rad)) + (dz * Math.Cos(rad)));
        //}

        //public void RotarEnY(float angulo, Punto centro)
        //{
        //    float rad = MathHelper.DegreesToRadians(angulo);
        //    float dx = X - centro.x;
        //    float dz = Z - centro.z;
        //    X = centro.x + (float)(dx * Math.Cos(rad) + dz * Math.Sin(rad));
        //    Z = centro.z + (float)(-dx * Math.Sin(rad) + dz * Math.Cos(rad));
        //}

        //public void RotarEnZ(float angulo, Punto centro)
        //{
        //    float rad = MathHelper.DegreesToRadians(angulo);
        //    float dx = X - centro.x;
        //    float dy = Y - centro.y;
        //    X = centro.x + (float)(dx * Math.Cos(rad) - dy * Math.Sin(rad));
        //    Y = centro.y + (float)(dx * Math.Sin(rad) + dy * Math.Cos(rad));
        //}

        public override string ToString()
        {
            return $"[{x:F2}]-[{y:F2}]-[{z:F2}]";
        }

    }
}

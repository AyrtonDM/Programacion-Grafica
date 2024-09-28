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

        public override string ToString()
        {
            return $"[{x:F2}]-[{y:F2}]-[{z:F2}]";
        }
    }
}

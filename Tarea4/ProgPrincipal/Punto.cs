using System;
using System.Collections.Generic;
using System.Linq;
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

        public void Escalar(float valor)
        {
            X = valor * X;
            Y = valor * Y;
            Z = valor * Z;
        }

        public void Escalar(float x, float y, float z)
        {
            X = x * X;
            Y = y * Y;
            Z = z * Z;
        }
    }
}

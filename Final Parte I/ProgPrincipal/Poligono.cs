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

namespace ProgPrincipal
{
    [Serializable]
    public class Poligono
    {
        public Punto Centro;
        public Dictionary<string,Punto> puntos;

        public Poligono()
        {
            puntos = new Dictionary<string, Punto>();
            Centro = new Punto();

        }

        public Poligono(Dictionary<string,Punto> puntos)
        {
            this.puntos = puntos;
            Centro = new Punto();

        }

        public Poligono(Dictionary<string, Punto> puntos, Punto np)
        {
            this.puntos = puntos;
            Centro = np;
        }

        public Poligono(Punto nc)
        {
            Centro = nc;
            puntos = new Dictionary<string, Punto>();
        }

        public void CalcularCentro()
        {
            if (puntos.Count == 0)
            {
                Centro = new Punto(0, 0, 0);
            }

            float minX = float.MaxValue, minY = float.MaxValue, minZ = float.MaxValue;
            float maxX = float.MinValue, maxY = float.MinValue, maxZ = float.MinValue;

            foreach (var punto in puntos.Values)
            {
                if (punto.x < minX) minX = punto.x;
                if (punto.x > maxX) maxX = punto.x;

                if (punto.y < minY) minY = punto.y;
                if (punto.y > maxY) maxY = punto.y;

                if (punto.z < minZ) minZ = punto.z;
                if (punto.z > maxZ) maxZ = punto.z;
            }

            float centroX = (minX + maxX) / 2;
            float centroY = (minY + maxY) / 2;
            float centroZ = (minZ + maxZ) / 2;

            Centro = new Punto(centroX, centroY, centroZ);
        }


        public void agregar(string clave,Punto p)
        {  
            puntos.Add(clave,p);
            CalcularCentro();
        }

        public void remover(string clave)
        {
            puntos.Remove(clave);
        }

        public void limpiar()
        {
            puntos.Clear();
        }

        public void Escalar(float escalar)
        {
            Matrix4 matrizEscala = Matrix4.CreateScale(escalar, escalar, escalar);
  
            foreach (var clave in puntos.Keys)
            {
                Punto puntoActual = puntos[clave];

                Vector4 punto4 = new Vector4(puntoActual.x - Centro.x, puntoActual.y - Centro.y, puntoActual.z - Centro.z, 1.0f);

                Vector4 puntoEscalado = matrizEscala * punto4;

                puntos[clave] = new Punto(puntoEscalado.X + Centro.x, puntoEscalado.Y + Centro.y, puntoEscalado.Z + Centro.z);
            }
        }


        public void trasladar(Punto desplazamiento)
        {
            Matrix4 matrizTraslacion = Matrix4.CreateTranslation(desplazamiento.x, desplazamiento.y, desplazamiento.z);

            foreach (var clave in puntos.Keys)
            {
                Punto puntoActual = puntos[clave];
                Vector3 punto = new Vector3(puntoActual.x, puntoActual.y, puntoActual.z);

                Vector4 punto4 = new Vector4(punto, 1.0f);

                Vector4 puntoTrasladado = punto4 * matrizTraslacion;

                puntos[clave] = new Punto(puntoTrasladado.X, puntoTrasladado.Y, puntoTrasladado.Z);
            }
            CalcularCentro();
        }

        public void Rotar(float angulox, float anguloy, float anguloz)
        {
            Matrix4 rotacionX = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(angulox));
            Matrix4 rotacionY = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(anguloy));
            Matrix4 rotacionZ = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(anguloz));

            Matrix4 rotacionTotal = rotacionZ * rotacionY * rotacionX;

            foreach (var clave in puntos.Keys)
            {
                Vector3 punto = new Vector3(puntos[clave].x - Centro.x, puntos[clave].y - Centro.y, puntos[clave].z - Centro.z);
                Vector4 punto4 = new Vector4(punto, 1.0f);
                Vector4 transformado = Vector4.Transform(punto4, rotacionTotal);

                puntos[clave] = new Punto(transformado.X + Centro.x, transformado.Y + Centro.y, transformado.Z + Centro.z);
            }
        }

        public void Dibujar()
        {
            GL.Color4(Color.FromArgb(255, 255, 255));
            GL.Begin(PrimitiveType.LineLoop);
            foreach (Punto valor in puntos.Values)
            {
                GL.Vertex3(valor.x, valor.y, valor.z);
            }
            GL.End();
        }
    }
}

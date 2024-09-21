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

        public void agregar(string clave,Punto p)
        {  
            puntos.Add(clave,p);
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
            // Crear una matriz de escala
            Matrix4 escala = Matrix4.CreateScale(escalar, escalar, escalar);

            // Aplicar la matriz a cada punto
            foreach (var clave in puntos.Keys.ToList())
            {
                Vector3 punto = new Vector3(puntos[clave].x, puntos[clave].y, puntos[clave].z);

                // Crear un Vector4 para hacer la transformación
                Vector4 punto4 = new Vector4(punto, 1.0f);
                Vector4 transformado = Vector4.Transform(punto4, escala);

                // Convertir el resultado nuevamente a Vector3
                puntos[clave] = new Punto(transformado.X, transformado.Y, transformado.Z);
            }
        }

        public void trasladar(Punto nc)
        {
            Centro = nc;
        }


        public void Rotar(float angulox, float anguloy, float anguloz)
        {
            // Crear matrices de rotación para cada eje
            Matrix4 rotacionX = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(angulox));
            Matrix4 rotacionY = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(anguloy));
            Matrix4 rotacionZ = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(anguloz));

            // Combinación de las rotaciones: Z * Y * X
            Matrix4 rotacionTotal = rotacionZ * rotacionY * rotacionX;

            // Aplicar la rotación a cada punto
            foreach (var clave in puntos.Keys.ToList())
            {
                Vector3 punto = new Vector3(puntos[clave].x,puntos[clave].y, puntos[clave].z);
                Vector4 punto4 = new Vector4(punto, 1.0f);
                Vector4 transformado = Vector4.Transform(punto4, rotacionTotal);

                puntos[clave] = new Punto(transformado.X, transformado.Y, transformado.Z);
            }

            //// Crear matrices de rotación para cada eje
            //Matrix4 rotacionX = Matrix4.CreateRotationX(MathHelper.DegreesToRadians(angulox));
            //Matrix4 rotacionY = Matrix4.CreateRotationY(MathHelper.DegreesToRadians(anguloy));
            //Matrix4 rotacionZ = Matrix4.CreateRotationZ(MathHelper.DegreesToRadians(anguloz));

            //// Combinación de las rotaciones: Z * Y * X
            //Matrix4 rotacionTotal = rotacionZ * rotacionY * rotacionX;

            //// Aplicar la rotación a cada punto
            //foreach (var clave in puntos.Keys.ToList())
            //{
            //    // Obtener el punto original
            //    Vector3 punto = new Vector3(puntos[clave].x, puntos[clave].y, puntos[clave].z);

            //    // Paso 1: Trasladar el punto de modo que el centro de rotación esté en el origen
            //    Vector3 puntoTrasladado = punto - new Vector3(centroRotacion.x, centroRotacion.y, centroRotacion.z);

            //    // Convertir a Vector4 para realizar la transformación
            //    Vector4 punto4 = new Vector4(puntoTrasladado, 1.0f);

            //    // Paso 2: Rotar el punto trasladado
            //    Vector4 transformado = Vector4.Transform(punto4, rotacionTotal);

            //    // Paso 3: Trasladar el punto de vuelta a su posición original
            //    Vector3 puntoRotado = new Vector3(transformado.X, transformado.Y, transformado.Z) + new Vector3(centroRotacion.x, centroRotacion.y, centroRotacion.z);

            //    // Actualizar el punto en el diccionario
            //    puntos[clave] = new Punto(puntoRotado.X, puntoRotado.Y, puntoRotado.Z);
            //}

        }

        public void Dibujar()
        {
            GL.Color4(Color.FromArgb(255, 255, 255));
            //GL.LineWidth(4.0f);
            GL.Begin(PrimitiveType.LineLoop);
            foreach (Punto valor in puntos.Values)
            {
                GL.Vertex3(valor.x + Centro.x, valor.y + Centro.y, valor.z + Centro.z);

            }
            GL.End();
        }
    }
}

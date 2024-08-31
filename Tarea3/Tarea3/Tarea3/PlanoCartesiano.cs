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

namespace Tarea3
{
    public class PlanoCartesiano
    {
        private float escala;
        private int cantidadDivisiones;

        // Propiedad pública para acceder y modificar la escala
        public float Escala
        {
            get { return escala; }
            set { escala = value; }
        }

        public PlanoCartesiano(float escalaInicial, int divisiones)
        {
            this.escala = escalaInicial;
            this.cantidadDivisiones = divisiones;
        }

        public void CambiarEscala(float nuevaEscala)
        {
            this.escala = nuevaEscala;
        }

        public void Dibujar()
        {
            GL.LineWidth(1.0f);

            // Dibujar los ejes principales

            // Eje X - Rojo
            GL.Color3(1.0f, 0.0f, 0.0f);
            DibujarLinea(-cantidadDivisiones * escala, 0, 0, cantidadDivisiones * escala, 0, 0);

            // Eje Y - Verde
            GL.Color3(0.0f, 1.0f, 0.0f);
            DibujarLinea(0, -cantidadDivisiones * escala, 0, 0, cantidadDivisiones * escala, 0);

            // Eje Z - Azul
            GL.Color3(0.0f, 0.0f, 1.0f);
            DibujarLinea(0, 0, -cantidadDivisiones * escala, 0, 0, cantidadDivisiones * escala);

            GL.LineWidth(1.0f); // Restablecer el grosor de la línea para las divisiones

            // Dibujar las divisiones en el plano XY, XZ y YZ
            GL.Color3(0.0f, 0.0f, 0.0f); // Gris para las líneas de división

            // Divisiones en el plano XY
            for (int i = -cantidadDivisiones; i <= cantidadDivisiones; i++)
            {
                float valor = i * escala;
                // Líneas paralelas al eje X en el plano XY
                DibujarLinea(valor, -cantidadDivisiones * escala, 0, valor, cantidadDivisiones * escala, 0);
                // Líneas paralelas al eje Y en el plano XY
                DibujarLinea(-cantidadDivisiones * escala, valor, 0, cantidadDivisiones * escala, valor, 0);
            }

            // Divisiones en el plano XZ
            for (int i = -cantidadDivisiones; i <= cantidadDivisiones; i++)
            {
                float valor = i * escala;
                // Líneas paralelas al eje X en el plano XZ
                DibujarLinea(valor, 0, -cantidadDivisiones * escala, valor, 0, cantidadDivisiones * escala);
                // Líneas paralelas al eje Z en el plano XZ
                DibujarLinea(-cantidadDivisiones * escala, 0, valor, cantidadDivisiones * escala, 0, valor);
            }

            // Divisiones en el plano YZ
            for (int i = -cantidadDivisiones; i <= cantidadDivisiones; i++)
            {
                float valor = i * escala;
                // Líneas paralelas al eje Y en el plano YZ
                DibujarLinea(0, valor, -cantidadDivisiones * escala, 0, valor, cantidadDivisiones * escala);
                // Líneas paralelas al eje Z en el plano YZ
                DibujarLinea(0, -cantidadDivisiones * escala, valor, 0, cantidadDivisiones * escala, valor);
            }
        }

        private void DibujarLinea(float x1, float y1, float z1, float x2, float y2, float z2)
        {
            GL.Begin(PrimitiveType.Lines);
            GL.Vertex3(x1, y1, z1);
            GL.Vertex3(x2, y2, z2);
            GL.End();
        }
    }
}

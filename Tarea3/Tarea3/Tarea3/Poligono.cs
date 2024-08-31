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
    public class Poligono
    {
        public Punto centro;
        private List<Punto> figuras = new List<Punto>();

        public Poligono()
        {
           figuras  = new List<Punto>();
           centro = new Punto();

        }
        
        public Poligono(List<Punto> puntos, Punto np)
        {
            figuras = puntos;
            centro = np;
        }

        public Poligono(Punto nc)
        {
            centro = nc;
            figuras = new List<Punto>();
        }

        public void agregar(Punto p)
        {
            figuras.Add(p);
        }

        public void remover(Punto p)
        {
            figuras.Remove(p);
        }

        public void limpiar()
        {
            figuras.Clear();
        }

        public void Escalar(float valor)
        {
            foreach (Punto kvp in figuras)
            {
                kvp.Escalar(valor);

            }
        }

        public void Escalar(float x,float y, float z)
        {
            foreach (Punto kvp in figuras)
            {
                kvp.Escalar(x,y,z);

            }
        }


        public void Dibujar()
        {
            GL.Color4(Color.FromArgb(255,255,255));
            //GL.LineWidth(4.0f);
            GL.Begin(PrimitiveType.LineLoop);
            foreach (Punto valor in figuras)
            {
                GL.Vertex3(valor.X + centro.X, valor.Y + centro.Y, valor.Z + centro.Z);

            }
            GL.End();
        }
    }
}

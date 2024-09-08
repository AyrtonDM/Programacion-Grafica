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
        public List<Punto> figuras = new List<Punto>();

        public Poligono()
        {
            figuras = new List<Punto>();
            Centro = new Punto();

        }

        public Poligono(List<Punto> figuras)
        {
            this.figuras = figuras;
            Centro = new Punto();

        }

        public Poligono(List<Punto> puntos, Punto np)
        {
            figuras = puntos;
            Centro = np;
        }

        public Poligono(Punto nc)
        {
            Centro = nc;
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

        public void Escalar(float x, float y, float z)
        {
            foreach (Punto kvp in figuras)
            {
                kvp.Escalar(x, y, z);

            }
        }


        public void Dibujar()
        {
            GL.Color4(Color.FromArgb(255, 255, 255));
            //GL.LineWidth(4.0f);
            GL.Begin(PrimitiveType.LineLoop);
            foreach (Punto valor in figuras)
            {
                GL.Vertex3(valor.x + Centro.x, valor.y + Centro.y, valor.z + Centro.z);

            }
            GL.End();
        }
    }
}

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

        public void Escalar(float valor)
        {
            foreach (Punto kvp in puntos.Values)
            {
                kvp.Escalar(valor);
            }
        }

        public void trasladar(Punto nc)
        {
            Centro = nc;
        }


        public void Rotar(float angulox,float anguloy, float anguloz)
        {
            foreach (Punto p in puntos.Values)
            {
                p.RotarEnX(angulox);
                p.RotarEnY(anguloy);
                p.RotarEnZ(anguloz);
            }
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

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
    public class Objeto
    {
        Punto centro;
        Dictionary<string, Parte> partes;

        public Objeto()
        {
            centro = new Punto();
            partes = new Dictionary<string, Parte>();
        }

        public Objeto(Dictionary<string, Parte> parte, Punto np)
        {
            partes = parte;
            centro = np;
        }

        public Objeto(Punto nc)
        {
            centro = nc;
            partes = new Dictionary<string, Parte>();
        }

        public void agregar(string clave, Parte p)
        {
            p.centro.X = centro.X + p.centro.X;
            p.centro.Y = centro.Y + p.centro.Y;
            p.centro.Z = centro.Z + p.centro.Z;
            partes.Add(clave, p);
        }

        public void remover(string clave)
        {
            partes.Remove(clave);
        }

        public void Dibujar()
        {
            foreach (Parte valor in partes.Values)
            {
                valor.Dibujar();
            }
        }
    }
}

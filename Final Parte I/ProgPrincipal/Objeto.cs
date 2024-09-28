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
    public class Objeto
    {
        public Punto Centro;
        public Dictionary<string, Parte> partes;

        public Objeto()
        {
            Centro = new Punto();
            partes = new Dictionary<string, Parte>();
        }

        public Objeto(Dictionary<string, Parte> parte, Punto np)
        {
            partes = parte;
            Centro = np;
        }

        public Objeto(Punto nc)
        {
            Centro = nc;
            partes = new Dictionary<string, Parte>();
        }

        public void CalcularCentro()
        {
            if (partes.Count == 0)
            {
                Centro = new Punto(0, 0, 0);
            }

            float sumaX = 0, sumaY = 0, sumaZ = 0;

            foreach (var parte in partes.Values)
            {
                sumaX += parte.Centro.x;
                sumaY += parte.Centro.y;
                sumaZ += parte.Centro.z;
            }

            float promedioX = sumaX / partes.Count;
            float promedioY = sumaY / partes.Count;
            float promedioZ = sumaZ / partes.Count;

            Centro = new Punto(promedioX, promedioY, promedioZ);
        }

        public void agregar(string clave, Parte p)
        {
            partes.Add(clave, p);
            CalcularCentro();
        }

        public void remover(string clave)
        {
            partes.Remove(clave);
        }

        public Parte obtener(string clave)
        {
            return partes[clave];
        }

        public void Escalar(float factor)
        {   
            foreach (Parte valor in partes.Values)
            {
                valor.Escalar(factor);
            }
        }

        public void trasladar(Punto nc)
        {
            foreach (Parte kvp in partes.Values)
            {
                kvp.trasladar(nc);
            }
            CalcularCentro();
        }

        public void Rotar(float angulox, float anguloy, float anguloz)
        {
            foreach (Parte valor in partes.Values)
            {
                valor.Centro = Centro;
                valor.Rotar(angulox, anguloy, anguloz);
            }
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

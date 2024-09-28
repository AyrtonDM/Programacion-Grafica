using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgPrincipal
{
    [Serializable]
    public class Parte
    {
        public Punto Centro;
        public Dictionary<string, Poligono> poligonos;

        public Parte()
        {
            poligonos = new Dictionary<string, Poligono>();
            Centro = new Punto();
        }

        public Parte(Dictionary<string, Poligono> poligono, Punto np)
        {
            poligonos = poligono;
            Centro = np;
        }

        public Parte(Punto nc)
        {
            Centro = nc;
            poligonos = new Dictionary<string, Poligono>();
        }

        public void CalcularCentro()
        {
            if (poligonos.Count == 0)
            {
                Centro = new Punto(0, 0, 0);
            }

            float sumaX = 0, sumaY = 0, sumaZ = 0;

            foreach (var poligono in poligonos.Values)
            {
                sumaX += poligono.Centro.x;
                sumaY += poligono.Centro.y;
                sumaZ += poligono.Centro.z;
            }

            float promedioX = sumaX / poligonos.Count;
            float promedioY = sumaY / poligonos.Count;
            float promedioZ = sumaZ / poligonos.Count;

            Centro = new Punto(promedioX, promedioY, promedioZ);
        }

        public void agregar(string clave, Poligono p)
        {
            poligonos.Add(clave, p);
            CalcularCentro();
        }

        public void remover(string clave)
        {
            poligonos.Remove(clave);
        }

        public Poligono obtener(string clave)
        {
            return poligonos[clave];
        }

        public Poligono obtenerpoligono(string clave)
        {
            return poligonos[clave];
        }

        public void Escalar(float factor)
        {
            foreach (Poligono valor in poligonos.Values)
            {
                valor.Centro = Centro;
                valor.Escalar(factor);
            }
            Console.WriteLine(Centro);
        }

        public void trasladar(Punto nc)
        {
            foreach (Poligono kvp in poligonos.Values)
            {
                kvp.trasladar(nc);
            }
            CalcularCentro();
        }

        public void Rotar(float angulox, float anguloy, float anguloz)
        {
            foreach(Poligono valor in poligonos.Values)
            {
                valor.Centro = Centro;
                valor.Rotar(angulox, anguloy, anguloz);
            }
        }

        public void Dibujar()
        {
            foreach (Poligono valor in poligonos.Values)
            {
                valor.Dibujar();
            }
        }
    }
}

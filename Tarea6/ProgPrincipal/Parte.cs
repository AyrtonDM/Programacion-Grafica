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

        public void agregar(string clave, Poligono p)
        {
            p.Centro = Centro;
            poligonos.Add(clave, p);
        }

        public void remover(string clave)
        {
            poligonos.Remove(clave);
        }

        public Poligono obtenerpoligono(string clave)
        {
            return poligonos[clave];
        }

        public void Escalar(float factor)
        {
            foreach (Poligono valor in poligonos.Values)
            {
                valor.Escalar(factor);
            }
        }

        public void trasladar(Punto nc)
        {
            Centro.x = Centro.x + nc.x;
            Centro.y = Centro.y + nc.y;
            Centro.z = Centro.z + nc.z;
            foreach (Poligono kvp in poligonos.Values)
            {
                kvp.trasladar(Centro);

            }
        }

        public void Rotar(float angulox, float anguloy, float anguloz)
        {
            foreach(Poligono valor in poligonos.Values)
            {
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

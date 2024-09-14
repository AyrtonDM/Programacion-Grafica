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

        public void Escalar(float valor)
        {
            //float x = Centro.x * valor;
            //float y = Centro.y * valor;
            //float z = Centro.z * valor;
            //Centro.x = Centro.x + x;
            //Centro.y = Centro.y + y;
            //Centro.z = Centro.z + z;
            foreach (Poligono kvp in poligonos.Values)
            {
                kvp.Escalar(valor);
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
            foreach (Poligono p in poligonos.Values)
            {
                p.Rotar(angulox,anguloy,anguloz);
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

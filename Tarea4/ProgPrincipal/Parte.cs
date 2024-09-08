using System;
using System.Collections.Generic;
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

        public void Dibujar()
        {
            foreach (Poligono valor in poligonos.Values)
            {
                valor.Dibujar();
            }
        }
    }
}

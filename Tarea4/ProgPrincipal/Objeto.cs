using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgPrincipal
{
    [Serializable]
    public class Objeto
    {
        public Punto centro;
        public Dictionary<string, Parte> partes;

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
            p.Centro.x = centro.x + p.Centro.x;
            p.Centro.y = centro.y + p.Centro.y;
            p.Centro.z = centro.z + p.Centro.z;
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

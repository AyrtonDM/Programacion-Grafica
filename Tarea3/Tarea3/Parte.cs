using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tarea3
{
    public class Parte
    {
        public Punto centroObjeto;
        public Punto centro;
        public Dictionary<string,Poligono> poligonos;

        public Parte()
        {
            poligonos = new Dictionary<string,Poligono>();
            centro = new Punto();
        }

        public Parte(Dictionary<string, Poligono> poligono, Punto np)
        {
            poligonos = poligono;
            centro = np;
        }

        public Parte(Punto nc)
        {
            centro = nc;
            poligonos = new Dictionary<string, Poligono>();
        }

        public void agregar(string clave,Poligono p)
        {
            p.centro = centro;
            poligonos.Add(clave,p);
        }

        public void remover(string clave)
        {
            poligonos.Remove(clave);
        }

        public void actualizarCentro()
        {
            centro.X = centro.X + centroObjeto.X;
            centro.Y = centro.Y + centroObjeto.Y;
            centro.Z = centro.Z + centroObjeto.Z;
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

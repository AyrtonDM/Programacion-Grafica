using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProgPrincipal
{
    [Serializable]
    public class Escenario
    {
        public Dictionary<string, Objeto> objetos;

        public Escenario()
        {
            objetos = new Dictionary<string, Objeto>();
        }

        public Escenario(Dictionary<string, Objeto> objeto)
        {
            objetos = objeto;
        }

        public void agregar(string clave, Objeto p)
        {
            objetos.Add(clave, p);
        }

        public void remover(string clave)
        {
            objetos.Remove(clave);
        }

        public void Escalar(float factor)
        {
            foreach (Objeto valor in objetos.Values)
            {
                valor.Escalar(factor);
            }
        }

        public void Trasladar(Punto nc)
        {
            foreach (Objeto valor in objetos.Values)
            {
                valor.trasladar(nc);
            }
        }

        public void Rotar(float angulox, float anguloy, float anguloz)
        {
            foreach (Objeto valor in objetos.Values)
            {
                valor.Rotar(angulox, anguloy, anguloz);
            }
        }   

        public void Dibujar()
        {
            foreach (Objeto valor in objetos.Values)
            {
                valor.Dibujar();
            }
        }
    }
}

using OpenTK;
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
        public Punto Centro;
        public Dictionary<string, Objeto> objetos;

        public Escenario()
        {
            objetos = new Dictionary<string, Objeto>();
            Centro = new Punto(0, 0, 0);
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

        public Objeto obtener(string clave)
        {
            return objetos[clave];
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
                valor.Centro = Centro;
                valor.Rotar(angulox,anguloy,anguloz);
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

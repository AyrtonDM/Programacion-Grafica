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
    public class Escenario
    {
        Dictionary<string, Objeto> objetos;

        public Escenario()
        {
            objetos = new Dictionary<string, Objeto>();
        }

        public Escenario(Dictionary<string,Objeto> objeto)
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

        public void Dibujar()
        {
            foreach (Objeto valor in objetos.Values)
            {
                valor.Dibujar();
            }
        }
    }
}
